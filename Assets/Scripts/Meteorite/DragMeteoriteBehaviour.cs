using UnityEngine;
using System.Collections.Generic;
public class DragMeteoriteBehaviour : MonoBehaviour
{
    public List<GameObject> MeteorSkins;
    [SerializeField] private int _currentSkin;
    private void Awake()
    {
        _currentSkin = 0;
        foreach (GameObject meteor in MeteorSkins)
            meteor.SetActive(false);
        MeteorSkins[0].SetActive(true);
        MeteorSkins[5].SetActive(true);
    }
    public void ChangeSkin(int i)
    {
        if (_currentSkin == i)
            return;

        MeteorSkins[_currentSkin].SetActive(false);

        if(_currentSkin+5<MeteorSkins.Count)
            MeteorSkins[_currentSkin + 5].SetActive(false);
        _currentSkin = i;
        MeteorSkins[_currentSkin].SetActive(true);
        if (_currentSkin + 5 < MeteorSkins.Count)
            MeteorSkins[_currentSkin + 5].SetActive(true);
    }
}
