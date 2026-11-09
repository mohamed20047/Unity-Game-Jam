using UnityEngine;

public abstract class MoveBehaviour : ScriptableObject
{
    public abstract void Tick(Rigidbody2D self, Transform target);
}
