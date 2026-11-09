using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    private float _health;
    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(float DamageAmount)
    {
        _health -= DamageAmount;
        if (_health <= 0)
        {
            Destroy(gameObject); //TODO: Use the pooler
        }
    }
}
