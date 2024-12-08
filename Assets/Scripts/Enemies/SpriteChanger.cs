using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _normal, _underwater;

    private void OnEnable()
    {
        ChangeSprite();

        WaterLimit.GoDeep += ChangeSprite;
    }

    private void OnDisable()
    {
        WaterLimit.GoDeep -= ChangeSprite;
    }

    private void ChangeSprite()
    {
        _spriteRenderer.sprite = WaterLimit.WaterDepths ? _underwater : _normal;
    }
}
