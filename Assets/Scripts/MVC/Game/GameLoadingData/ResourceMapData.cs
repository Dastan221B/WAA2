using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceMapData : IGameLoadedData
{
    public bool IsLoaded { get; private set; }

    private Dictionary<string, ResourceObject> _resourceObjects;
    public Type Type => typeof(ResourceMapData);


    public object PullOutData()
    {
        IsLoaded = false;
        return _resourceObjects;
    }


    public void SetData(object data)
    {
        try
        {
            _resourceObjects = (Dictionary<string, ResourceObject>)data;
            IsLoaded = true;
        }
        catch
        {
            Debug.LogError("Data doesn't loaded");
        }
    }
}