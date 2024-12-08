using UnityEngine;
using System;

public class WaterSound : MonoBehaviour
{
    public static event Action PlayWaterSplash = delegate { };
    private Collider2D _collider;

    public void Initialize()
    {
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
            _collider.enabled = false;
            PlayWaterSplash?.Invoke();
        }
    }
}
