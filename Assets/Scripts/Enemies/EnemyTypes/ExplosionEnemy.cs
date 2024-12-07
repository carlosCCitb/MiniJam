using UnityEngine;

public class ExplosionEnemy : EnemyController
{
    [SerializeField] private LayerMask _layersToApplyExplosionForces;

    public override void DeadBehaviour()
    {
        //RequestDespawn();
    }

    private void FixedUpdate()
    {
        _currentMovementState.OnStateUpdate(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DoDamage(collision.rigidbody);
        ApplyExplosionForces();

        GoToState<DeathState>();
        DeadBehaviour();
    }

    private void ApplyExplosionForces()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _enemySO.Range, _layersToApplyExplosionForces);

        foreach (var collider in colliders)
            collider.attachedRigidbody.AddForce((collider.attachedRigidbody.position - (Vector2)transform.position).normalized * 1000, ForceMode2D.Impulse);
    }
}
