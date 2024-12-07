using UnityEngine;
using System.Reflection;
public class RetardedBehaviour : MonoBehaviour
{
    public GameObject Pole;
    public Vector3[] Lastpositions;
    public GameObject ToSwap;
    private Transform[] gameObjectsToSwap;
    private GameObject[] gameObjectsChildren;
    private float[] weights;
    public Transform PointUp, PointDown;
    private void Awake()
    {
        gameObjectsToSwap = ToSwap.GetComponentsInChildren<Transform>();
        gameObjectsChildren = new GameObject[gameObjectsToSwap.Length];
        weights = new float[gameObjectsToSwap.Length];
        Lastpositions = new Vector3[gameObjectsToSwap.Length];
        for (int i = 0; i < gameObjectsToSwap.Length; i++)
        {
            gameObjectsChildren[i] = new GameObject();
            //CopyComponent<Transform>(gameObjectsToSwap[i], gameObjectsChildren[i]);
            gameObjectsChildren[i].transform.parent = transform;
            weights[i] = (gameObjectsChildren[i].transform.position.y - PointDown.position.y) /
                (PointUp.position.y - PointDown.position.y);
            SpriteRenderer sr = gameObjectsToSwap[i].gameObject.GetComponent<SpriteRenderer>();
            //gameObjectsChildren[i].AddComponent<SpriteRenderer>();
            CopyComponent<SpriteRenderer>(sr, gameObjectsChildren[i]);
            sr.enabled = false;
        }
    }
    private void Update()
    {
        for (int i = 0; i < gameObjectsChildren.Length; i++)
        {
            Lastpositions[i] = gameObjectsChildren[i].transform.position;
            gameObjectsChildren[i].transform.position = Vector2.Lerp(Pole.transform.position, Lastpositions[i], weights[i]);
        }
    }
    public static T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        T copiedComponent = destination.AddComponent<T>();
        CopyFieldsAndProperties(original, copiedComponent);
        return copiedComponent;
    }
    private static void CopyFieldsAndProperties<T>(T source, T destination) where T : Component
    {
        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (var field in fields)
        {
            field.SetValue(destination, field.GetValue(source));
        }
        PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
        foreach (var property in properties)
        {
            if (property.CanWrite && property.CanRead)
            {
                property.SetValue(destination, property.GetValue(source));
            }
        }
    }

}
