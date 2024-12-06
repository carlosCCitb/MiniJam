using UnityEngine;

[CreateAssetMenu(fileName = "DeathState", menuName = "ScriptableObjects/States/DeathState")]
public class DeathState : StatesSO
{
    public override void OnStateEnter(EnemyController enemyController)
    {
        enemyController.gameObject.GetComponent<AnimationController>().Die();
        if(enemyController.TryGetComponent<TangentMovement>(out TangentMovement tang))
            enemyController.GetComponent<TangentMovement>().enabled = false;
    }

    public override void OnStateExit(EnemyController enemyController)
    {
        enemyController.DeadBehaviour();
        if (enemyController.TryGetComponent<TangentMovement>(out TangentMovement tang))
            enemyController.GetComponent<TangentMovement>().enabled = true;
    }

    public override void OnStateUpdate(EnemyController enemyController)
    {
    }
}
