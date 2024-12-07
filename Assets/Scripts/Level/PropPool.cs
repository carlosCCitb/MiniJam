using UnityEngine;

public class PropPool : Pool<Prop, Prop.Type, Prop[]>
{
    public void Spawn(Prop.Type type, Vector3 position)
    {
        SpawnAndGetObject(type, position, Quaternion.identity);
    }

    protected override Prop InstantiateFromValue(Prop[] value)
    {
        return Instantiate(value[Random.Range(0, value.Length)]);
    }

    protected override void OnActivationManaging(Prop obj, params object[] args)
    {
        obj.Initialize();
    }

    protected override void OnDespawnManaging(Prop obj)
    { }

    protected override void OnInstantiationManaging(Prop obj, params object[] args)
    { }
}
