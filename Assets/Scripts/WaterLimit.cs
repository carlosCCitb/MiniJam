using UnityEngine;
using System;
public class WaterLimit : MonoBehaviour
{
    public static event Action GoDeep = delegate { };
    public static bool WaterDepths = false;
    private Collider2D _collider;

    public void Initialize()
    {
        WaterDepths = false;
        _collider.enabled = true;
    }
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        Initialize();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.gameObject.layer == 8)
        {
            GoDeep?.Invoke();
            WaterDepths = true;
            _collider.enabled = false;
        }
    }
}
