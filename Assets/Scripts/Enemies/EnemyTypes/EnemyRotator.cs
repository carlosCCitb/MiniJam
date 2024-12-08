using UnityEngine;

public class EnemyRotator : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private Transform _transformToInvert;
    [SerializeField] private Transform _hand, _head;
    [SerializeField] private float CharacterMaxAngleRot, CharacterMinAngleRot;
    [SerializeField] private float offsetHand, offsetHead;
    private Quaternion initialRotation;
    [SerializeField] private bool raptor;
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
        if( Mathf.Abs(angle) <= CharacterMaxAngleRot || Mathf.Abs(angle) >= CharacterMaxAngleRot + 90f)
        {
            lookRotation = initialRotation;
            //Debug.Log("mid");
        }
        else if (angle > 0)
        {
            if(angle<=90f)
            {
                lookRotation = Quaternion.Euler(new Vector3(0f, 0f, correction * angle + offset2));
                //Debug.Log("topD");
            }
            else
            {
                lookRotation = Quaternion.Euler(new Vector3(0f, 0f, correction * angle -offset2 + 180f));
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
                lookRotation = Quaternion.Euler(new Vector3(0f, 0f, correction * angle -offset2 + 90f));
                //Debug.Log("botI");
            }
        }
        transform.rotation = lookRotation;

        float value = Mathf.Abs(_transformToInvert.localScale.y);
        _transformToInvert.localScale = new Vector3(enemyController.TargetToHit.position.x> transform.position.x ?
            -value : value,_transformToInvert.localScale.y, _transformToInvert.localScale.z);

        float AngleBetweenPoints(Vector2 a, Vector2 b) => Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;

        float angle2 = AngleBetweenPoints(transform.position, enemyController.TargetToHit.position);


        Quaternion lookRotation2;
        //if (CharacterMaxAngleRot > angle2 && angle2 > CharacterMinAngleRot)
        if (Mathf.Abs(angle) <= CharacterMaxAngleRot) 
        {
            lookRotation2 = Quaternion.Euler(new Vector3(0f, 0f, angle2-offsetHand));
            //Debug.Log("mid");
        }
        else if (Mathf.Abs(angle) >= CharacterMaxAngleRot + 90f)
        {
            if (!raptor)
                lookRotation2 = Quaternion.Euler(new Vector3(0f, 0f, angle2 - offsetHand));
            else
            {
                //Debug.Log("AYUDA");
                lookRotation2 = Quaternion.Euler(new Vector3(0f, 0f, angle2 + offsetHand-160f));
            }
        }
        else if(angle<0)
        {
            if (angle>=-90f)
                lookRotation2 = Quaternion.Euler(new Vector3(0f, 0f, CharacterMinAngleRot - offsetHand));
            else
                lookRotation2 = Quaternion.Euler(new Vector3(0f, 0f, CharacterMinAngleRot + offsetHand + 90f));
            //Debug.Log("bot");
        }
        else
        {
            if(angle<=90)
                lookRotation2 = Quaternion.Euler(new Vector3(0f, 0f, CharacterMaxAngleRot - offsetHand));
            else
                lookRotation2 = Quaternion.Euler(new Vector3(0f, 0f, CharacterMaxAngleRot + offsetHand - 90f));
            //Debug.Log("top");
        }
        _hand.rotation = lookRotation2;


        float angle3 = AngleBetweenPoints(transform.position, enemyController.TargetToHit.position);
        Quaternion lookRotation3;
        if (Mathf.Abs(angle) <= CharacterMaxAngleRot ) 
        {
            lookRotation3 = Quaternion.Euler(new Vector3(0f, 0f, angle2 - offsetHead));
            //Debug.Log("mid");
        }
        else if (Mathf.Abs(angle) >= CharacterMaxAngleRot + 90f)
        {
            lookRotation3 = Quaternion.Euler(new Vector3(0f, 0f, angle2 + offsetHead + 170f));
        }
        else if (angle2 < 0)
        {
            if (angle >= -90f)
                lookRotation3 = Quaternion.Euler(new Vector3(0f, 0f, CharacterMinAngleRot - offsetHead));
            else
                lookRotation3 = Quaternion.Euler(new Vector3(0f, 0f, CharacterMinAngleRot + offsetHead + 90f));
            //Debug.Log("bot");
        }
        else
        {
            if (angle <= 90)
                lookRotation3 = Quaternion.Euler(new Vector3(0f, 0f, CharacterMaxAngleRot - offsetHead));
            else
                lookRotation3 = Quaternion.Euler(new Vector3(0f, 0f, CharacterMaxAngleRot + offsetHead - 90f));
            //Debug.Log("top");
        }
        _head.rotation = lookRotation3;
    }
}
