using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam;
    public float parallaxEffect;

    private SpriteRenderer[] spriteRenderers;

    void Start()
    {
        // Inicializar la posición de inicio y calcular la longitud usando el primer SpriteRenderer
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        if (spriteRenderers.Length > 0)
        {
            startpos = transform.position.x;
            length = spriteRenderers[0].bounds.size.x;
        }
        else
        {
            Debug.LogError("No SpriteRenderers found in children!");
        }
    }

    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        // Mover el GameObject base
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        // Ajustar la posición de inicio para un efecto de bucle continuo
        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}
