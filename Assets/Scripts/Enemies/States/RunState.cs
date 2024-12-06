using UnityEngine;

[CreateAssetMenu(fileName = "RunState", menuName = "ScriptableObjects/States/RunState")]
public class RunState : StatesSO
{
    public override void OnStateEnter(EnemyController enemyController)
    {
    }

    public override void OnStateExit(EnemyController enemyController)
    {
    }

    public override void OnStateUpdate(EnemyController enemyController)
    {
        enemyController.GetComponent<RadialMovement>().RunFromPlayer();
    }
}
