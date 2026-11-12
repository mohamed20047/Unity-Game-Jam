using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour
{
    [Header("Aura Settings")]
    [Tooltip("Radius of the aura reach.")]
    [SerializeField] private float _radius = 2f;
    [Tooltip("Damage dealt per tick.")]
    [SerializeField] private float _damage = 5f;
    [Tooltip("Seconds between damage ticks while an enemy stays inside the aura.")]
    [SerializeField] private float _damageInterval = 1f;

    [Header("References")]
    [SerializeField] private CircleCollider2D _circleCollider;

    private readonly Dictionary<int, float> _lastDamageTime = new Dictionary<int, float>();

    private void Awake()
    {
        if (_circleCollider == null)
        {
            _circleCollider = GetComponent<CircleCollider2D>();
        }

        if (_circleCollider != null)
        {
            _circleCollider.radius = _radius;
            _circleCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleDamage(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        HandleDamage(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _lastDamageTime.Remove(other.GetInstanceID());
    }

    private void HandleDamage(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            int id = other.GetInstanceID();

            if (!_lastDamageTime.TryGetValue(id, out float lastTime) || Time.time - lastTime >= _damageInterval)
            {
                damageable.TakeDamage(_damage);
                _lastDamageTime[id] = Time.time;
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
            _circleCollider.radius = _radius;
            _circleCollider.isTrigger = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireSphere(Vector3.zero, _radius);
    }
#endif
    #endregion
}
