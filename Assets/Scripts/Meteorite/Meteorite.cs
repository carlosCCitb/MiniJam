using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using System.Threading;
using UnityEngine;

public class Meteorite : MonoBehaviour, IDamageable
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Shooter _shooter;
    [SerializeField] private PathManager _pathManager;

    [Space, SerializeField] private int _healthPoints;
    [SerializeField, ReadOnly] private int _currentHealthPoints;

    [Space, SerializeField] private int _damage;
    [SerializeField] private float _shootingSpeed;
    [SerializeField] private float _shootingCooldown;

    [Space, SerializeField] private float _acceleration;
    [SerializeField] private Vector2 _minMaxSpeed;
    [SerializeField, ReadOnly] private Vector2 _inputVelocity;
    [SerializeField, ReadOnly] private float _currentVerticalSpeed;

    private float _lastTimeShoot;

    private CancellationTokenSource _cancellationTokenSource;

    private void Awake()
    {
        _currentHealthPoints = _healthPoints;
    }

    private void OnEnable()
    {
        _playerInput.OnInputMove += OnInputMoveChanged;
        _playerInput.OnInputAttack += OnInputAttack;
    }

    private void OnDisable()
    {
        _playerInput.OnInputMove -= OnInputMoveChanged;
        _playerInput.OnInputAttack -= OnInputAttack;

        _cancellationTokenSource?.Cancel();
    }

    private void FixedUpdate()
    {
        float healthSpeedRatio = ((float)_currentHealthPoints / (float)_healthPoints) + 0.5f;

        _rigidbody.AddForce(_acceleration * _rigidbody.mass * _inputVelocity / (Mathf.Max(Mathf.Abs(_currentVerticalSpeed), 1000.0f) * healthSpeedRatio * 0.01f), ForceMode2D.Force);
        
        _currentVerticalSpeed -= (_inputVelocity.y + (_camera.transform.position.y - _rigidbody.position.y)) * _acceleration * Time.fixedDeltaTime;
        _currentVerticalSpeed = Mathf.Clamp(_currentVerticalSpeed, _minMaxSpeed.x, _minMaxSpeed.y);

        _pathManager.AddDisplacement(_currentVerticalSpeed, _minMaxSpeed, Time.fixedDeltaTime);
    }

    private void OnInputMoveChanged(Vector2 input)
    { 
        _inputVelocity = input;
    }

    private void OnInputAttack(bool value)
    {
        _cancellationTokenSource?.Cancel();

        if (value)
        {
            _cancellationTokenSource = new();
            ShootAsync().Forget();
        }
    }

    private async UniTaskVoid ShootAsync()
    {
        await UniTask.WaitUntil(() => Time.timeScale != 0);

        float timeSinceLastShoot = Time.time - _lastTimeShoot;

        if (timeSinceLastShoot < _shootingCooldown)
            await UniTask.Delay(TimeSpan.FromSeconds(_shootingCooldown - timeSinceLastShoot), cancellationToken: _cancellationTokenSource.Token);

        while (enabled)
        {
            Vector2 shootingTarget = _camera.ScreenToWorldPoint(_playerInput.MousePosition);

            _shooter.Shoot(Bullet.Type.Standard, shootingTarget, transform.position, (shootingTarget - (Vector2)transform.position).normalized * _shootingSpeed, _damage);
            _lastTimeShoot = Time.time;
            await UniTask.Delay(TimeSpan.FromSeconds(_shootingCooldown), cancellationToken: _cancellationTokenSource.Token);
        }
    }

    public void OnHurt(int damage)
    {
        _currentHealthPoints -= damage;
        Mathf.Max(_currentHealthPoints, 0);

        if (_currentHealthPoints == 0)
            OnDie();
    }

    public void OnDie()
    {
        // TODO
        Debug.Log($"[{nameof(Meteorite)}.cs] Player dead");

        enabled = false;
    }

    [Button]
    private void DoDamage()
    {
        OnHurt(10);
    }

    private void OnDestroy()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
    }
}
