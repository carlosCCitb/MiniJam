using NaughtyAttributes;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;



    [SerializeField] private AudioSourcePool _audioSourcePool;
    public AudioSourcePool AudioSourcePool => _audioSourcePool;



    private void Awake()
    {
        // Singleton
        if (_instance != this && _instance != null)
            Destroy(gameObject);

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}