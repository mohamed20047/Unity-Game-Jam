using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private MainScriptableObject _mainScriptableObject;

    private Rigidbody2D _rb;
    private IMoveDirection _moveDirection;

    void Start()
    {
        _moveDirection = GetComponent<IMoveDirection>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _moveDirection.MoveDiriction * _mainScriptableObject.PlayerMovementSpeed;
    }
}
