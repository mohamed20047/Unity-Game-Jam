using UnityEngine;

public class DirectProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float lifetime = 3f;

    private Vector2 direction;
    private Rigidbody2D rb;

    // Called by the caster right after spawning.
    public void Initialize(Vector2 dir)
    {
        rb = GetComponent<Rigidbody2D>();
        
        direction = dir.normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        rb.linearVelocity = (direction * speed);
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit " + other.name);

        if (other.TryGetComponent(out EnemyHealth enemy))
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}