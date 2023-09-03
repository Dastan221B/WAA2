using System;
using System.Collections.Generic;

[Serializable]
public class AddCastleBuildingResult : TurnSecondsBasedBooleanInfo
{
    public string castleObjectId { get; set; }
    public List<int> buildings { get; set; }
    public int buildingId { get; set; }
    public Dictionary<int, PurchaseableCreatureInfo> purchasableCreatureInfoMap { get; set; }
}
