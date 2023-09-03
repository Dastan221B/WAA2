using System;
using System.Collections.Generic;

[Serializable]
public class MoveHeroToGarrisonResult : TurnSecondsBasedBooleanInfo
{
    public string castleObjectId { get; set; }
    public string heroId { get; set; }
    public HeroObjectFullInfo heroInGarrison { get; set; }
    public int movePointsLeft { get; set; }
}
