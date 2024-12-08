using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;



    [SerializeField] private MainMenu _mainMenu;

    [Space, SerializeField] private SceneOperationsManager _sceneOperationsManager;
    [SerializeField] private AudioSourcePool _audioSourcePool;
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private ParticlePool _particlePool;
    [SerializeField] private PropPool _propPool;

    public SceneOperationsManager SceneOperationsManager => _sceneOperationsManager;
    public AudioSourcePool AudioSourcePool => _audioSourcePool;
    public BulletPool BulletPool => _bulletPool;
    public EnemyPool EnemyPool => _enemyPool;
    public ParticlePool ParticlePool => _particlePool;
    public PropPool PropPool => _propPool;



    private void Awake()
    {
        // Singleton
        if (_instance != this && _instance != null)
            Destroy(gameObject);

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (_mainMenu)
            _mainMenu.InitAudio();
        else
            Debug.LogWarning("GameManager should only be in MainMenuScene");
    }
}
