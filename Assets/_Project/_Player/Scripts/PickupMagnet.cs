using UnityEngine;

public class PickupMagnet : MonoBehaviour
{
    [SerializeField] private Transform target; // the player

    private void OnTriggerEnter2D(Collider2D other)
    {
        // The collision matrix guarantees only pickups reach this point
        if (other.TryGetComponent(out Orb pickup))
        {
            pickup.Attract(target);
        }
    }
}