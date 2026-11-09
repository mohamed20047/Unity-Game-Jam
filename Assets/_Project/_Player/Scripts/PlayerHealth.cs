using UnityEngine;

public class PlayerHealth : MonoBehaviour
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
            // Game Over
            GameFlow.I.Go(Phase.GameOver);
            Destroy(gameObject);
        }
    }
}
