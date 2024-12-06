using NaughtyAttributes;
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioClipConfiguration", menuName = "ScriptableObjects/Audio/AudioClipConfiguration", order = 1)]
public class AudioClipConfiguration : ScriptableObject
{
    [Serializable]
    public enum Type
    {
        None = 0
    }

    public enum Space
    { 
        Sound2D,
        Sound3D
    }

    [Space, SerializeField] private Type _type;
    [SerializeField] private AudioMixerGroup _group;

    [Space, SerializeField] private bool _loop;
    [SerializeField, Range(0.0f, 1.0f)] private float _volume = 0.5f;

    [Space, SerializeField] private Space _space;
    [HideIf(nameof(Is3DSound)), SerializeField, Range(-1.0f, 1.0f)] private float _stereoPan; // -1.0f is only left ear / 1.0f is only right ear
    [ShowIf(nameof(Is3DSound)), SerializeField] private Vector2 _minMaxDistance = new(1f, 500f);

    [Space, SerializeField] private bool _shouldVaryPitch;
    [ShowIf(nameof(_shouldVaryPitch)), SerializeField, MinMaxSlider(-3.0f, 3.0f)] private Vector2 _minMaxPitch = new(0.9f, 1.1f);

    public Type ClipType => _type;
    public AudioMixerGroup Group => _group;

    public bool Loop => _loop;
    public float Volume => _volume;

    public bool Is3DSound => _space == Space.Sound3D;
    public float StereoPan => _stereoPan;
    public Vector2 MinMaxDistance => _minMaxDistance;
    public float Pitch => _shouldVaryPitch ? UnityEngine.Random.Range(_minMaxPitch.x, _minMaxPitch.y) : 1.0f;


    [Button("Play Clip (Only Play Mode)")]
    public bool Play(Transform transformToAttach = null)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
            return false;
#endif
        return GameManager.Instance.AudioSourcePool.TrySpawn(this, 0.0f, transformToAttach);
    }

    public bool PlayAt(float time, Transform transformToAttach = null) => GameManager.Instance.AudioSourcePool.TrySpawn(this, time, transformToAttach);



#if UNITY_EDITOR
    [Button("Play Clip (Only Editor Mode)")]
    private void PlayClip()
    {
        if (Application.isPlaying)
            return;

        if (!AssetDatabase.LoadAssetAtPath<AllAudioClipsHolder>("Assets\\ScriptableObjects\\Audio\\AllAudioClipsHolder.asset").TryGetValue(_type, out AudioClip clip))
            return;

        typeof(AudioImporter).Assembly
            .GetType("UnityEditor.AudioUtil")
            .GetMethod(
                "PlayPreviewClip",
                BindingFlags.Static | BindingFlags.Public,
                null,
                new System.Type[] { typeof(AudioClip), typeof(int), typeof(bool) },
                null
            )
            .Invoke(
                null,
                new object[] { clip, 0, false }
            );
    }

    [Button("Stop All Clips (Only Editor Mode)")]
    private void StopAllClips()
    {
        if (Application.isPlaying)
            return;

        typeof(AudioImporter).Assembly
            .GetType("UnityEditor.AudioUtil")
            .GetMethod(
                "StopAllPreviewClips",
                BindingFlags.Static | BindingFlags.Public,
                null,
                new System.Type[] { },
                null
            )
            .Invoke(
                null,
                new object[] { }
            );
    }

    [Button("Rename to Audio_nameofType (Only Editor Mode)")]
    private void Rename()
    {
        if (Application.isPlaying)
            return;

        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, $"Audio_{_type}");
        AssetDatabase.SaveAssets();
    }

#endif
}
