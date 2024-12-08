using UnityEngine;
using UnityEngine.InputSystem;

public class Cursor : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _enemy, _noEnemy;
    [SerializeField] private int _enemyLayer;
    private void Awake()
    {
        _spriteRenderer.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        //Vector3 mousepos = Camera.main.Scre
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);

        // If it hits something...
        if (hit.collider.gameObject.layer == _enemyLayer)
        {

        }
    }
}
