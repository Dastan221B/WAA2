using System;
using System.Collections.Generic;

[Serializable]
public class TradeStartedResult : TurnSecondsBasedBooleanInfo
{
    public HeroObjectFullInfo requesterHeroInfo { get; set; }
    public HeroObjectFullInfo receiverHeroInfo { get; set; }
    public int movePointsLeft { get; set; }
    public List<Coordinates> movementPath { get; set; }
    public string heroId { get; set; }
}
