using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticlePool : Pool<ParticleConfiguration, ParticleConfiguration.Type, ParticleConfiguration>
{
    public void Spawn(ParticleConfiguration.Type type, Vector3 pos) => SpawnAndGetObject(type, pos, Quaternion.identity);

    protected override ParticleConfiguration InstantiateFromValue(ParticleConfiguration value)
    {
        return Instantiate(value);
    }

    protected override void OnActivationManaging(ParticleConfiguration obj, params object[] args)
    {
        obj.OnParticleSpawned().Forget();
    }

    protected override void OnInstantiationManaging(ParticleConfiguration obj, params object[] args)
    {
        // No additional steps required
    }

    protected override void OnDespawnManaging(ParticleConfiguration obj)
    {
        // No additional steps required
    }

    public List<Vector3> GetParticlesPositionsByType(ParticleConfiguration.Type type)
    {
        return spawnedObjects
            .Where(x => x.PoolableType == type && isActiveAndEnabled)
            .Select(x => x.transform.position)
            .ToList();
    }
}
