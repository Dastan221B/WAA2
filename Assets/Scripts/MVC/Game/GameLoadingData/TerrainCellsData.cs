using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainCellsData : IGameLoadedData
{

    private TerrainCell[][] _terrainCells;

    public bool IsLoaded{ get; private set; }

    public Type Type => typeof(TerrainCellsData);


    public object PullOutData()
    {
        IsLoaded = false;
        return _terrainCells;
    }

    public void SetData(object data)
    {
        try
        {
            _terrainCells = (TerrainCell[][])data;
            IsLoaded = true;
        }
        catch
        {
            Debug.LogError("Data doesn't loaded");
        }
    }
}