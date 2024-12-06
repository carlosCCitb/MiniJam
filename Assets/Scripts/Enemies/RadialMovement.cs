using UnityEngine;

public class RadialMovement : EnemyMovement
{
    private Vector2 _lastVelocity;
    private void Awake()
    {
        Inicialize();
        _speed = _enemyController.GetEnemySo().ChaseSpeed;
    }
    public void MoveToPlayer()
    {
        Direction = (_enemyController.Target.position - transform.position).normalized;
        _lastVelocity = Direction;
        Direction = Direction * _speed;
    }
    public void RunFromPlayer()
    {
        Direction = -(_enemyController.Target.position - transform.position).normalized;
        _lastVelocity = Direction;
        Direction = Direction * _speed;
    }
    public void Stop()
    {
        Direction = Vector2.zero; 
    }
    public void MoveWithInercy()
    {
        Direction = _lastVelocity * _speed;
    }
}
