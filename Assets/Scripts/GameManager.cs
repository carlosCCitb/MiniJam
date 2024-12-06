using NaughtyAttributes;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;



    [SerializeField] private AudioSourcePool _audioSourcePool;
    [SerializeField] private BulletPool _bulletPool;

    public AudioSourcePool AudioSourcePool => _audioSourcePool;
    public BulletPool BulletPool => _bulletPool;



    private void Awake()
    {
        // Singleton
        if (_instance != this && _instance != null)
            Destroy(gameObject);

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
