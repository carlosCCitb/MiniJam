using NaughtyAttributes;
using UnityEngine;

public class MeteoriteMovement : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private PlayerInput _playerInput;

    [Space, SerializeField] private float _acceleration;
    [SerializeField] private Vector2 _maxMinVelocity;

    [ShowNonSerializedField, ReadOnly] private float _currentVelocity;
    [ShowNonSerializedField, ReadOnly] private Vector2 _inputVelocity;

    private Vector2 _shootingTarget;

    private void OnEnable()
    {
        _playerInput.OnInputMove += OnInputMoveChanged;
        _playerInput.OnInputMouse += OnMousePositionChanged;
    }

    private void OnDisable()
    {
        _playerInput.OnInputMove -= OnInputMoveChanged;
        _playerInput.OnInputMouse -= OnMousePositionChanged;
    }

    private void FixedUpdate()
    {   
        _rigidbody.AddForce(_acceleration * _rigidbody.mass * _inputVelocity / (Mathf.Max(Mathf.Abs(_currentVelocity), 1000.0f) * 0.01f), ForceMode2D.Force);
        
        _currentVelocity -= (_inputVelocity.y + (_camera.transform.position.y - _rigidbody.position.y)) * _acceleration * Time.fixedDeltaTime;
        _currentVelocity = Mathf.Clamp(_currentVelocity, _maxMinVelocity.x, _maxMinVelocity.y);
    }

    private void OnInputMoveChanged(Vector2 input)
    { 
        _inputVelocity = input;
    }

    private void OnMousePositionChanged(Vector2 mousePosition)
    { 
        _shootingTarget = mousePosition;
    }
}
