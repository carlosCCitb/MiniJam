using UnityEngine;
using System.Collections.Generic;
public class DragMeteoriteBehaviour : MonoBehaviour
{
    public List<GameObject> MeteorSkins;
    [SerializeField] private int _currentSkin;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private List<float> ShapeValues;
    private float _initialRateOverTime;
    private ParticleSystem.ShapeModule shape;
    private ParticleSystem.EmissionModule emission;
    [SerializeField] private float rateMultiplier;
    [SerializeField] private AudioClipConfiguration _demolishRock1;
    [SerializeField] private AudioSource _audioSource;
    private float _startVolume;
    [SerializeField] [Range(0f, 0.2f)] float _volumeAttenuation;
    public int GetCurrentSkin()
    {
        return _currentSkin;
    }
    private void Awake()
    {
        _currentSkin = 0;
        foreach (GameObject meteor in MeteorSkins)
            meteor.SetActive(false);
        MeteorSkins[0].SetActive(true);
        MeteorSkins[5].SetActive(true);
        shape = particles.shape;
        emission = particles.emission;
        shape.radius = ShapeValues[0];
        _initialRateOverTime = emission.rateOverTime.constant;
        _startVolume = _audioSource.volume;
    }
    public void ChangeSkin(int i)
    {
        if (_currentSkin == i)
            return;
        if (_currentSkin < 3)
            _demolishRock1.Play();
        _audioSource.volume = _startVolume - i * _volumeAttenuation; 
        MeteorSkins[_currentSkin].SetActive(false);
        if(_currentSkin+5 < MeteorSkins.Count)
            MeteorSkins[_currentSkin + 5].SetActive(false);

        _currentSkin = i;

        MeteorSkins[_currentSkin].SetActive(true);
        if (_currentSkin + 5 < MeteorSkins.Count)
            MeteorSkins[_currentSkin + 5].SetActive(true);
        AdjustParticlesToShape(i);
        
    }
    public void AdjustParticlesToShape(int i)
    {
        shape.radius = ShapeValues[i];
        emission.rateOverTime = _initialRateOverTime - _initialRateOverTime* rateMultiplier * i;
    }
}
