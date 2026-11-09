using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemy1;
    [SerializeField] int _spawnedEnemiesAtStart = 500;
    [SerializeField] private float _StartSpawnInterval= .2f;
    [SerializeField] private float _minSpawnPadding = 2f;    
    [SerializeField] private float _maxSpawnPadding = 6f;    
    [SerializeField] private bool _isSpawning = false;

    private Camera _cam;
    private float _timer;
    private float _minRadius;
    private float _maxRadius;

    private void Start()
    {
        _cam = Camera.main;

        float camHalfHeight = _cam.orthographicSize;
        float camHalfWidth = camHalfHeight * _cam.aspect;

        _minRadius = Mathf.Sqrt((camHalfWidth * camHalfWidth) + (camHalfHeight * camHalfHeight)) + _minSpawnPadding;
        _maxRadius = _minRadius + _maxSpawnPadding;

        for(int i =0; i< _spawnedEnemiesAtStart; i++)
        {
            SpawnInCircleRing();
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _StartSpawnInterval && _isSpawning)
        {
            _timer = 0f;
            SpawnInCircleRing();
        }
    }

    private void SpawnInCircleRing()
    {

        Vector2 spawnPos = GetRandomPositionInCircleRing();
        Instantiate(_enemy1, spawnPos, Quaternion.identity);
    }

    public Vector2 GetRandomPositionInCircleRing()
    {  
        float randomAngle = Random.Range(0f, Mathf.PI * 2);
        float randomRadius = Random.Range(_minRadius, _maxRadius);

        // 5. Convert to Cartesian coordinates (X, Y)
        float x = Mathf.Cos(randomAngle) * randomRadius;
        float y = Mathf.Sin(randomAngle) * randomRadius;

        Vector2 centerPos = new Vector2(_cam.transform.position.x, _cam.transform.position.y);
        return centerPos + new Vector2(x, y);
    }
}
