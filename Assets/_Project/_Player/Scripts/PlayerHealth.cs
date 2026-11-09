using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _startMaxHealth;
    private int _health;

    public int Health
    {
        get { return _health; }
        set
        {
            _health = Mathf.Max(0, value);
            _health = Mathf.Min(value, MaxHealth);
        }
    }

    public int MaxHealth => _startMaxHealth + MaxHealthAddition;
    public int MaxHealthAddition { get; set; }

    private void Start()
    {
        _health = _startMaxHealth;
    }

    public void TakeDamage(int DamageAmount)
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
