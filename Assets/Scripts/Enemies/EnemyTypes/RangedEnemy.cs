using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class RangedEnemy : EnemyController
{
    [SerializeField] private Shooter _shooter;
    CancellationTokenSource _cancellationTokenSource = new();
    [SerializeField] private Transform _transformToInvert;
    [SerializeField] private Animator _animator;
    private void OnEnable()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new();
        AttackRange = _enemySO.Range + _enemySO.Offset;
        ShootAsync().Forget();
    }

    private void OnDisable()
    {
        _cancellationTokenSource?.Cancel();
    }
    /*private void LateUpdate()
    {
        Quaternion lookRotation = Quaternion.Euler(new Vector3(0f, 0f, AngleBetweenPoints(transform.position, Target.position) + 90.0f));
        transform.rotation = lookRotation;

        _spriteRenderer.flipX = Target.position.x > transform.position.x;

        float AngleBetweenPoints(Vector2 a, Vector2 b) => Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }*/

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
        await UniTask.WaitUntil(() => Vector3.Distance(transform.position, Target.position) <= _enemySO.Range, cancellationToken: _cancellationTokenSource.Token);

        while (enabled)
        {
            await UniTask.WaitUntil(() => _currentMovementState is not ChaseState, cancellationToken: _cancellationTokenSource.Token);
            _animator.SetTrigger("Attack");
            _shooter.Shoot(_enemySO.BulletType, Target.position, transform.position, (Target.position - transform.position).normalized * _enemySO.BulletSpeed, _enemySO.Damage);
            await UniTask.Delay(TimeSpan.FromSeconds(_enemySO.Coldown), cancellationToken: _cancellationTokenSource.Token);
        }
    }
    private void OnDestroy()
    {
        _cancellationTokenSource?.Dispose();
    }
}
