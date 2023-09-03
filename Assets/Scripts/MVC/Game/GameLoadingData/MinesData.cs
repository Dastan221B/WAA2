using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinesData : IGameLoadedData
{
    public bool IsLoaded { get; private set; }

    private Dictionary<string, MineObject> _mineObjects { get; set; }

    public Type Type => typeof(MinesData);


    public object PullOutData()
    {
        IsLoaded = false;
        return _mineObjects;
    }

    public void SetData(object data)
    {
        try
        {
            _mineObjects = (Dictionary<string, MineObject>)data;
            IsLoaded = true;
        }
        catch
        {
            Debug.LogError("Data doesn't loaded");
        }
    }
}
