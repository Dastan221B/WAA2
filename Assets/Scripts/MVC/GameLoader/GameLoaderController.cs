using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoaderController : MonoBehaviour
{
    private GameLoaderModel _gameLoaderModel;
    
    public void Init(GameLoaderModel gameLoaderModel)
    {
        _gameLoaderModel = gameLoaderModel;
    }

    public void LoadGame()
    {
        _gameLoaderModel.LoadObjects();
        //StartCoroutine(LoadObjects());
    }

}
