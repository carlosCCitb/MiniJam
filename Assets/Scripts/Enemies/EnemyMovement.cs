using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    protected EnemyController _enemyController;
    [SerializeField] protected float _speed;
    protected Rigidbody2D _rigidBody;
    protected Vector2 _direction;
    private void Awake()
    {
        _enemyController = GetComponent<EnemyController>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }
}
