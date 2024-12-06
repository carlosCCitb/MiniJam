using UnityEngine;

public class RadialMovement : EnemyMovement
{
    private void Awake()
    {
        _speed = _enemyController.GetEnemySo().ChaseSpeed;
    }
    public void MoveToPlayer()
    {
        _direction = (_enemyController.Target.position - transform.position).normalized;
        _rigidBody.linearVelocity = _direction * _speed;
    }
    public void RunFromPlayer()
    {
        _direction = -(_enemyController.Target.position - transform.position).normalized;
        _rigidBody.linearVelocity = _direction * _speed;
    }
    public void Stop()
    {
        _rigidBody.linearVelocity = Vector2.zero; 
    }
    public void MoveWithInercy()
    {

    }
}
