using UnityEngine;
public class ExplosionEnemy : EnemyController
{
    [SerializeField] private LayerMask _layersToApplyExplosionForces;
    private void Awake()
    {
        TargetToHit = Target;
        AttackRange = _enemySO.Range;
    }
    private void LateUpdate()
    {
        Quaternion lookRotation = Quaternion.Euler(new Vector3(0f, 0f, AngleBetweenPoints(transform.position, Target.position) + 90.0f));
        transform.rotation = lookRotation;

        _spriteRenderer.flipX = Target.position.x > transform.position.x;

        float AngleBetweenPoints(Vector2 a, Vector2 b) => Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void FixedUpdate()
    {
        _currentMovementState.OnStateUpdate(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            GameManager.Instance.ParticlePool.Spawn(ParticleConfiguration.Type.Explosion, transform.position);

            DoDamage(collision.rigidbody);
            ApplyExplosionForces();

            GoToState<DeathState>();
        }
    }

    private void ApplyExplosionForces()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _enemySO.Range, _layersToApplyExplosionForces);

        foreach (var collider in colliders)
            collider.attachedRigidbody.AddForce((collider.attachedRigidbody.position - (Vector2)transform.position).normalized * 1000, ForceMode2D.Impulse);
    }
}
