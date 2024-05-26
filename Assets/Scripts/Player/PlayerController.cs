using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Singleton instance
    public static PlayerController Instance { get; private set; }

    public static event Action OnPlayerDeath;

    [SerializeField] private float cannonsCooldown = 1f;

    private float cannonsReadyTime = 0f;

    private PlayerInput playerInput;
    private FrameInput frameInput;
    private Movement movement;
    private Rigidbody2D rigidBody;
    private Health health;
    private Cannon[] cannons;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null) 
        { 
            Instance = this; 
        }
        else if (Instance != this)
        {
            Debug.LogWarning("Another instance detected and destroyed");
            Destroy(gameObject);
        }

        playerInput = GetComponent<PlayerInput>();
        movement = GetComponent<Movement>();
        rigidBody = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        cannons = GetComponentsInChildren<Cannon>();
    }

    private void OnEnable()
    {
        health.OnDeath += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        health.OnDeath -= HandlePlayerDeath;
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

    private void HandlePlayerDeath()
    {
        print(gameObject.name + " : Died");
        OnPlayerDeath?.Invoke();
    }
}
