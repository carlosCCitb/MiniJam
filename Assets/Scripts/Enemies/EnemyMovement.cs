using UnityEngine;

public abstract class EnemyMovement : MonoBehaviour
{
    protected EnemyController _enemyController;
    [SerializeField] protected float _speed;
    protected Rigidbody2D _rigidBody;
    public Vector2 Direction;
    protected void Inicialize()
    {
        _enemyController = GetComponent<EnemyController>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }
}
