using UnityEngine;

public class DirectProjectileCaster : MonoBehaviour
{
    [SerializeField] private DirectProjectile _projectilePrefab;
    [SerializeField] private float _cooldown = 1.5f;

    private IMoveDirection _moveDirection;
    private Vector2 _attackDirection;
    private float _timer;

    private void Awake()
    {
        _moveDirection = GetComponentInParent<IMoveDirection>();
    }

    private void Start()
    {
        _attackDirection = Vector2.right;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_moveDirection.MoveDirection != Vector2.zero)
        {
            _attackDirection = _moveDirection.MoveDirection;
        }

        if (_timer >= _cooldown)
        {
            _timer -= _cooldown;
            Fire();
        }
    }

    private void Fire()
    {
        DirectProjectile projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        projectile.Initialize(_attackDirection);
    }
}
