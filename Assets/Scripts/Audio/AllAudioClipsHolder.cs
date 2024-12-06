using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "AllAudioClipsHolder", menuName = "ScriptableObjects/Audio/AllAudioClipsHolder", order = 0)]
public class AllAudioClipsHolder : ScriptableObject
{
    [Space, SerializeField] private SerializedDictionary<AudioClipConfiguration.Type, AudioClip> _audioClips;

    public bool TryGetValue(AudioClipConfiguration.Type type, out AudioClip audioClip) => _audioClips.TryGetValue(type, out audioClip);
}
