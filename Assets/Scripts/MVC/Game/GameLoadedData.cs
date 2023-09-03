using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLoadedData : MonoBehaviour
{
    private List<IGameLoadedData> _gameLoadedData = new List<IGameLoadedData>();
    public event Action OnLoadedAllData;

    private void Awake()
    {
        _gameLoadedData.Add(new CastlesData());
        _gameLoadedData.Add(new CreatureData());
        _gameLoadedData.Add(new HeroesData());
        _gameLoadedData.Add(new MinesData());
        _gameLoadedData.Add(new ResourceMapData());
        _gameLoadedData.Add(new ResourcesData());
        _gameLoadedData.Add(new TerrainCellsData());
    }

    public void AddToData(object data, Type type)
    {
        StartCoroutine(AddToDataAsync(data,type));
    }

    private IEnumerator AddToDataAsync(object data, Type type)
    {
        while (true)
        {
            foreach (var loader in _gameLoadedData)
            {
                if (type == loader.Type)
                {
                    loader.SetData(data);
                }
            }
            break;
            yield return null;
        }
        if (_gameLoadedData.Where(item => item.IsLoaded).Where(item => item.Type != typeof(ResourcesData)).ToArray().Length >= _gameLoadedData.Where(item => item.Type != typeof(ResourcesData)).ToArray().Length)
        {
            OnLoadedAllData?.Invoke();
        }
    }

    public bool TryGetLoadedDataByType(Type type , out object data)
    {
        foreach (var item in _gameLoadedData)
        {
            if(item.Type == type)
            {
                data = item.PullOutData();
                return true;
            }
        }
        data = null;
        return false;
    }

}