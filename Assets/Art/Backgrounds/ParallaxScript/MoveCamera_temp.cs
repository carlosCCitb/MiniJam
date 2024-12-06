using UnityEngine;

public class CameraVerticalMovement : MonoBehaviour
{
    // Velocidad de movimiento de la cámara
    public float moveSpeed = 5f;

    // Límites de movimiento en el eje Y
    public float upperLimit = 10f;
    public float lowerLimit = -10f;

    /* void Update()
     {
         // Obtener la entrada vertical del usuario (W/S o flechas arriba/abajo)
         float verticalInput = Input.GetAxis("Vertical");

         // Calcular el nuevo valor de posición en Y
         float newYPosition = transform.position.y + verticalInput * moveSpeed * Time.deltaTime;

         // Restringir el movimiento dentro de los límites especificados
         newYPosition = Mathf.Clamp(newYPosition, lowerLimit, upperLimit);

         // Actualizar la posición de la cámara
         transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
     }*/
    void Update()
    {
        transform.position += new Vector3(0, Time.deltaTime * 5f, 0);
    }

}
