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
    public List<int> _buildingSet;
    [field: SerializeField] public List<int> buildingSet { get {
            return _buildingSet;
        } set {
            _buildingSet = value;
            Debug.Log(_buildingSet.Count);
        }
    }
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

