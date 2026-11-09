using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    [SerializeField] private MainScriptableObject _mainScriptableObject;

    private Rigidbody2D _rb;
    private IMoveDirection _moveDirection;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

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
