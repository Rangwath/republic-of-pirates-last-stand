using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public FrameInput FrameInput { get; private set; }

    private PlayerInputActions playerInputActions;

    private InputAction move;
    private InputAction fireRight;
    private InputAction fireLeft;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        move = playerInputActions.Player.Move;
        fireRight = playerInputActions.Player.FireRight;
        fireLeft = playerInputActions.Player.FireLeft;
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
            FireRight = fireRight.WasPressedThisFrame(),
            FireLeft = fireLeft.WasPerformedThisFrame()
        };
    }
}

public struct FrameInput
{
    public Vector2 Move;
    public bool FireRight;
    public bool FireLeft;
}
