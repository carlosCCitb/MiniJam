using UnityEngine;
using System.Collections.Generic;
public class DragMeteoriteBehaviour : MonoBehaviour
{
    public List<GameObject> MeteorSkins;
    [SerializeField] private int _currentSkin;
    private void Awake()
    {
        _currentSkin = 0;
    }
    public void ChangeSkin(int i)
    {
        MeteorSkins[_currentSkin].SetActive(false);
        _currentSkin = i;
        MeteorSkins[_currentSkin].SetActive(true);
    }
}
