using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public FrameInput FrameInput { get; private set; }

    private PlayerInputActions playerInputActions;

    private InputAction move;
    private InputAction fire;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        move = playerInputActions.Player.Move;
        fire = playerInputActions.Player.Fire;
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    private void Update()
    {
        FrameInput = GatherInput();
    }

    private FrameInput GatherInput()
    {
        return new FrameInput
        {
            Move = move.ReadValue<Vector2>(),
            Fire = fire.WasPressedThisFrame()
        };
    }
}

public struct FrameInput
{
    public Vector2 Move;
    public bool Fire;
}
