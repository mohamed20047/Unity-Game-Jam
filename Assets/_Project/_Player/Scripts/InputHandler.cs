using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour, IMoveDirection
{
    private InputSystem_Actions _controls;

    public Vector2 MoveDirection { get; set; }

    private void Awake()
    {
        _controls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _controls.Player.Enable();
        _controls.Player.Move.performed += OnMovePerformed;
        _controls.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        _controls.Player.Move.performed -= OnMovePerformed;
        _controls.Player.Move.canceled -= OnMoveCanceled;
        _controls.Player.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        MoveDirection = context.ReadValue<Vector2>().normalized;
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        MoveDirection = Vector2.zero;
    }
}
