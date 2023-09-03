using System;
using System.Collections.Generic;

[Serializable]
public class HireCastleCreatureResult : TurnSecondsBasedBooleanInfo
{
    public string mapObjectId { get; set; }
    public List<ArmySlotInfo> creatureMap { get; set; }
}
