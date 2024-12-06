using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using System.Threading;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Shooter _shooter;

    [Space, SerializeField] private int _damage;
    [SerializeField] private float _shootingSpeed;
    [SerializeField] private float _shootingCooldown;

    [Space, SerializeField] private float _acceleration;
    [SerializeField] private Vector2 _maxMinVelocity;
    [ShowNonSerializedField, ReadOnly] private float _currentVelocity;
    [ShowNonSerializedField, ReadOnly] private Vector2 _inputVelocity;

    private float _lastTimeShoot;

    private CancellationTokenSource _cancellationTokenSource;

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
        _rigidbody.AddForce(_acceleration * _rigidbody.mass * _inputVelocity / (Mathf.Max(Mathf.Abs(_currentVelocity), 1000.0f) * 0.01f), ForceMode2D.Force);
        
        _currentVelocity -= (_inputVelocity.y + (_camera.transform.position.y - _rigidbody.position.y)) * _acceleration * Time.fixedDeltaTime;
        _currentVelocity = Mathf.Clamp(_currentVelocity, _maxMinVelocity.x, _maxMinVelocity.y);
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

    private void OnDestroy()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
    }
}
