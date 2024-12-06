using NaughtyAttributes;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;



    [SerializeField] private AudioSourcePool _audioSourcePool;
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private ParticlePool _particlePool;

    public AudioSourcePool AudioSourcePool => _audioSourcePool;
    public BulletPool BulletPool => _bulletPool;
    public EnemyPool EnemyPool => _enemyPool;
    public ParticlePool ParticlePool => _particlePool;



    private void Awake()
    {
        // Singleton
        if (_instance != this && _instance != null)
            Destroy(gameObject);

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
