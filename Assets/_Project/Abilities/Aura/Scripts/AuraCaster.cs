using UnityEngine;

public class AuraCaster : MonoBehaviour
{
    [SerializeField] private Aura _auraPrefab;

    private void Start()
    {
        if (_auraPrefab != null)
        {
            Instantiate(_auraPrefab, transform.position, Quaternion.identity, transform);
        }
    }
}
