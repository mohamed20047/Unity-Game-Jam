using UnityEngine;

public class Caster : MonoBehaviour
{
    [SerializeField] private DirectProjectile projectilePrefab;
    [SerializeField] private float cooldown = 1.5f;

    private IMoveDirection _moveDirection;
    private Vector2 attackDir;
    private float timer;

    private void Awake()
    {
        _moveDirection = GetComponentInParent<IMoveDirection>();
    }

    private void Start()
    {
        attackDir = Vector2.right;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (_moveDirection.MoveDiriction != Vector2.zero)
        {
            attackDir = _moveDirection.MoveDiriction;
        }
        
 
        if (timer >= cooldown)
        {
            timer -= cooldown;
            Fire();
        }
    }

    private void Fire()
    {
        DirectProjectile p = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        p.Initialize(attackDir);
    }
}
