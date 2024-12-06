using UnityEngine;

public class EnemyHPBehaviour : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemySO enemySO;
    private int Health;
    void Awake()
    {
        Health = enemySO.Health;
    }
    [SerializeField] private EnemySO EnemySO;
    public void OnHurt(int Damage)
    {
        Health -= Damage;
        if (Health < 1)
            OnDie();
    }
    public void OnDie()
    {
        throw new System.NotImplementedException();
    }
}
