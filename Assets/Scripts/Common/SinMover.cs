using UnityEngine;

public class SinMover : MonoBehaviour
{
    [SerializeField] private float _offset, _speed;

    private void Update()
    {
        transform.position += _speed * Mathf.Sin(Time.time + _offset) * 0.25f * Time.deltaTime * Vector3.one;
    }
}
