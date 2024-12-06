using UnityEngine;

public class Limits : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private BoxCollider2D colliderLeft, colliderRight, colliderTop, colliderBottom;

    private void Awake()
    {
        colliderLeft.transform.position = (Vector2)_camera.ScreenToWorldPoint(new Vector2(0, _camera.scaledPixelHeight * 0.5f));
        colliderRight.transform.position = (Vector2)_camera.ScreenToWorldPoint(new Vector2(_camera.scaledPixelWidth, _camera.scaledPixelHeight * 0.5f));
        colliderTop.transform.position = (Vector2)_camera.ScreenToWorldPoint(new Vector2(_camera.scaledPixelWidth * 0.5f, _camera.scaledPixelHeight));
        colliderBottom.transform.position = (Vector2)_camera.ScreenToWorldPoint(new Vector2(_camera.scaledPixelWidth * 0.5f, 0));

        colliderLeft.size = new Vector2(colliderLeft.size.x, colliderTop.transform.position.y - colliderBottom.transform.position.y);
        colliderRight.size = colliderLeft.size;

        colliderTop.size = new Vector2(colliderRight.transform.position.x - colliderLeft.transform.position.x, colliderTop.size.y);
        colliderBottom.size = colliderTop.size;

        colliderLeft.enabled = true;
        colliderRight.enabled = true;
        colliderTop.enabled = true;
        colliderBottom.enabled = true;
    }
}
