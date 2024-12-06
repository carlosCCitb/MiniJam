using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody2D))]
public class ComposedMovement : MonoBehaviour
{
    [SerializeField] private TangentMovement _tangentMovement;
    [SerializeField] private RadialMovement _radialMovement;
    [SerializeField] private float _timeFactor;
    private Vector2 _lastVelocity;
    private Rigidbody2D _rigidBody;
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.gravityScale = 0f;
        _radialMovement = GetComponent<RadialMovement>();
        _tangentMovement = GetComponent<TangentMovement>();
    }
    void FixedUpdate()
    {
        _rigidBody.linearVelocity = _tangentMovement.Direction + _radialMovement.Direction;
    }
}
