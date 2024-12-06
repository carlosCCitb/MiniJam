using UnityEngine;

[CreateAssetMenu(fileName = "OrbitState", menuName = "ScriptableObjects/States/OrbitState")]
public class OrbitState : StatesSO
{
    public override void OnStateEnter(EnemyController enemyController)
    {
        enemyController.GetComponent<RadialMovement>().Stop();
    }

    public override void OnStateExit(EnemyController enemyController)
    {

    }

    public override void OnStateUpdate(EnemyController enemyController)
    {
        Rigidbody2D rigidbody = enemyController.GetComponent<Rigidbody2D>();
        TangentMovement tangentMovement = enemyController.GetComponent<TangentMovement>();
        float radii = (enemyController.Target.position - enemyController.transform.position).magnitude;
        rigidbody.AddForce(tangentMovement.Direction * tangentMovement.Direction / radii * rigidbody.mass);
    }
}
