using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class DicCastleDTO
{
    public long id { get; set; }
    public string name { get; set; }
    public long mapObjectId { get; set; }
    public List<int> creatureSet { get; set; }
    public List<int> buildingSet { get; set; } 

    public List<int> heroSet { get; set; }

    public DicCastleDTO Clone()
    {
        return new DicCastleDTO()
        {
            id = id,
            name = name,
            mapObjectId = mapObjectId,
            creatureSet = creatureSet,
            buildingSet = buildingSet,
            heroSet = heroSet
        };
    }

}

