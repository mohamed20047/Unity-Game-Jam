using UnityEngine;

public class AutoAimedProjectile : MonoBehaviour
{
    [SerializeField] private float _speed = 25f;
    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _lifetime = 3f;

    private Vector2 _direction;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rigidbody.linearVelocity = _direction * _speed;
        Destroy(gameObject, _lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage);
        }
    }

    public void Initialize(Vector2 direction)
    {
        _direction = direction.normalized;

        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
