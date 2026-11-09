using System.Collections.Generic;
using UnityEngine;

//HACK: to run before the OnEnable of the EnemyMovement script
[DefaultExecutionOrder(-100)]
public class EnemyUpdateManager : MonoBehaviour
{
    public static EnemyUpdateManager Instance { get; private set; }

    private readonly List<EnemyMovement> _enemies = new List<EnemyMovement>(512);

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Register(EnemyMovement enemy)
    {
        _enemies.Add(enemy);
    }

    public void Deregister(EnemyMovement enemy)
    {
        // TODO: Optimize it -if needed- to be O(1) by save the enemy index number.
        _enemies.Remove(enemy);
    }
    private void FixedUpdate()
    {
        foreach(EnemyMovement enemy in _enemies)
        {
            enemy.MyFixedUpdate();
        }
    }

}
