using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] MoveBehaviour move;  // drag the asset in

    private Rigidbody2D _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        EnemyUpdateManager.Instance.Register(this);
    }
    public void MyFixedUpdate()
    {
        move.Tick(_rb, PlayerMovement.Instance.transform);
    }

    private void OnDisable()
    {
        EnemyUpdateManager.Instance.Deregister(this);
    }

}

