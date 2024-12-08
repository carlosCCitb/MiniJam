using UnityEngine;
using System.Collections.Generic;
using System;
public class Cursor : MonoBehaviour
{
    [SerializeField] private Color _enemy, _noEnemy, _currentCursor;
    [SerializeField] private int _enemyLayer;
    private void Awake()
    {
        
    }
    void OnGUI()
    {
        GUI.skin.settings.cursorColor = _currentCursor;
    }

    private void Update()
    {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousepos.z = -10f;
        RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector3.forward);
        if (hit.collider.gameObject.layer == _enemyLayer)
        {
            _currentCursor = Color.Lerp(_currentCursor, _enemy, 0.5f); 
        }
        else
        {
            _currentCursor = Color.Lerp(_currentCursor, _noEnemy, 0.5f);
        }
    }
}
