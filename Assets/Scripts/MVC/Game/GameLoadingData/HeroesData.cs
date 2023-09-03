using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesData : IGameLoadedData
{
    public bool IsLoaded { get; private set; }

    private Dictionary<string, HeroObject> _heroesObjects;
    public Type Type => typeof(HeroesData);

    public object PullOutData()
    {
        IsLoaded = false;
        return _heroesObjects;
    }

    public void SetData(object data)
    {
        try
        {
            _heroesObjects = (Dictionary<string, HeroObject>)data;
            IsLoaded = true;
        }
        catch
        {
            Debug.LogError("Data doesn't loaded");
        }
    }
}