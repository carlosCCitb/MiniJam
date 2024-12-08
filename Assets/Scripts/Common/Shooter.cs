using System.Drawing;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private bool _isPlayer;
    [SerializeField] private AudioClipConfiguration _shootSound;
    public void Shoot(Bullet.Type type, Vector3 target, Vector3 initialPosition, Vector3 velocity, int damage)
    {
        Quaternion lookRotation = Quaternion.Euler(new Vector3(0f, 0f, AngleBetweenPoints(initialPosition, target) + 90.0f));
        GameManager.Instance.BulletPool.Spawn(type, initialPosition, lookRotation, _isPlayer ? BulletPool.PlayerBulletLayer : BulletPool.EnemyBulletLayer)
            .SetValues(velocity, damage);
        _shootSound.Play();
        float AngleBetweenPoints(Vector2 a, Vector2 b) => Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
    
}
