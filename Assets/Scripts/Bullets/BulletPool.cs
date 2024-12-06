using UnityEngine;

public class BulletPool : Pool<Bullet, Bullet.Type, Bullet>
{
    public static int PlayerBulletLayer { private set; get; }
    public static int EnemyBulletLayer { private set; get; }

    private void Awake()
    {
        PlayerBulletLayer = LayerMask.NameToLayer("BulletPlayer");
        EnemyBulletLayer = LayerMask.NameToLayer("BulletEnemy");
    }

    public Bullet Spawn(Bullet.Type type, Vector3 pos, Quaternion rot, int layer)
    {
        return SpawnAndGetObject(type, pos, rot, layer);
    }

    protected override Bullet InstantiateFromValue(Bullet value)
    {
        return Instantiate(value);
    }

    protected override void OnActivationManaging(Bullet obj, params object[] args)
    {
        if (args[0] is int layer)
            obj.gameObject.layer = layer;
    }

    protected override void OnDespawnManaging(Bullet obj)
    { }

    protected override void OnInstantiationManaging(Bullet obj, params object[] args)
    { }
}
