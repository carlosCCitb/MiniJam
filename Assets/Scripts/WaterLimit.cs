using UnityEngine;
using System;
public class WaterLimit : MonoBehaviour
{
    public static event Action GoDeep = delegate { };
    public static bool WaterDepths = false;
    private Collider2D _collider;

    public void Inicialize()
    {
        WaterDepths = false;
        _collider.enabled = true;
    }
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        Inicialize();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GoDeep.Invoke();
        WaterDepths = true;
        _collider.enabled = false;
    }
}
