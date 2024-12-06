using UnityEngine;

[CreateAssetMenu(fileName = "AttackState", menuName = "ScriptableObjects/States/AttackState")]
public class AttackState : StatesSO
{
    public override void OnStateEnter(EnemyController enemyController)
    {
    }

    public override void OnStateExit(EnemyController enemyController)
    {
        enemyController.AttackFinished();
    }

    public override void OnStateUpdate(EnemyController enemyController)
    {
    }
}
