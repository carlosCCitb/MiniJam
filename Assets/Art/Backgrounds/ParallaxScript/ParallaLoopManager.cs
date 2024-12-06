using UnityEngine;

public class ParallaxLoopManager : MonoBehaviour
{
    public GameObject cam;

    void Start()
    {
        Parallax_Loop[] layers = GetComponentsInChildren<Parallax_Loop>();
        foreach (var layer in layers)
        {
            layer.enabled = true;
        }
    }
}
