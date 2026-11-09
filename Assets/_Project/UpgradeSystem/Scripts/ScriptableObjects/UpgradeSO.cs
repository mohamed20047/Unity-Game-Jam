using UnityEngine;


public abstract class UpgradeSO : ScriptableObject
{
    [SerializeField] private string _description;
    [SerializeField] private UpgradingTrackSO _track;
    public string Description => _description;
    public UpgradingTrackSO Track => _track;
    public abstract void Apply(GameObject player);
}
