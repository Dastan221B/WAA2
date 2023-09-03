using System;
using System.Collections.Generic;

[Serializable]
public class MoveHeroToGarrisonRequest : GameSessionIdBasedRequest
{
    public string objectId;
    public List<ArmySlotInfo> castleCreatures;
    public List<ArmySlotInfo> armyInGarrison;

    public MoveHeroToGarrisonRequest(string gameSessionId, string objectId, List<ArmySlotInfo> castleCreatures, List<ArmySlotInfo> armyInGarrison) : base(gameSessionId)
    {
        this.gameSessionId = gameSessionId;
        this.objectId = objectId;
        this.castleCreatures = castleCreatures;
        this.armyInGarrison = armyInGarrison;
    }
}

