using UnityEngine;
using System.Collections.Generic;

public class SubmitTradeResult : TurnSecondsBasedBooleanInfo
{
    public string requesterHeroObjectId { get; set; }
    public string receiverHeroObjectId { get; set; }
    public List<ArmySlotInfo> requesterArmy { get; set; }
    public List<ArmySlotInfo> receiverArmy { get; set; }
}
