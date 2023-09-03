using UnityEngine;

public class BetweenScenesScripts : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
