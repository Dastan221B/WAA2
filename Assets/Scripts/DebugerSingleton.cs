using System.Collections;
using UnityEngine;

public class DebugerSingleton : MonoBehaviour
{
    public static DebugerSingleton Instance;


    public void Awake()
    {
        Instance = GetComponent<DebugerSingleton>();
    }

    public void DebugText(object text)
    {
        Debug.Log(text.ToString());
    }

}