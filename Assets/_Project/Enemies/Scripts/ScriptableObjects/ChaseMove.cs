using UnityEngine;

[CreateAssetMenu(menuName = "Unity Game Jam/AI/Move/Chase")]
public class ChaseMove : MoveBehaviour
{
    [SerializeField] float speed = 3f;
    public override void Tick(Rigidbody2D self, Transform target)
    {
        self.linearVelocity = ((Vector2)target.position - self.position).normalized * speed;
           
    }
}
