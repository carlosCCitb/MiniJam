using UnityEngine;
using System.Collections.Generic;
using System;
public class Cursor : MonoBehaviour
{
    [SerializeField] private Color _enemy, _noEnemy, _currentCursor;
    [SerializeField] private int _enemyLayer;
    private void Update()
    {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousepos.z = -10f;
        RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector3.forward);
        if(hit)
        {
            if (hit.collider.gameObject.layer == _enemyLayer)
            {
                Debug.Log("enemy");
                _currentCursor = Color.Lerp(_currentCursor, _enemy, 0.5f);
            }
            else
            {
                Debug.Log("no enemy");
                _currentCursor = Color.Lerp(_currentCursor, _noEnemy, 0.5f);
            }
        }
        else
        {
            Debug.Log("nothing");
            _currentCursor = Color.Lerp(_currentCursor, _noEnemy, 0.5f);
        }
    }
}
