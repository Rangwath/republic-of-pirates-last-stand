using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public static Action<Vector2> OnFireCannons;

    [SerializeField] private float cannonsCooldown = 1f;

    private float cannonsReadyTime = 0f;

    private PlayerInput playerInput;
    private FrameInput frameInput;
    private Movement movement;
    private Rigidbody2D rigidBody;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }

        playerInput = GetComponent<PlayerInput>();
        movement = GetComponent<Movement>();
        rigidBody = GetComponent<Rigidbody2D>();
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
            OnFireCannons?.Invoke(rigidBody.velocity);
            cannonsReadyTime = Time.time + cannonsCooldown;
        }
    }
}
