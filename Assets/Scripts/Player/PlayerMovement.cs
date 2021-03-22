using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rgbd;
    private Camera followCam;
    private PlayerInput playerInput;

    public TestPoleVault tpv;

    [SerializeField] private float speed = 30f;
    [SerializeField] private float swimSpeed = 5f;

    private float turnSmoothVelocity; // Variable for smooth damping
    [SerializeField] private float turnSmoothTime = 0.1f; // Variable for increasing turning speed incrementally and smoothly

    private float speedSmoothVelocity; // Variable for smooth damping
    [SerializeField] private float speedSmoothTime = 0.1f; // Variable for increasing speed incrementally and smoothly

    public static bool isMoving;
    public static bool captured;
    public static bool droppedOff;
    public static Vector3 guardPos;
    public static Vector3 guardVelocity;

    public Rigidbody rgbdPenguin;
    //public Rigidbody rgbdPenguinHead;
    public Rigidbody rgbdPenguinBeak;

    public float currentSpeed =>
    new Vector2(rgbd.velocity.x, rgbd.velocity.z).magnitude; // current speed for smooth damping

    private void Start()
    {
        rgbd = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        followCam = Camera.main;
    }

    private void FixedUpdate()
    {
        if (!captured)
        {
            if (!playerInput.lookAround && !tpv.isVaulting) Rotate();
            if (Water.isWater)
            {
                SwimUp(playerInput.swimInput);
            }
            else
            {
                Move(playerInput.moveInput);
            }
            if (playerInput.jump)
            {
                rgbdPenguin.useGravity = false;
                rgbdPenguinBeak.useGravity = false;
            }
            else
            {
                rgbdPenguin.useGravity = true;
                rgbdPenguinBeak.useGravity = true;
            }
            
        }
        else
        {
            rgbd.useGravity = false;
            rgbd.MovePosition(new Vector3(guardPos.x, 3f, guardPos.z + 1.5f));

            if (droppedOff)
                CheckpointManager.Instance.teleportToCurrCp();
        }


    }

    private void Move(Vector2 moveInput)
    {
        var targetSpeed = speed * moveInput.magnitude;
        var moveDirection = Vector3.Normalize(transform.forward * moveInput.y + transform.right * moveInput.x);

        targetSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        var velocity = targetSpeed * moveDirection;

        if (velocity != Vector3.zero)
            isMoving = true;
        else
            isMoving = false;

        rgbd.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    public void Rotate()
    {
        var targetRotation = followCam.transform.eulerAngles.y;

        targetRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);

        transform.eulerAngles = Vector3.up * targetRotation;
    }

    public void SwimUp(Vector3 swimInput)
    {
        var targetSpeed = speed * swimInput.magnitude;
        var moveDirection = Vector3.Normalize(transform.forward * swimInput.z + transform.right * swimInput.x + transform.up * swimInput.y);

        targetSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        var velocity = targetSpeed * moveDirection;

        var move = transform.position + velocity * Time.deltaTime;

        rgbd.MovePosition(move);
    }
}
