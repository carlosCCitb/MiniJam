using UnityEngine;

[CreateAssetMenu(fileName = "OrbitState", menuName = "ScriptableObjects/States/OrbitState")]
public class OrbitState : MovementStatesSO
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
        Vector2 radDirection = enemyController.Target.position - enemyController.transform.position;
        float radii = radDirection.magnitude;
        float value = tangentMovement.Direction.magnitude * tangentMovement.Direction.magnitude / radii * rigidbody.mass;
        rigidbody.AddForce(radDirection.normalized * value);
    }
}
