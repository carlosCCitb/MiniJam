using UnityEngine;

public class PropSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 _minMaxHorizontal;
    [SerializeField] private float _vertical;

    public void Spawn(Prop.Type type)
    { 
        GameManager.Instance.PropPool.Spawn(type, new Vector2(Random.Range(_minMaxHorizontal.x, _minMaxHorizontal.y), _vertical));
    }
}
