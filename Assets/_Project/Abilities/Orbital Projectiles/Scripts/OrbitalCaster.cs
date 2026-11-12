using UnityEngine;

public class OrbitalCaster : MonoBehaviour
{
    [SerializeField] private OrbitalProjectile _orbitalPrefab;

    private void Start()
    {
        OrbitalProjectile orb = Instantiate(_orbitalPrefab);
        orb.Initialize(transform.parent);
    }
}
