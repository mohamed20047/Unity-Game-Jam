using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Unity Game Jam/Upgrades/_Upgrade Track")]
public class UpgradingTrackSO : ScriptableObject
{
    [SerializeField] private string _title;
    [SerializeField] private Sprite _icon;
    [SerializeField] private List<UpgradeSO> _upgradeSOs = new List<UpgradeSO>();

    public string Title => _title;
    public Sprite Icon => _icon;
    public List<UpgradeSO> UpgradeSOs => _upgradeSOs;
}
