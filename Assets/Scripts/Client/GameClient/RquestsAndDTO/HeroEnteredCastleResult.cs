using System;
using System.Collections.Generic;

[Serializable]
public class HeroEnteredCastleResult : TurnSecondsBasedBooleanInfo
{
    public string castleId { get; set; }
    public string heroId { get; set; }
    public List<Coordinates> movementPath { get; set; }
    public int movePointsLeft { get; set; }
}
