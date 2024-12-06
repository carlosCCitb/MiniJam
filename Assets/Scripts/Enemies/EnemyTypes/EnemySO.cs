using UnityEngine;
[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemies/Enemy")]
public class EnemySO : ScriptableObject
{
    public int Health;
    public int Damage;
    public float Range;
    public float Offset;
    public float FallingSpeed, ChaseSpeed, TangentSpeed;
    public Sign Sign;
    public float Coldown;
    public Bullet.Type BulletType;
    public float BulletSpeed;
}
