using UnityEngine;
using System.Collections.Generic;
using System;
using NaughtyAttributes;
using Cysharp.Threading.Tasks;
using System.Threading;

public abstract class EnemyController : MonoBehaviour, Pool<EnemyController, EnemyController.Type, EnemyController>.IPoolable, IDamageable
{
    [Serializable]
    public enum Type
    {
        Ranged = 0,
        Melee = 1,
        Exploding = 2
    }
    [SerializeField] private AudioClipConfiguration _onDieSound1;
    [SerializeField] private AudioClipConfiguration _onDieRandom;
    [SerializeField] private Type _type;
    [SerializeField] private Vector3 _nextDestination;
    [SerializeField] private Stack<Vector3> _destinationPoints;
    [SerializeField] protected NormalStates _currentState;
    [SerializeField] protected MovementStatesSO _currentMovementState;
    [SerializeField] protected EnemySO _enemySO;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    public Transform Target;
    public Transform TargetToHit;
    [SerializeField] private Color RedColor;
    [SerializeField] private Collider2D _collider;
    public float AttackRange;
    [Space, SerializeField, ReadOnly] private float _currentHealth;

    protected bool IsDead => _currentHealth == 0;
    [SerializeField] private float _deathTime;

    public Type PoolableType => _type;

    public GameObject PoolableGameObject => gameObject;

    public event Action<EnemyController> OnPoolableDespawnNeeded;
    private CancellationTokenSource _cancellationTokenSource;
    private CancellationTokenSource _cancellationTokenSource2;
    private void Update()
    {
        _currentState.OnStateUpdate(this);
    }
    public void Initialize(EnemySO enemySO, Transform targetFollow, Transform targetHit)
    {
        Target = targetFollow;
        TargetToHit = targetHit;
        _enemySO = enemySO;
        _collider.enabled = true;
        _currentHealth = _enemySO.Health;
        transform.localScale = new Vector3(1, 1, 1);
        _spriteRenderer.color = Color.white;
        GoToMovingState<ChaseState>();
        GoToState<FallingState>();
    }
    public EnemySO GetEnemySo()
    {
        return _enemySO;
    }
    public void SetNextDirection()
    {

    }
    public void DestinationReached()
    {

    }
    public void SetLookAt(AnimationController aController)
    {

    }
    public void DeadBehaviour()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new();
        _collider.enabled = false;
        _spriteRenderer.color = Color.white;
        DeathAsync().Forget();
        GoToMovingState<InerceState>();
    }

    public void GoToState<T>() where T : NormalStates
    {
        if (_currentState.StatesToGo.Find(obj => obj is T))
        {
            _currentState.OnStateExit(this);
            _currentState = _currentState.StatesToGo.Find(obj => obj is T);
            _currentState.OnStateEnter(this);
        }
    }
    public void GoToMovingState<T>() where T : MovementStatesSO
    {
        if (_currentMovementState.MovementStatesToGo.Find(obj => obj is T))
        {
            _currentMovementState.OnStateExit(this);
            _currentMovementState = _currentMovementState.MovementStatesToGo.Find(obj => obj is T);
            _currentMovementState.OnStateEnter(this);
        }
    }
    protected virtual void RequestDespawn()
    {
        OnPoolableDespawnNeeded?.Invoke(this);
    }
    protected void DoDamage(Rigidbody2D rigidbody)
    {
        if (rigidbody.TryGetComponent(out IDamageable damageable))
            damageable.OnHurt(_enemySO.Damage);
    }
    [Button]
    public void Hurty()
    {
        _cancellationTokenSource2?.Cancel();
        _cancellationTokenSource2 = new();
        OnHurtAsync().Forget();
    }
    public void OnHurt(int Damage)
    {
        _currentHealth -= Damage;
        Mathf.Max(_currentHealth, 0);
        _cancellationTokenSource2?.Cancel();
        _cancellationTokenSource2 = new();
        OnHurtAsync().Forget();
        if (_currentHealth == 0)
            OnDie();
    }
    public void OnDie()
    {
        GoToState<DeathState>();
        float random = UnityEngine.Random.Range(0, 100);
        if (random != 0)
            _onDieSound1.Play();
        else
            _onDieRandom.Play();
    }
    private async UniTaskVoid DeathAsync()
    {
        int times = 30;
        float timePart = _deathTime / (float)times;
        for (int i = 0; i < times; i++)
        {
            var factor = i * timePart / _deathTime;
            _spriteRenderer.color = Color.Lerp(Color.white, Color.black, factor);
            transform.localScale = Vector3.Lerp(new Vector3(1, 1, 1), Vector3.zero, factor);
            await UniTask.WaitForSeconds(timePart, cancellationToken: _cancellationTokenSource.Token);
        }

        RequestDespawn();
    }
    private async UniTaskVoid OnHurtAsync()
    {
        float blinkDuration = 0.16f;
        float time = 0;
        int Times = 2;
        for(int i=0; i< Times; i++)
        {
            time = 0;
            while(time < blinkDuration)
            {
                _spriteRenderer.color = Color.Lerp(Color.white, RedColor, time/blinkDuration);
                await UniTask.Yield(cancellationToken: _cancellationTokenSource2.Token);
                time += Time.deltaTime;
            }
            time = 0;
            while (time < blinkDuration)
            {
                _spriteRenderer.color = Color.Lerp(RedColor, Color.white, time/blinkDuration);
                await UniTask.Yield(cancellationToken: _cancellationTokenSource2.Token);
                time += Time.deltaTime;
            }
            _spriteRenderer.color = Color.white;
        }
        RequestDespawn();
    }
    private void OnDestroy()
    {
        _cancellationTokenSource?.Dispose();
        _cancellationTokenSource2?.Dispose();
    }
    private void OnDisable()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource2?.Cancel();
    }
}
