using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class RangedEnemy : EnemyController
{
    [SerializeField] private Shooter _shooter;

    CancellationTokenSource _cancellationTokenSource = new();

    private void OnEnable()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new();

        ShootAsync().Forget();
    }

    private void OnDisable()
    {
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
    public override void DeadBehaviour()
    {
        GoToState<DeathState>();
        GoToMovingState<InerceState>();
    }

    private async UniTaskVoid ShootAsync()
    {
        await UniTask.WaitUntil(() => Vector3.Distance(transform.position, Target.position) <= _enemySO.Range, cancellationToken: _cancellationTokenSource.Token);

        while (enabled)
        {
            await UniTask.WaitUntil(() => _currentMovementState is not ChaseState, cancellationToken: _cancellationTokenSource.Token);

            _shooter.Shoot(_enemySO.BulletType, Target.position, transform.position, (Target.position - transform.position).normalized * _enemySO.BulletSpeed, _enemySO.Damage);
            await UniTask.Delay(TimeSpan.FromSeconds(_enemySO.Coldown), cancellationToken: _cancellationTokenSource.Token);
        }
    }

    private void OnDestroy()
    {
        _cancellationTokenSource?.Dispose();
    }
}
