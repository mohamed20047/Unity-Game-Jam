using UnityEngine;

public class AutoAimedCaster : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private AutoAimedProjectile _projectilePrefab;
    [SerializeField] private float _cooldown = 3f;

    [Header("Targeting")]
    [SerializeField] private float _detectionRadius = 50f;
    [SerializeField] private LayerMask _enemyLayer;

    private IMoveDirection _moveDirection;
    private Vector2 _facingDirection;
    private float _timer;

    private void Awake()
    {
        _moveDirection = GetComponentInParent<IMoveDirection>();
    }

    private void Start()
    {
        _facingDirection = Vector2.right;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_moveDirection.MoveDirection != Vector2.zero)
        {
            _facingDirection = _moveDirection.MoveDirection;
        }

        if (_timer >= _cooldown)
        {
            _timer -= _cooldown;
            Fire();
        }
    }

    private void Fire()
    {
        Vector2 direction = FindNearestEnemyDirection();
        AutoAimedProjectile projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        projectile.Initialize(direction);
    }

    private Vector2 FindNearestEnemyDirection()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _detectionRadius, _enemyLayer);

        if (hits.Length == 0)
        {
            return _facingDirection;
        }

        float closestDistanceSqr = float.MaxValue;
        Vector2 closestDirection = _facingDirection;

        for (int i = 0; i < hits.Length; i++)
        {
            Vector2 toEnemy = (Vector2)hits[i].transform.position - (Vector2)transform.position;
            float distanceSqr = toEnemy.sqrMagnitude;

            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestDirection = toEnemy.normalized;
            }
        }

        return closestDirection;
    }
}
