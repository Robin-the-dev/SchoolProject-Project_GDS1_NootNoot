using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;

public class GuardAI : MonoBehaviour
{
    // === non-static variables ====
    NavMeshAgent agent;
    RawImage hud;
    RaycastHit hit;

    //guard states
    bool patrolling;
    bool chasing;
    bool confused;
    bool capturing;
    bool canHear;
    bool inSight;
    bool heard;

    //Simon's variable for chef's hat mechanic
    public bool hasKey;
    [SerializeField]
    public GameObject key;

    //raycast hit counts
    int playerHits;
    int obstacleHits;

    //patrol position
    int patrolPos;

    public float guardVelo;

    // === static variables ===
    static GameObject player;

    //guard settings
    static readonly float distThreshold = 1.5f; //threshold when calculating Vector3.Distance - are two Vector3 coords at the same spot?
    static readonly float guardSpeed = 6f;      //speed the guards patrol at
    static readonly float soundRadius = 20f;    //radius for sound detection (NavMesh path)
    static readonly float patrolDist = 60f;     //distance between each patrol waypoint

    //raycast settings
    static readonly int rays = 29; //MUST BE ODD (to split the rays evenly from a middle degree)
    static readonly float rayWidth = 4f; //how far apart in degrees each ray will be cast
    static readonly int raysHalved = (rays - 1) / 2; //equal amount of rays on -/+ side of rotation degree
    static readonly float rayDist = 25f; //how far each ray will be cast (ie. if player > rayDist distance away from guard they can't be seen even if in direct line of a ray cast
    static readonly float rayAngle = -0.15f; //raycast downwards angle rather than raycasting from eye level - height angle
    static readonly float rayHeight = 4.3f; //how high up on the guard to raycast from (ie. eyes not the stomach)
    static readonly int rayDepth = 2; //how many depths of rays going at different height angles)

    //patrol waypoint corners
    static Vector3[] patrolCorners;
    static Vector3 topleft;
    static Vector3 topright;
    static Vector3 bottomright;
    static Vector3 bottomleft;

    //capture drop off point
    static Vector3 dropOff;
    //has the penguin been captured before? (checked by UI manager?)
    public static bool capturedBefore;
    
    //For audio scripts
    private GuardAudio guardAudio;

    void Start()
    { 
        player = GameObject.FindGameObjectWithTag("Player");
        hud = GetComponentInChildren<RawImage>();
        agent = GetComponent<NavMeshAgent>();
        guardAudio = GetComponent<GuardAudio>();

        patrolling = true;
        patrolCorners = new Vector3[4];

        topleft = new Vector3(-15, 1f, 55f);
        topright = bottomright = bottomleft = topleft;
        topright.x += patrolDist;
        bottomright.x = topright.x;
        bottomright.z -= patrolDist;
        bottomleft.z = bottomright.z;

        patrolCorners[0] = topleft;
        patrolCorners[1] = topright;
        patrolCorners[2] = bottomright;
        patrolCorners[3] = bottomleft;

        dropOff = GameObject.FindGameObjectWithTag("Pane").transform.position;

        //set next patrolling corner based on guard starting position
        for (int i=0; i < patrolCorners.Length; i++)
            if (Vector3.Distance(transform.position, patrolCorners[i]) < distThreshold)
                patrolPos = i + 1;

    }

