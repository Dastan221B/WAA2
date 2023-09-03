using System;
using UnityEngine;

public class GameLoadScreenView : MonoBehaviour
{
    [SerializeField] private GameObject _loadScreen;
   
    public void OpenLoadScreen()
    {
        _loadScreen.SetActive(true);
    }

    public void CloseLoadScreen()
    {
        _loadScreen.SetActive(false);
    }

}