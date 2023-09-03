using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastlesData : IGameLoadedData
{
    public bool IsLoaded { get; private set; }

    private Dictionary<string, CastleObject> _castleObjects { get; set; }

    public Type Type => typeof(CastlesData);


    public object PullOutData()
    {
        IsLoaded = false;
        return _castleObjects;
    }

    public void SetData(object data)
    {
        try
        {
            _castleObjects = (Dictionary<string, CastleObject>)data;
            IsLoaded = true;
        }
        catch
        {
            Debug.LogError("Data doesn't loaded");
        }
    }
}