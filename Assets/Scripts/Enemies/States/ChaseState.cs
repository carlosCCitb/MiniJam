using UnityEngine;

[CreateAssetMenu(fileName = "ChaseState", menuName = "ScriptableObjects/States/ChaseState")]
public class ChaseState : StatesSO
{
    public override void OnStateEnter(EnemyController enemyController)
    {
    }

    public override void OnStateExit(EnemyController enemyController)
    {

    }

    public override void OnStateUpdate(EnemyController enemyController)
    {
        enemyController.GetComponent<RadialMovement>().MoveToPlayer();
    }
}
