using UnityEngine;

public class DirectProjectile : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _damage = 10f;
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
            Destroy(gameObject);
        }
    }

    public void Initialize(Vector2 direction)
    {
        _direction = direction.normalized;

        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
