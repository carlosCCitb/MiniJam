using UnityEngine;

public class AudioSourcePool : Pool<AudioPlayer, DontClassify, GameObject>
{
    [SerializeField] private AllAudioClipsHolder _allAudioClipsHolder;

    public bool TrySpawn(AudioClipConfiguration config, float time, Transform transformToAttach)
    {
        if (!_allAudioClipsHolder.TryGetValue(config.ClipType, out AudioClip clip))
            return false;

        SpawnAndGetObject(DontClassify.Generic, transformToAttach ? transformToAttach.position : Vector3.zero, Quaternion.identity)
            .Play(config, clip, time, transformToAttach);

        return true;
    }

    protected override AudioPlayer InstantiateFromValue(GameObject value)
    {
        return Instantiate(value).GetComponent<AudioPlayer>();
    }

    protected override void OnActivationManaging(AudioPlayer obj, params object[] args)
    { }

    protected override void OnDespawnManaging(AudioPlayer obj)
    { }

    protected override void OnInstantiationManaging(AudioPlayer obj, params object[] args)
    { }
}
