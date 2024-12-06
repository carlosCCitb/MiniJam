using NaughtyAttributes;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [SerializeField] private float _totalDistanceToTravel; // m
    [SerializeField, ReadOnly] private float _currentDistance; // m

    [Space, SerializeField] private Vector2 _minMaxMeteoriteSpeed; // m/s

    [Space]
    [BoxGroup("Background values"), SerializeField] private Transform _backgroundTransform;
    [BoxGroup("Background values"), SerializeField] private float _backgroundTotalDisplacement;
    [BoxGroup("Background values"), SerializeField] private Transform[] _mountains;

    public void AddDisplacement(float currentSpeed, Vector2 minMaxSpeed, float dt)
    {
        float factor = 1.0f - Mathf.InverseLerp(minMaxSpeed.x, minMaxSpeed.y, currentSpeed);
        float speed = Mathf.Lerp(_minMaxMeteoriteSpeed.x, _minMaxMeteoriteSpeed.y, factor);

        _currentDistance += speed * dt;

        _backgroundTransform.position = new(_backgroundTransform.position.x, _backgroundTotalDisplacement * (_currentDistance / _totalDistanceToTravel), _backgroundTransform.position.z);

        ApplyMountainDisplacement();
    }

    private void ApplyMountainDisplacement()
    {
        // This is hardcoded

        float parentPositionY = _mountains[0].transform.parent.position.y;

        if (parentPositionY > -20 && parentPositionY < 0)
        {
            float factor = Mathf.InverseLerp(-20, 0, parentPositionY);

            _mountains[0].localPosition = new(_mountains[0].localPosition.x, Mathf.Lerp(12.5f, 0.0f, factor), _mountains[0].localPosition.z);
            _mountains[1].localPosition = new(_mountains[1].localPosition.x, Mathf.Lerp(10.0f, 0.0f, factor), _mountains[1].localPosition.z);
            _mountains[2].localPosition = new(_mountains[2].localPosition.x, Mathf.Lerp(5.0f, 0.0f, factor), _mountains[2].localPosition.z);
        }
    }
}
