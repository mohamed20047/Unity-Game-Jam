using System.Collections.Generic;
using UnityEngine;

public class MeleeSweepArea : MonoBehaviour
{
    [Header("Sweep Area")]
    [Tooltip("Reach of the melee sweep.")]
    [SerializeField] private float _range = 2f;
    [Tooltip("Total angle width of the arc in degrees.")]
    [SerializeField] private float _arcAngle = 120f;

    [Header("Damage")]
    [Tooltip("Damage dealt per sweep.")]
    [SerializeField] private float _damage = 10f;

    [Header("References")]
    [SerializeField] private CircleCollider2D _circleCollider;

    private readonly HashSet<int> _hitEnemyIds = new HashSet<int>();

    private void Awake()
    {
        if (_circleCollider == null)
        {
            _circleCollider = GetComponent<CircleCollider2D>();
        }

        if (_circleCollider != null)
        {
            _circleCollider.radius = _range;
            _circleCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleHit(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        HandleHit(other);
    }

    public void Initialize(Vector2 facingDirection)
    {
        float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void HandleHit(Collider2D other)
    {
        Vector2 closestPoint = other.bounds.ClosestPoint(transform.position);
        Vector2 toTarget = closestPoint - (Vector2)transform.position;

        if (toTarget != Vector2.zero)
        {
            float angle = Vector2.Angle(transform.right, toTarget);

            if (angle > _arcAngle * 0.5f)
            {
                return;
            }
        }

        if (other.TryGetComponent(out IDamageable damageable))
        {
            Component damageableComponent = damageable as Component;
            int id = damageableComponent != null ? damageableComponent.gameObject.GetInstanceID() : other.GetInstanceID();

            if (_hitEnemyIds.Add(id))
            {
                damageable.TakeDamage(_damage);
            }
        }
    }

    #region Editor Callbacks
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_circleCollider == null)
        {
            _circleCollider = GetComponent<CircleCollider2D>();
        }

        if (_circleCollider != null)
        {
            _circleCollider.radius = _range;
            _circleCollider.isTrigger = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireSphere(Vector3.zero, _range);
    }
#endif
    #endregion
}
