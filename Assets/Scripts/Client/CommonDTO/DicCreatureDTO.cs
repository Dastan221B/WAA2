using System;
using System.Collections.Generic;

[Serializable]
public class DicCreatureDTO
{
    public long id { get; set; }
    public int level { get; set; }
    public int minDmg { get; set; }
    public int maxDmg { get; set; }
    public int attack { get; set; }
    public int defense { get; set; }
    public int healthPoints { get; set; }
    public int speed { get; set; }
    public long mapObjectId { get; set; }
    public long battleFieldObjectId { get; set; }
    public long castleId { get; set; }
    public long buildingId { get; set; }
    public long upgradeToId { get; set; }
    public string name { get; set; }
    public MovementType movementType { get; set; }
    public AttackType attackType { get; set; }
    public List<ObjectPrice> price { get; set; }

    public DicCreatureDTO Clone()
    {
        return new DicCreatureDTO()
        {
            id = id,
            level = level,
            minDmg = minDmg,
            maxDmg = maxDmg,
            attack = attack,
            healthPoints = healthPoints,
            speed = speed,
            mapObjectId = mapObjectId,
            battleFieldObjectId = battleFieldObjectId ,
            castleId = castleId,
            buildingId = buildingId,
            upgradeToId = upgradeToId,
            name = name,
            movementType = movementType,
            attackType = attackType,
            price = price
        };
    }
}
