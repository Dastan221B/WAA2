using System;
using System.Collections.Generic;

[Serializable]
public class TurnNotificationInfo : SessionIdBasedBooleanResult
{
    public int ordinal { get; set; }
    public int turnSeconds { get; set; }
    public Dictionary<string, HeroObjectFullInfo> heroesInfo { get; set; }
    public Dictionary<string, CastleObjectFullInfo> castlesInfo { get; set; }
}
