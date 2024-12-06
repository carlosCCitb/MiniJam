using UnityEngine;

public class Parallax_Loop : MonoBehaviour
{
    public float parallaxEffect; // Controla la intensidad del parallax
    public GameObject cam;       // Cámara principal

    private float length;        // Altura del sprite
    private float startpos;      // Posición inicial del sprite
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Vincula la cámara si no se asignó manualmente
        if (cam == null)
        {
            cam = Camera.main.gameObject;
        }

        // Calcula la posición inicial y la altura del sprite
        startpos = transform.position.y;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            length = spriteRenderer.bounds.size.y;
        }
        else
        {
            Debug.LogError("No SpriteRenderer found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (cam == null || spriteRenderer == null) return;

        // Calcula el desplazamiento del parallax
        float dist = cam.transform.position.y * parallaxEffect;
        float temp = cam.transform.position.y * (1 - parallaxEffect);

        // Ajusta la posición del sprite
        transform.position = new Vector3(transform.position.x, startpos + dist, transform.position.z);

        // Reposiciona el sprite antes de que salga de la vista
        if (temp > startpos + length)
        {
            startpos += length;
        }
        else if (temp < startpos - length)
        {
            startpos -= length;
        }
    }
}
