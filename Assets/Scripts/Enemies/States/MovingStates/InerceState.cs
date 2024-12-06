using UnityEngine;

[CreateAssetMenu(fileName = "InerceState", menuName = "ScriptableObjects/States/InerceState")]
public class InerceState : StatesSO
{
    public override void OnStateEnter(EnemyController enemyController)
    {
    }

    public override void OnStateExit(EnemyController enemyController)
    {
    }

    public override void OnStateUpdate(EnemyController enemyController)
    {
        enemyController.GetComponent<RadialMovement>().MoveWithInercy();
    }
}
