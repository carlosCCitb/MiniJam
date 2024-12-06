using UnityEngine;

[CreateAssetMenu(fileName = "DeathState", menuName = "ScriptableObjects/States/DeathState")]
public class DeathState : StatesSO
{
    public override void OnStateEnter(EnemyController enemyController)
    {
        enemyController.gameObject.GetComponent<AnimationController>().Die();
    }

    public override void OnStateExit(EnemyController enemyController)
    {
        enemyController.CleanDeadBody();
    }

    public override void OnStateUpdate(EnemyController enemyController)
    {
    }
}
