using UnityEngine;

[CreateAssetMenu(fileName = "FallingState", menuName = "ScriptableObjects/States/FallingState")]
public class FallingState : NormalStates
{
    public override void OnStateEnter(EnemyController enemyController)
    {
    }

    public override void OnStateExit(EnemyController enemyController)
    {
    }

    public override void OnStateUpdate(EnemyController enemyController)
    {
        float dist = (enemyController.TargetToHit.position - enemyController.transform.position).magnitude;
        if (dist < enemyController.AttackRange)
        {
            enemyController.GoToState<AttackState>();
        }
    }
}
