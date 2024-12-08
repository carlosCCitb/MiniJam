using UnityEngine;

public class IKRotator : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private Transform _transformToInvert;
    [SerializeField] private Transform _hand, _head;
    [SerializeField] private float CharacterMaxAngleRot, CharacterMinAngleRot;
    private Quaternion initialRotation;
    private void Awake()
    {
        initialRotation = transform.rotation;
    }
    private void LateUpdate()
    {
        float angle = AngleBetweenPoints(transform.position, enemyController.TargetToHit.position);
        //Debug.Log(angle);
        Quaternion lookRotation;
        float offset2 = -45f;
        float correction = 1f;
        //if (CharacterMaxAngleRot > angle && angle > CharacterMinAngleRot)
        if (Mathf.Abs(angle) <= CharacterMaxAngleRot || Mathf.Abs(angle) >= CharacterMaxAngleRot + 90f)
        {
            lookRotation = initialRotation;
            //Debug.Log("mid");
        }
        else if (angle > 0)
        {
            if (angle <= 90f)
            {
                lookRotation = Quaternion.Euler(new Vector3(0f, 0f, correction * angle + offset2));
                //Debug.Log("topD");
            }
            else
            {
                lookRotation = Quaternion.Euler(new Vector3(0f, 0f, correction * angle - offset2 + 180f));
                //Debug.Log("topI");
            }
        }
        else
        {
            if (angle >= -90f)
            {
                lookRotation = Quaternion.Euler(new Vector3(0f, 0f, correction * angle - offset2));
                //Debug.Log("botD");
            }
            else
            {
                lookRotation = Quaternion.Euler(new Vector3(0f, 0f, correction * angle - offset2 + 90f));
                //Debug.Log("botI");
            }
        }
        transform.rotation = lookRotation;
        float value = Mathf.Abs(_transformToInvert.localScale.y);
        _transformToInvert.localScale = new Vector3(enemyController.TargetToHit.position.x > transform.position.x ?
            -value : value, _transformToInvert.localScale.y, _transformToInvert.localScale.z);

        float AngleBetweenPoints(Vector2 a, Vector2 b) => Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
