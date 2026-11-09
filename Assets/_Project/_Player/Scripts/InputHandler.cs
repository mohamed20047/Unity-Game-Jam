using UnityEngine;

public class InputHandler : MonoBehaviour , IMoveDirection
{
    private InputSystem_Actions _controls;

    public Vector2 MoveDiriction {  get; set; }

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

    private void OnMovePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        MoveDiriction = obj.ReadValue<Vector2>().normalized;
    }

    private void OnMoveCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        MoveDiriction = Vector2.zero;
    }
}
