using UnityEngine;
using System.Reflection;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;
public class RetardedBehaviour : MonoBehaviour
{
    public GameObject Pole;
    public Vector3[] Lastpositions;
    public GameObject ToSwap;
    public Transform[] gameObjectsToSwap;
    private GameObject[] gameObjectsChildren;
    private GameObject[] gameObjectsGrandChildren;
    private float[] weights;
    public Transform PointUp, PointDown;
    public GameObject empty;

    CancellationTokenSource _cancellationTokenSource;

    private void Awake()
    {
        //transform.localScale = Pole.transform.localScale;
        gameObjectsToSwap = ToSwap.GetComponentsInChildren<Transform>();
        gameObjectsChildren = new GameObject[gameObjectsToSwap.Length];
        gameObjectsGrandChildren = new GameObject[gameObjectsToSwap.Length];
        weights = new float[gameObjectsToSwap.Length];
        Lastpositions = new Vector3[gameObjectsToSwap.Length];
        for (int i = 1; i < gameObjectsToSwap.Length; i++)
        {
            gameObjectsChildren[i] = Instantiate(empty, gameObjectsToSwap[i].transform.position, Quaternion.identity, transform);
            gameObjectsGrandChildren[i] = Instantiate(empty);
            gameObjectsGrandChildren[i].transform.parent = gameObjectsChildren[i].transform;
            gameObjectsChildren[i].transform.localScale = Vector3.Scale(Pole.transform.localScale, ToSwap.transform.parent.localScale);
            weights[i] = (gameObjectsChildren[i].transform.position.y - PointDown.position.y) /
                (PointUp.position.y - PointDown.position.y);
           SpriteRenderer sr = gameObjectsToSwap[i].gameObject.GetComponent<SpriteRenderer>();
           CopyComponent<SpriteRenderer>(sr, gameObjectsGrandChildren[i]);
           sr.enabled = false;
        }
    }

    private void OnEnable()
    {
        _cancellationTokenSource = new();
        LateUpdateAsync().Forget();
    }

    private void OnDisable()
    {
        _cancellationTokenSource.Cancel();
    }

    private void LateUpdate()
    {
        for (int i = 1; i < gameObjectsChildren.Length; i++)
        {
            gameObjectsGrandChildren[i].transform.localPosition = gameObjectsToSwap[i].transform.localPosition
                + Vector3.up * ToSwap.transform.localPosition.y;
            gameObjectsChildren[i].transform.position = Vector2.Lerp(Pole.transform.position, Lastpositions[i], weights[i]);
        }
    }

    private async UniTaskVoid LateUpdateAsync()
    {
        while (enabled)
        {
            for (int i = 1; i < gameObjectsChildren.Length; i++)
            {
                Lastpositions[i] = gameObjectsChildren[i].transform.position;
            }

            await UniTask.Delay(TimeSpan.FromSeconds(1.0f / 30.0f), delayTiming: PlayerLoopTiming.PostLateUpdate, cancellationToken: _cancellationTokenSource.Token);
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

    private void OnDestroy()
    {
        _cancellationTokenSource?.Dispose();
    }
}
