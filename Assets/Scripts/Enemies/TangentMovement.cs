using UnityEngine;

public class TangentMovement : EnemyMovement
{
    private void Awake()
    {
        _speed = _enemyController.GetEnemySo().TangentSpeed;
    }
    private void Update()
    {
        _direction = (_enemyController.Target.position - transform.position).normalized;
    }
}
