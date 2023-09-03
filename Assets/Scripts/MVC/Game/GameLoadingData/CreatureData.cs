using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureData : IGameLoadedData
{
    public bool IsLoaded { get; private set; }

    public Type Type => typeof(CreatureData);

    private Dictionary<string, CreatureObject> _creatureObjects { get; set; }

    public void SetData(object data)
    {
        try
        {
            _creatureObjects = (Dictionary<string, CreatureObject>)data;
            IsLoaded = true;
        }
        catch
        {
            Debug.LogError("Data doesn't loaded");
        }
 
    }

    public object PullOutData()
    {
        IsLoaded = false;
        return _creatureObjects;
    }
}