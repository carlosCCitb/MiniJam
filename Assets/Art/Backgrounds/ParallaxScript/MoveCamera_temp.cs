using UnityEngine;

public class CameraVerticalMovement : MonoBehaviour
{
    // Velocidad de movimiento de la c�mara
    public float moveSpeed = 5f;

    // L�mites de movimiento en el eje Y
    public float upperLimit = 10f;
    public float lowerLimit = -10f;

    /* void Update()
     {
         // Obtener la entrada vertical del usuario (W/S o flechas arriba/abajo)
         float verticalInput = Input.GetAxis("Vertical");

         // Calcular el nuevo valor de posici�n en Y
         float newYPosition = transform.position.y + verticalInput * moveSpeed * Time.deltaTime;

         // Restringir el movimiento dentro de los l�mites especificados
         newYPosition = Mathf.Clamp(newYPosition, lowerLimit, upperLimit);

         // Actualizar la posici�n de la c�mara
         transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
     }*/
    void Update()
    {
        transform.position += new Vector3(0, Time.deltaTime * 5f, 0);
    }

}
