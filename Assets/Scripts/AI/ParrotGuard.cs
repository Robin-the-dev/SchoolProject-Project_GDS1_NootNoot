using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ParrotGuard : MonoBehaviour
{
    // === non-static variables ====
    NavMeshAgent agent;
    RawImage hud;

    //guard states
    bool patrolling;
    bool capturing;
    bool chasing;

    //patrol position
    int patrolPos;

    int guardLvl;

    bool inEnclosure;

    public float guardVelo;
    bool inRange;

    //patrol waypoint corners
    Vector3[] patrolPoints;
    Vector3 pos1;
    Vector3 pos2;


    // === static variables ===
    static GameObject player;

    //guard settings
    static readonly float distThreshold = 2f; //threshold when calculating Vector3.Distance - are two Vector3 coords at the same spot?
    static readonly float guardSpeed = 6f;

    static Vector3 door;
    static Vector3 pole;
    //capture drop off point

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
        patrolPoints = new Vector3[2];
        inEnclosure = true;
        inRange = false;
        chasing = false;

        if (Mathf.Approximately(transform.position.y, 17f))
        {
            guardLvl = 2;
            pos1 = new Vector3(55, 17.2f, -102);
            pos2 = pos1;
            pos2.z = pos1.z + 42;
            pos2.x = pos1.x - 35;
            patrolPoints[0] = pos1;
            patrolPoints[1] = pos2;
        }
        else if (Mathf.Approximately(transform.position.y, 0))
        {
            guardLvl = 1;
            pos1 = new Vector3(50, 0, -120);
            pos2 = pos1;
            pos2.z = pos1.z + 70f;
            patrolPoints[0] = pos1;
            patrolPoints[1] = pos2;
        }


        door = GameObject.FindGameObjectWithTag("Door").transform.position;
        pole = GameObject.FindGameObjectWithTag("CagePole").transform.position;

        patrolPos = 0;
    }

    void FixedUpdate()
    {
        if (agent.isStopped)
            guardVelo = 0;
        else
            guardVelo = agent.speed;

        if (capturing)
        {
            CapturePenguin();
        }
        else
        {
            if (Vector3.Distance(player.transform.position, transform.position) < distThreshold)
            {
                capturing = true;
                guardAudio.PlayAudio("Captured");
                CapturePenguin();
            }

            CheckPenguin();

            if (chasing)
            {
                ChasePenguin();
            }

            if (patrolling)
                Patrol();
        }
    }

    void CapturePenguin()
    {
        agent.speed = guardSpeed + 3f;
        hud.texture = Loader.hudImage[5]; //captured indicator
        PlayerMovement.captured = true; //disable penguin movement
        PlayerMovement.guardPos = transform.position; //send the guard's position to the player script
        PlayerMovement.guardVelocity = agent.velocity;

        agent.SetDestination(door); //go to door
        if (Vector3.Distance(player.transform.position, door) < 5f)
        {
            PlayerMovement.droppedOff = true;
            PlayerMovement.captured = false;
            capturing = false;
            patrolling = true;
            Invoke("DropOffDelay", 2f);
        }

        if (guardLvl == 1)
            Parrot.onShoulder = false;

    }

    void DropOffDelay()
    {
        PlayerMovement.droppedOff = false;
        
    }

    void CheckPenguin()
    {

        if (Vector3.Distance(transform.position, pole) < 40f)
            inEnclosure = true;
        else
            inEnclosure = false;

        if (Vector3.Distance(player.transform.position, pole) < 45f)
            inRange = true;

        if (inRange && inEnclosure)
        {
            chasing = true;
            guardAudio.PlayAudio("Chasing");
        }
        else
            chasing = false;

    }

    void ChasePenguin()
    {
        if (!capturing)
        {
            guardAudio.PlayAudio("Chasing"); // Plays chasing audio
            patrolling = false;
            hud.texture = Loader.hudImage[3]; //3 = chase
            agent.SetDestination(player.transform.position); //chase the penguin
            agent.speed = guardSpeed + 8; //increase guard speed
        }

    }

    void Patrol()
    {
        hud.texture = Loader.hudImage[0];
        agent.speed = guardSpeed;

        agent.SetDestination(patrolPoints[patrolPos]);
        ;

        // go from waypoint to waypoint
        if (Vector3.Distance(transform.position, patrolPoints[patrolPos]) < distThreshold)
        {
            if (patrolPos == patrolPoints.Length - 1)
                patrolPos = 0;
            else
                patrolPos++;
        }
    }

}