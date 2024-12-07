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
        Debug.Log("dist " + dist);
        Debug.Log("range " + enemyController.AttackRange);
        if (dist < enemyController.AttackRange)
        {
            Debug.Log("a tiro nena");
            enemyController.GoToState<AttackState>();
        }
    }
}
