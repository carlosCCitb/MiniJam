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
    [BoxGroup("Background values"), SerializeField] private SpriteRenderer[] _mountains;
    [BoxGroup("Background values"), SerializeField] private SpriteRenderer _clouds;
    [BoxGroup("Background values"), SerializeField] private Color _redColor;

    public void AddDisplacement(float currentSpeed, Vector2 minMaxSpeed, float dt)
    {
        float factor = 1.0f - Mathf.InverseLerp(minMaxSpeed.x, minMaxSpeed.y, currentSpeed);
        float speed = Mathf.Lerp(_minMaxMeteoriteSpeed.x, _minMaxMeteoriteSpeed.y, factor);

        _currentDistance += speed * dt;

        _backgroundTransform.position = new(_backgroundTransform.position.x, _backgroundTotalDisplacement * (_currentDistance / _totalDistanceToTravel), _backgroundTransform.position.z);

        ApplyMountainDisplacement();
        ApplyMountainColoring();
    }

    private void ApplyMountainDisplacement()
    {
        // This is hardcoded

        float parentPositionY = _mountains[0].transform.parent.position.y;

        if (parentPositionY > -30 && parentPositionY < 2.5)
        {
            float factor = Mathf.InverseLerp(-30, 2.5f, parentPositionY);
            _clouds.transform.localPosition = new(_clouds.transform.localPosition.x, Mathf.Lerp(30.0f, 2.5f, factor), _clouds.transform.localPosition.z);
        }

        if (parentPositionY > -20 && parentPositionY < 0)
        {
            float factor = Mathf.InverseLerp(-20, 0, parentPositionY);

            _mountains[0].transform.localPosition = new(_mountains[0].transform.localPosition.x, Mathf.Lerp(12.5f, 0.0f, factor), _mountains[0].transform.localPosition.z);
            _mountains[1].transform.localPosition = new(_mountains[1].transform.localPosition.x, Mathf.Lerp(10.0f, 0.0f, factor), _mountains[1].transform.localPosition.z);
            _mountains[2].transform.localPosition = new(_mountains[2].transform.localPosition.x, Mathf.Lerp(5.0f, 0.0f, factor), _mountains[2].transform.localPosition.z);
        }
    }

    private void ApplyMountainColoring()
    {
        // This is hardcoded

        if (_clouds.transform.position.y > -10 && _clouds.transform.position.y < 2.5)
        {
            float factor = Mathf.InverseLerp(-10, 2.5f, _clouds.transform.position.y);
            _clouds.color = Color.Lerp(Color.white, _redColor, factor);
        }

        foreach (var mountain in _mountains)
            if (mountain.transform.position.y > -10 && mountain.transform.position.y < 5)
            {
                float factor = Mathf.InverseLerp(-10, 5, mountain.transform.position.y);
                mountain.color = Color.Lerp(Color.white, _redColor, factor);
            }
    }
}
