using System;
using System.Collections.Generic;

[Serializable]
public class MoveHeroToCaslteResult : TurnSecondsBasedBooleanInfo
{
    public string castleObjectId { get; set; }
    public string heroId { get; set; }
    public HeroObjectFullInfo heroInCastle { get; set; }
    public int movePointsLeft { get; set; }
}
