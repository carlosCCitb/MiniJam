using System.Drawing;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private bool _isPlayer;
    [SerializeField] private AudioClipConfiguration _shootSound;
    [SerializeField] private int bulletsNumber;
    [SerializeField] private float angleRange = 12f;
    private float angle = 0;

    public void Shoot(Bullet.Type type, Vector3 target, Vector3 initialPosition, Vector3 velocity, int damage)
    {
        var saveVelocity = velocity;
        angle = 0;
        Quaternion lookRotation = Quaternion.Euler(new Vector3(0f, 0f, AngleBetweenPoints(initialPosition, target) + 90.0f));
        for(int i = 0; i< bulletsNumber; i++)
        {
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            velocity = rotation * saveVelocity;
            GameManager.Instance.BulletPool.Spawn(type, initialPosition, lookRotation, _isPlayer ? BulletPool.PlayerBulletLayer : BulletPool.EnemyBulletLayer)
                .SetValues(velocity, damage);
            angle =  angleRange;
            angleRange *= -1f;
        }
        _shootSound.Play();
        float AngleBetweenPoints(Vector2 a, Vector2 b) => Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
    
}
