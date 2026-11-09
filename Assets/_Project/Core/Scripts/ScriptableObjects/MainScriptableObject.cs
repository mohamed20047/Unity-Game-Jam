using UnityEngine;

[CreateAssetMenu(fileName = "MainScriptableObject", menuName = "Unity Game Jam/MainScriptableObject")]
public class MainScriptableObject : ScriptableObject
{
    [SerializeField] private float _playerMovementSpeed;

    public float PlayerMovementSpeed => _playerMovementSpeed;
}
