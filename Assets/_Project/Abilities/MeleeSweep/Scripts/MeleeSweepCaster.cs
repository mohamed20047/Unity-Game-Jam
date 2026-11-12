using UnityEngine;

public class MeleeSweepCaster : MonoBehaviour
{
    private enum State
    {
        Cooldown,
        Active
    }

    [Header("Sweep Settings")]
    [SerializeField] private MeleeSweepArea _sweepPrefab;
    [Tooltip("How long the sweep attack stays active.")]
    [SerializeField] private float _activeDuration = 0.7f;
    [Tooltip("Wait time after a sweep finishes before the next sweep begins.")]
    [SerializeField] private float _cooldown = 1f;

    private IMoveDirection _moveDirection;
    private Vector2 _facingDirection;
    private float _timer;
    private State _state;
    private MeleeSweepArea _currentSweep;

    private void Awake()
    {
        _moveDirection = GetComponentInParent<IMoveDirection>();
    }

    private void Start()
    {
        _facingDirection = Vector2.right;
        _timer = _cooldown;
        _state = State.Cooldown;
    }

    private void Update()
    {
        if (_moveDirection != null && _moveDirection.MoveDirection != Vector2.zero)
        {
            _facingDirection = _moveDirection.MoveDirection;
        }

        _timer += Time.deltaTime;

        if (_state == State.Cooldown)
        {
            if (_timer >= _cooldown)
            {
                _timer = 0f;
                _state = State.Active;
                StartSweep();
            }
        }
        else if (_state == State.Active)
        {
            if (_timer >= _activeDuration)
            {
                _timer = 0f;
                _state = State.Cooldown;
                EndSweep();
            }
        }
    }

    private void OnDisable()
    {
        EndSweep();
    }

    private void StartSweep()
    {
        if (_sweepPrefab == null)
        {
            return;
        }

        float angle = Mathf.Atan2(_facingDirection.y, _facingDirection.x) * Mathf.Rad2Deg;
        _currentSweep = Instantiate(_sweepPrefab, transform.position, Quaternion.Euler(0f, 0f, angle), transform);
        _currentSweep.Initialize(_facingDirection);
    }

    private void EndSweep()
    {
        if (_currentSweep != null)
        {
            Destroy(_currentSweep.gameObject);
            _currentSweep = null;
        }
    }
}
