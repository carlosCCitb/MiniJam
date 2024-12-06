using UnityEngine;
using System.Collections.Generic;

public class PoleBehaviour : MonoBehaviour
{
    public Transform Pole;
    public Transform[] Bones;
    void OnEnable()
    {
        Bones = gameObject.GetComponentsInChildren<Transform>();
    }
    private void Update()
    {
        for (int i = 0; i < Bones.Length - 1; i++)
        {
            Bones[i].LookAt(Pole);
        }
    }
}
