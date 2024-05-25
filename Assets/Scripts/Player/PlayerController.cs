using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] private float cannonsCooldown = 1f;

    private float cannonsReadyTime = 0f;

    private PlayerInput playerInput;
    private FrameInput frameInput;
    private Movement movement;
    private Rigidbody2D rigidBody;
    private Cannon[] cannons;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }

        playerInput = GetComponent<PlayerInput>();
        movement = GetComponent<Movement>();
        rigidBody = GetComponent<Rigidbody2D>();
        cannons = GetComponentsInChildren<Cannon>();
    }

    private void Update()
    {
        GatherInput();
        ProcessMovement();
        ProcessFiring();
    }

    private void GatherInput()
    {
        frameInput = playerInput.FrameInput;
    }

    private void ProcessMovement()
    {
        movement.SetCurrentMovement(frameInput.Move);
    }

    private void ProcessFiring()
    {
        if (frameInput.Fire && Time.time >= cannonsReadyTime)
        {
            foreach (Cannon cannon in cannons)
            {
                cannon.Fire(rigidBody.velocity, gameObject.layer);
            }
            cannonsReadyTime = Time.time + cannonsCooldown;
        }
    }
}
