using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using NaughtyAttributes;
public class DragMeteoriteBehaviour : MonoBehaviour
{
    public List<GameObject> MeteorSkins;
    [SerializeField] private float[] _colliderRadius;
    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private CircleCollider2D _particlesCollider;
    [SerializeField] private int _currentSkin;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private ParticleSystem particles2;
    [SerializeField] private ParticleSystem particlesLighting;
    [SerializeField] private List<float> ShapeValues;
    private float _initialRateOverTime;
    private ParticleSystem.ShapeModule shape;
    private ParticleSystem.EmissionModule emission;
    [SerializeField] private float rateMultiplier;


    [SerializeField] private AudioClipConfiguration _demolishRock1;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _audioSource2;
    private float _startVolume;
    [SerializeField] [Range(0f, 0.2f)] float _volumeAttenuation;
    [SerializeField] private AudioClipConfiguration _splashHigh;
    [SerializeField] private AudioClipConfiguration _splashMid;
    [SerializeField] private AudioClipConfiguration _splashLow;

    [SerializeField] private Material bubbleMaterial;
    public int GetCurrentSkin()
    {
        return _currentSkin;
    }
    private void OnEnable()
    {
        WaterLimit.GoDeep += ChangeToWaterParticles;
        WaterSound.PlayWaterSplash += OnSplash;
    }
    private void OnDisable()
    {
        WaterLimit.GoDeep -= ChangeToWaterParticles;
        WaterSound.PlayWaterSplash -= OnSplash;
    }
    [Button]
    public void OnSplash()
    {
        if (_currentSkin == 0)
            _splashHigh.Play();
        else if (_currentSkin < 3)
            _splashMid.Play();
        else
            _splashLow.Play();
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
        _audioSource.volume = 0.3f;
        _audioSource2.enabled = false;
        _startVolume = 0.3f;
        StartCoroutine(StartSound2());
    }
    private IEnumerator StartSound2()
    {
        yield return new WaitForSeconds(7f);
        _audioSource2.enabled = true;
        _audioSource.volume = 0.25f;
        _audioSource.volume = 0.25f;
        _startVolume = 0.25f;
    }
    [Button]
    public void ChangeVolume()
    {
        _audioSource.volume = 0.3f;
        _audioSource2.volume = 0.3f;
    }
    public void ChangeSkin(int i)
    {
        if (_currentSkin == i)
            return;

        _collider.radius = _colliderRadius[i];
        
        _audioSource.volume = _startVolume - i * _volumeAttenuation;
        _audioSource2.volume = _startVolume - i * _volumeAttenuation;

        if (_currentSkin < 3)
            _demolishRock1.Play();
        MeteorSkins[_currentSkin].SetActive(false);
        if(_currentSkin+5 < MeteorSkins.Count)
            MeteorSkins[_currentSkin + 5].SetActive(false);

        _currentSkin = i;
        particlesLighting.gameObject.SetActive(GetCurrentSkin() >= 3);

        MeteorSkins[_currentSkin].SetActive(true);
        if (_currentSkin + 5 < MeteorSkins.Count)
            MeteorSkins[_currentSkin + 5].SetActive(true);
        AdjustParticlesToShape(i);
        
    }
    [Button]
    public void ChangeToWaterParticles()
    {
        if (particles.TryGetComponent(out ParticleSystemRenderer value))
            value.sharedMaterial = bubbleMaterial;
        else
            particles.gameObject.SetActive(false);

        if (particles2.TryGetComponent(out value))
            value.sharedMaterial = bubbleMaterial;
        else
            particles2.gameObject.SetActive(false);

        _particlesCollider.enabled = true;
    }
    public void AdjustParticlesToShape(int i)
    {
        shape.radius = ShapeValues[i];
        emission.rateOverTime = _initialRateOverTime - _initialRateOverTime* rateMultiplier * i;
    }
}
