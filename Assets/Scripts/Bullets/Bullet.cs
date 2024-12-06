using System;
using UnityEngine;

public class Bullet : MonoBehaviour, Pool<Bullet, Bullet.Type, Bullet>.IPoolable
{
    [Serializable]
    public enum Type
    {
        Standard = 0
    }

    [SerializeField] private Type _type;
    [SerializeField] private Rigidbody2D _rigidbody;

    [SerializeField] private float _maxDistanceToOrigin;

    private int _damage;

    public Type PoolableType => _type;
    public GameObject PoolableGameObject => gameObject;

    public event Action<Bullet> OnPoolableDespawnNeeded;

    protected virtual void Update()
    {
        if (transform.position.magnitude > _maxDistanceToOrigin)
            RequestDespawn();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody.TryGetComponent(out IDamageable damageable))
        {
            damageable.OnHurt(_damage);

            if (damageable is Meteorite)
                GameManager.Instance.ParticlePool.Spawn(ParticleConfiguration.Type.HitRock, collision.contacts[0].point);
        }

        RequestDespawn();
    }

    public void SetValues(Vector3 velocity, int damage)
    {
        _rigidbody.linearVelocity = velocity;
        _damage = damage;
    }

    protected virtual void RequestDespawn() => OnPoolableDespawnNeeded?.Invoke(this);

    private void OnValidate()
    {
        _maxDistanceToOrigin = Mathf.Max(_maxDistanceToOrigin, 5.0f);
    }
}
