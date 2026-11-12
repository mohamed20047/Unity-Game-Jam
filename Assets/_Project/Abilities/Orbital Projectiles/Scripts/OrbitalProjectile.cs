using System.Collections.Generic;
using UnityEngine;

public class OrbitalProjectile : MonoBehaviour
{
    [Header("Orbit")]
    [SerializeField] private float _orbitSpeed = 10f;
    [SerializeField] private float _orbitRadius = 25f;

    [Header("Damage")]
    [SerializeField] private float _damage = 15f;
    [Tooltip("Seconds between damage ticks while an enemy stays inside the trigger.")]
    [SerializeField] private float _damageInterval = 0.5f;

    private Transform _centerTransform;
    private float _angle;
    private Dictionary<int, float> _lastDamageTime = new Dictionary<int, float>();

    private void Update()
    {
        if (_centerTransform == null)
        {
            return;
        }

        _angle += _orbitSpeed * Time.deltaTime;
        Vector2 offset = new Vector2(Mathf.Cos(_angle), Mathf.Sin(_angle)) * _orbitRadius;
        transform.position = (Vector2)_centerTransform.position + offset;
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

    public void Initialize(Transform centerTransform)
    {
        _centerTransform = centerTransform;
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
}
