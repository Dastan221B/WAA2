using System;
using System.Collections.Generic;

[Serializable]
public class LeaveCastleResult : SessionIdBasedBooleanResult
{
    public string castleObjectId { get; set; }
    public List<ArmySlotInfo> castleCreatures { get; set; }
    public string heroInCastleId { get; set; }
    public List<ArmySlotInfo> castleArmy { get; set; }
    public string heroInGarrisonId { get; set; }
    public List<ArmySlotInfo> garrisonArmy { get; set; }
}
