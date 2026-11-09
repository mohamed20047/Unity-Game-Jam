using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    //TODO: make and editor script or a function in the start to check that every upgrade have it own track in track field
    [SerializeField] private List<UpgradingTrackSO> _tracks = new List<UpgradingTrackSO>();

    private List<UpgradeSO> _availableUpgrades = new List<UpgradeSO>();
    private void Start()
    {
        foreach (UpgradingTrackSO track in _tracks)
        {
            _availableUpgrades.Add(track.UpgradeSOs[0]);
        }
    }

    public List<UpgradeSO> GetThreeUpgrades()
    {
        List<UpgradeSO> result = new List<UpgradeSO>();

        List<UpgradeSO> pool = new List<UpgradeSO>(_availableUpgrades);

        // Determine how many we can safely pick (in case there are fewer than 3 available)
        int countToPick = Mathf.Min(3, pool.Count);

        for (int i = 0; i < countToPick; i++)
        {
            int randomIndex = Random.Range(0, pool.Count);
            result.Add(pool[randomIndex]);
            pool.RemoveAt(randomIndex); // Remove to prevent duplicates
        }

        return result;
    }

    public void ApplyUpgrade(UpgradeSO upgrade)
    {
        upgrade.Apply(PlayerMovement.Instance.gameObject);

        _availableUpgrades.Remove(upgrade);

        //TODO: Find Better Way to that. maybe by save the track level in dictionary
        _availableUpgrades.Add(upgrade.Track.UpgradeSOs[upgrade.Track.UpgradeSOs.IndexOf(upgrade) + 1]);
    }
}
