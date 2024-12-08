using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class RangedEnemy : EnemyController
{
    [SerializeField] private Shooter _shooter;
    CancellationTokenSource _cancellationTokenSource = new();
    [SerializeField] private Transform _transformToInvert;
    [SerializeField] private Animator _animator;

    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Material _waterMaterial;

    private void OnEnable()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new();
        AttackRange = _enemySO.Range + _enemySO.Offset;
        ShootAsync().Forget();

        if (WaterLimit.WaterDepths)
            ChangeParticleWater();

        WaterLimit.GoDeep += ChangeParticleWater;
    }

    private void OnDisable()
    {
        WaterLimit.GoDeep -= ChangeParticleWater;

        _cancellationTokenSource?.Cancel();
    }

    private void FixedUpdate()
    {
        _currentMovementState.OnStateUpdate(this);
        float DistanceToPlayer = (Target.transform.position - transform.position).magnitude;
        if (DistanceToPlayer > _enemySO.Range + _enemySO.Offset)
        {
            GoToMovingState<ChaseState>();
        }
        else if (DistanceToPlayer < _enemySO.Range - _enemySO.Offset)
        {
            GoToMovingState<RunState>();
        }
        else           
            GoToMovingState<OrbitState>();
    }
    private async UniTaskVoid ShootAsync()
    {
        await UniTask.WaitUntil(() => Vector3.Distance(transform.position, Target.position) <= AttackRange, cancellationToken: _cancellationTokenSource.Token);

        while (enabled)
        {
            await UniTask.WaitUntil(() => _currentMovementState is not ChaseState, cancellationToken: _cancellationTokenSource.Token);
            _animator.SetTrigger("Attack");
            _shooter.Shoot(_enemySO.BulletType, Target.position, transform.position, (Target.position - transform.position).normalized * _enemySO.BulletSpeed, _enemySO.Damage);
            await UniTask.Delay(TimeSpan.FromSeconds(_enemySO.Coldown), cancellationToken: _cancellationTokenSource.Token);
        }
    }

    private void ChangeParticleWater()
    {
        if (_particleSystem.TryGetComponent(out ParticleSystemRenderer value))
            value.sharedMaterial = _waterMaterial;
        else
            _particleSystem.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _cancellationTokenSource?.Dispose();
    }
}
