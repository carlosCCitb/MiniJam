using UnityEngine;
public enum Sign
{
    ClockWise,
    CounterClockWise
}
public class TangentMovement : EnemyMovement
{
    [SerializeField] private Sign _sign;
    private EnemySO _enemySO;
    private void Awake()
    {
        Inicialize();
        _enemySO = _enemyController.GetEnemySo();
        _speed = _enemySO.TangentSpeed;
        _sign = _enemySO.Sign;
    }
    private void Update()
    {
        Direction = (_enemyController.Target.position - transform.position).normalized;
        Direction = _sign == Sign.ClockWise ? new Vector2(Direction.y, -Direction.x) : new Vector2(-Direction.y, Direction.x);
        Direction *= _speed;
    }
}