    void FixedUpdate()
    {

        if (agent.isStopped)
            guardVelo = 0;
        else
            guardVelo = agent.speed;

        if (!capturing) //ignore everything if capturing the penguin
        {
            // --- Raycasting ---
            playerHits = obstacleHits = 0; //reset hit counters
            for (int i = -raysHalved; i <= raysHalved; i++) //loop to recast rays every frame
            {
                for (int level = 1; level < rayDepth+1; level++) //loop for ray cast depths
                {
                    //get angle of current y-axis rotation (direction facing), apply index degree (FOV) and then convert that angle back to a direction vector
                    float angle = (transform.rotation.eulerAngles.y + (i * rayWidth));
                    Vector3 angleVector = Quaternion.Euler(0, angle - 90, 0) * new Vector3(1, 0);

                    float newRayDist = rayDist/level; //change the ray distance based on the the ray depth
                    angleVector.y = rayAngle * level; //look down cast
                    Vector3 rayCastHeight = new Vector3(transform.position.x, transform.position.y + rayHeight, transform.position.z); //new height to raycast from (eyes etc.)

                    //used for testing, draws all the rays in the game scene
                    Debug.DrawRay(rayCastHeight, angleVector * newRayDist, new Color(1f, 0.7f, 0f, 0.20f), 0, true);

                    if (Physics.Raycast(rayCastHeight, angleVector, out hit, newRayDist))
                    {   //check for ray hits
                        if (hit.collider.gameObject.CompareTag("Player"))
                        {
                            playerHits++;
                        }
                        else if (hit.collider.gameObject.CompareTag("Obstacle"))
                            obstacleHits++;
                    }
                }
            }
            // --- END Raycasting ---

            // inSight checks
            if (playerHits > 0) //player in sight if 2 rays hit them
                inSight = true;
            else
                inSight = false;

            if (obstacleHits>15)
            {
                inSight = false;
            }

            // canHear checks
            canHear = CheckPlayerDist(player.transform.position);

            // check if guard is next to player ie. capture penguin
            if (Vector3.Distance(player.transform.position, transform.position) < distThreshold)
            {
                capturing = true;
                guardAudio.PlayAudio("Captured");
            }

            if (inSight || canHear)
            {
                //patrolling so goes into alert mode
                if (patrolling)
                {
                    guardAudio.PlayAudio("Alert"); // Plays alert audio
                    patrolling = false;
                    if (inSight)
                        hud.texture = Loader.hudImage[1]; //1 = sight
                    if (canHear)
                        hud.texture = Loader.hudImage[2]; //2 = sound
                    Invoke("DelayChase", 3f); //wait 2 seconds to chase
                }
                else if (confused)
                {
                    confused = false;
                    guardAudio.PlayAudio("Chasing"); // Plays chasing audio
                    Chase();
                }
                else if (chasing)
                    Chase();
            }

            if (!patrolling && !inSight && !canHear) // guard has lost the player (confused)
            {
                if (!confused)
                    guardAudio.PlayAudio("Confused"); // Plays Confused audio
                chasing = false;
                confused = true;
                hud.texture = Loader.hudImage[4];
                agent.speed = guardSpeed - 3;
                
                Invoke("DelayHUDRemove", 5f); //stay confused for 3 seconds before going back to patrolling
            }

            if (patrolling)
                Patrol();
        }
        else //if capturing is true
            CapturingPenguin();
    }

    void Chase()
    {
        confused = false;
        hud.texture = Loader.hudImage[3]; //3 = chase
        agent.SetDestination(player.transform.position); //chase the penguin
        agent.speed = guardSpeed + 3; //increase guard speed
    }
    
    void DelayChase()
    {
        if (inSight || canHear)
        {
            chasing = true;
            guardAudio.PlayAudio("Chasing"); // Plays chasing audio
        }
        else
            confused = true;
    }

    void DelayHUDRemove()
    {
        //go back to patrolling
        if (!inSight && !canHear)
        {
            confused = false;
            patrolling = true;
        }
    }

    bool CheckPlayerDist(Vector3 location)
    { //checks the player's distance from the guard by calculating a path to them (and can't go through obstacles) | this is mimicking sound from the player

        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(location, path);

        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length-1] = location;

        for(int i=0; i<path.corners.Length; i++)
            allWayPoints[i+1] = path.corners[i];

        float pathLength = 0f;

        for(int i=0; i<allWayPoints.Length-1; i++)
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);

        //penguin within the sound radius and is moving - can be heard
        if (pathLength < soundRadius)
        {
            if (PlayerMovement.isMoving)
            {
                heard = true;
                Invoke("CanHear", 1f);
                if (heard)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        else
            return false;
    }

    void Patrol()
    {
        hud.texture = Loader.hudImage[0];
        agent.speed = guardSpeed;

        agent.SetDestination(patrolCorners[patrolPos]);

        // go from waypoint to waypoint
        if (Vector3.Distance(transform.position, patrolCorners[patrolPos]) < distThreshold)
        {
            if (patrolPos == patrolCorners.Length-1)
                patrolPos = 0;
            else
                patrolPos++;
        }
    }

    void CapturingPenguin()
    {
        if (hasKey)
        {
            hasKey = false;
            key.transform.parent = player.transform;
            key.transform.localPosition = new Vector3(0.085f, 3.533f, -0.122f);
            key.transform.localRotation = Quaternion.Euler(83.219f, 360.0f, -0.001f);
            Debug.Log("transferKey");
        }
        agent.speed = guardSpeed + 3f;
        hud.texture = Loader.hudImage[5]; //captured indicator
        agent.SetDestination(dropOff); //go to the dropoff point
        PlayerMovement.captured = true; //disable penguin movement
        PlayerMovement.guardVelocity = agent.velocity;
        PlayerMovement.guardPos = transform.position; //send the guard's position to the player script

        // if penguin at the drop off point now
        if (Vector3.Distance(player.transform.position, dropOff) < 5f)
        {
            Debug.Log("drop off");
            PlayerMovement.captured = false;
            PlayerMovement.droppedOff = true;
            Invoke("DropOffDelay", 2f);
        }
        // if the penguin escaped guard's grasp
        else if (Vector3.Distance(player.transform.position, transform.position) > 4f)
        {
            PlayerMovement.captured = false;
            Invoke("DropOffDelay", 2f);
        }
    }

    void DropOffDelay()
    {
        chasing = confused = capturing = false;
        PlayerMovement.droppedOff = false;
        patrolling = true;
    }

    void CanHear()
    {
        if (!PlayerMovement.isMoving)
            heard = false;
    }        
}