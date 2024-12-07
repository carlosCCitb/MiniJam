using NaughtyAttributes;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Prop : MonoBehaviour, Pool<Prop, Prop.Type, Prop[]>.IPoolable
{
    [Serializable]
    public enum Type
    { 
        Cloud = 0,
        Satellite = 1
    }

    [SerializeField] private Type _type;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Space, SerializeField] private Color _colorInFront;
    [SerializeField] private Color _colorInBack;

    [Space, SerializeField] private float _scaleInFront;
    [SerializeField] private float _scaleInBack;

    [Space, SerializeField] private bool _canFlip;
    [SerializeField] private bool _shouldRotate;
    [ShowIf(nameof(_shouldRotate)), SerializeField] private Vector2 _minMaxAngularRotation;
    [SerializeField] private float _maxVerticalPositionToDespawn;

    private bool _clockwise;
    private float _angularRotation;

    public Type PoolableType => _type;

    public GameObject PoolableGameObject => gameObject;

    public event Action<Prop> OnPoolableDespawnNeeded;

    public static float Speed = 1.0f;

    private void Update()
    {
        transform.position += Speed * Time.deltaTime * Vector3.up;

        if (_shouldRotate)
            transform.rotation *= Quaternion.Euler(0, 0, _angularRotation * Time.deltaTime * (_clockwise ? 1 : -1));

        if (transform.position.y > _maxVerticalPositionToDespawn)
            OnPoolableDespawnNeeded?.Invoke(this);
    }

    public void Initialize()
    {
        _clockwise = Random.value < 0.5f;
        _angularRotation = Random.Range(_minMaxAngularRotation.x, _minMaxAngularRotation.y);

        if (_canFlip)
            _spriteRenderer.flipX = Random.value < 0.5f;

        if (Random.value < 0.5f)
        {
            transform.localScale = Vector3.one * _scaleInFront;
            _spriteRenderer.color = _colorInFront;
            _spriteRenderer.sortingOrder = 50;
        }
        else
        {
            transform.localScale = Vector3.one * _scaleInBack;
            _spriteRenderer.color = _colorInBack;
            _spriteRenderer.sortingOrder = -50;
        }
    }
}
