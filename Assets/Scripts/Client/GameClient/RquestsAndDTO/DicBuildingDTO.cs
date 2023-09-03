using System;
using System.Collections.Generic;

[Serializable]
public class DicBuildingDTO
{
    public long id { get; set; }
    public string name { get; set; }
    public long castleId { get; set; }
    public int level { get; set; }
    public int defaultCreatureGrowth { get; set; }
    public long creatureId { get; set; }
    public List<int> dependencySet { get; set; }
    public List<ObjectPrice> price { get; set; }

    public DicBuildingDTO Clone()
    {
        return new DicBuildingDTO()
        {
            id = id,
            name = name,
            castleId = castleId,
            level = level,
            defaultCreatureGrowth = defaultCreatureGrowth,
            creatureId = creatureId,
            dependencySet = dependencySet,
            price = price
        };
    }
}
