using System;
using System.Collections.Generic;

[Serializable]
public class LeaveCastleRequest : GameSessionIdBasedRequest
{
    public string objectId;
    public string gameMapObjectType = "CASTLE";
    public List<ArmySlotInfo> castleCreatures;
    public List<ArmySlotInfo> armyInGarrison;

    public LeaveCastleRequest(string gameSessionId, string objectId, List<ArmySlotInfo> castleCreatures, List<ArmySlotInfo> armyInGarrison) : base(gameSessionId)
    {
        this.gameSessionId = gameSessionId;
        this.objectId = objectId;
        this.castleCreatures = castleCreatures;
        //this.heroObjectId = heroObjectId;
        this.armyInGarrison = armyInGarrison;
    }
}
