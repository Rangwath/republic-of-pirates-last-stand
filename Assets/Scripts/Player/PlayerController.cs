using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Singleton instance
    public static PlayerController Instance { get; private set; }

    public static event Action OnPlayerDeath;
    public static event Action<int> OnPlayerHealthChanged;

    [SerializeField] private float rightCannonsCooldown = 1f;
    [SerializeField] private float leftCannonsCooldown = 1f;
    [SerializeField] private Cannon[] rightCannons;
    [SerializeField] private Cannon[] leftCannons;

    private float rightCannonsReadyTime = 0f;
    private float leftCannonsReadyTime = 0f;
    private bool canPlayerControl = true;

    private PlayerInput playerInput;
    private FrameInput frameInput;
    private PlayerMovement movement;
    private Rigidbody2D rigidBody;
    private Health health;

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
        movement = GetComponent<PlayerMovement>();
        rigidBody = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        health.OnDeath += HandlePlayerDeath;
        health.OnHealthChanged += HandlePlayerHealthChanged;

        GameManager.OnGameOver += StopPlayerControls;
    }

    private void OnDisable()
    {
        health.OnDeath -= HandlePlayerDeath;
        health.OnHealthChanged -= HandlePlayerHealthChanged;

        GameManager.OnGameOver -= StopPlayerControls;
    }

    private void Update()
    {
        if (canPlayerControl)
        {
            GatherInput();
            ProcessFiring();
        }

        ProcessMovement();
    }

    private void GatherInput()
    {
        frameInput = playerInput.FrameInput;
    }

    private void ProcessMovement()
    {
        if (canPlayerControl)
        {
            movement.SetCurrentMovement(frameInput.Move);
        }
        else
        {
            movement.SetCurrentMovement(Vector2.zero);
        }
    }

    private void ProcessFiring()
    {
        if (frameInput.FireRight && Time.time >= rightCannonsReadyTime)
        {
            foreach (Cannon cannon in rightCannons)
            {
                cannon.Fire(rigidBody.velocity, gameObject.layer);
            }
            rightCannonsReadyTime = Time.time + rightCannonsCooldown;
        }
        
        if (frameInput.FireLeft && Time.time >= leftCannonsReadyTime)
        {
            foreach (Cannon cannon in leftCannons)
            {
                cannon.Fire(rigidBody.velocity, gameObject.layer);
            }
            leftCannonsReadyTime = Time.time + leftCannonsCooldown;
        }
    }

    private void HandlePlayerDeath()
    {
        print(gameObject.name + " : Died");
        OnPlayerDeath?.Invoke();
    }

    private void HandlePlayerHealthChanged(int newPlayerHealth)
    {
        OnPlayerHealthChanged?.Invoke(newPlayerHealth);
    }

    private void StopPlayerControls()
    {
        canPlayerControl = false;
    }
}
