using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesData : IGameLoadedData
{
    public bool IsLoaded { get; private set; }

    private Dictionary<int, Resource> _resources;
    public Type Type => typeof(ResourcesData);


    public object PullOutData()
    {
        IsLoaded = false;
        return _resources;
    }

    public void SetData(object data)
    {
        try
        {
            _resources = (Dictionary<int, Resource>)data;
            IsLoaded = true;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}