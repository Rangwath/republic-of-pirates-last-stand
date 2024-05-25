using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private PlayerInput playerInput;
    private FrameInput frameInput;
    private Rigidbody2D rigidBody;
    private Movement movement;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }

        playerInput = GetComponent<PlayerInput>();
        rigidBody = GetComponent<Rigidbody2D>();
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        GatherInput();
        ProcessMovement();
    }

    private void GatherInput()
    {
        frameInput = playerInput.FrameInput;
    }

    private void ProcessMovement()
    {
        movement.SetCurrentMovement(frameInput.Move);
    }
}
