using UnityEngine;

[CreateAssetMenu(menuName = "Unity Game Jam/Upgrades/Health/AddToMaxHealth")]
public class AddToMaxHealthUpgradeSO : UpgradeSO
{
    [SerializeField] private int addAmount;
    public override void Apply(GameObject player)
    {
        if (player.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.MaxHealthAddition += addAmount;
        }
        else
        {
            Debug.LogError("Can't apply this upgrade");

        }
    }

}
