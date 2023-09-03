using System;
using System.Collections.Generic;

[Serializable]
public class FighterObjectFullInfo
{
    public string mapObjectId { get; set; }
    public List<ArmySlotInfo> army { get; set; }
    public int dicHeroId { get; set; } = -1;
}
