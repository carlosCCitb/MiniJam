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
    [BoxGroup("Background values"), SerializeField] private Color _initialColor;
    [BoxGroup("Background values"), SerializeField] private Color _redColor;

    [Space]
    [BoxGroup("UI"), SerializeField] private RectTransform _pointerTransform;
    [BoxGroup("UI"), SerializeField] private RectTransform _progressBarTransform;

    public void AddDisplacement(float currentSpeed, Vector2 minMaxSpeed, float dt)
    {
        float factor = 1.0f - Mathf.InverseLerp(minMaxSpeed.x, minMaxSpeed.y, currentSpeed);
        float speed = Mathf.Lerp(_minMaxMeteoriteSpeed.x, _minMaxMeteoriteSpeed.y, factor);

        _currentDistance += speed * dt;

        _backgroundTransform.position = new(_backgroundTransform.position.x, _backgroundTotalDisplacement * (_currentDistance / _totalDistanceToTravel), _backgroundTransform.position.z);

        ApplyMountainDisplacement();
        ApplyMountainColoring();

        _pointerTransform.position = new(_pointerTransform.position.x, Mathf.Lerp(_progressBarTransform.position.y + _progressBarTransform.rect.max.y, _progressBarTransform.position.y + _progressBarTransform.rect.min.y, _currentDistance / _totalDistanceToTravel), _pointerTransform.position.z);
    }

    private void ApplyMountainDisplacement()
    {
        // This is hardcoded

        float parentPositionY = _mountains[0].transform.parent.position.y;

        if (parentPositionY > -35 && parentPositionY < 7.5f)
        {
            float factor = Mathf.InverseLerp(-35.0f, 0, parentPositionY);
            _clouds.transform.localPosition = new(_clouds.transform.localPosition.x, Mathf.Lerp(40.0f, 7.5f, factor), _clouds.transform.localPosition.z);
        }

        if (parentPositionY > -27.5f && parentPositionY < 0)
        {
            float factor = Mathf.InverseLerp(-27.5f, 0, parentPositionY);

            _mountains[0].transform.localPosition = new(_mountains[0].transform.localPosition.x, Mathf.Lerp(23.0f, 0.0f, factor), _mountains[0].transform.localPosition.z);
            _mountains[1].transform.localPosition = new(_mountains[1].transform.localPosition.x, Mathf.Lerp(20.5f, 0.0f, factor), _mountains[1].transform.localPosition.z);
            _mountains[2].transform.localPosition = new(_mountains[2].transform.localPosition.x, Mathf.Lerp(15.5f, 0.0f, factor), _mountains[2].transform.localPosition.z);
            _mountains[3].transform.localPosition = new(_mountains[2].transform.localPosition.x, Mathf.Lerp(10.5f, 0.0f, factor), _mountains[2].transform.localPosition.z);
        }
    }

    private void ApplyMountainColoring()
    {
        // This is hardcoded

        if (_clouds.transform.position.y > -10 && _clouds.transform.position.y < 2.5)
        {
            float factor = Mathf.InverseLerp(-10, 2.5f, _clouds.transform.position.y);
            _clouds.color = Color.Lerp(_initialColor, _redColor, factor);
        }

        foreach (var mountain in _mountains)
            if (mountain.transform.position.y > -10 && mountain.transform.position.y < 5)
            {
                float factor = Mathf.InverseLerp(-10, 5, mountain.transform.position.y);
                mountain.color = Color.Lerp(Color.white, _redColor, factor);
            }
    }
}
