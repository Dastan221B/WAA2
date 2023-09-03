using System;
using System.Collections.Generic;

[Serializable]
public class HireCastleCreatureRequest : GameSessionIdBasedRequest
{
    public string objectId;
    public int creatureId;
    public int amount;
    public List<ArmySlotInfo> castleCreatures;
    public List<ArmySlotInfo> armyInGarrison;

    public HireCastleCreatureRequest(string gameSessionId, string objectId, int creatureId, int amount,
        List<ArmySlotInfo> castleCreatures, List<ArmySlotInfo> armyInGarrison) : base(gameSessionId)
    {
        this.gameSessionId = gameSessionId;
        this.objectId = objectId;
        this.creatureId = creatureId;
        this.amount = amount;
        this.castleCreatures = castleCreatures;
        this.armyInGarrison = armyInGarrison;
    }
}
