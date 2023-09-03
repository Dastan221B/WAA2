using System;
using System.Collections.Generic;

[Serializable]
public class BattleInitialInfo : TurnSecondsInfo
{
    public Dictionary<int, CreatureStackObjectFullInfo> mapObjects { get; set; }
    public FighterObjectFullInfo assaulter { get; set; }
    public FighterObjectFullInfo defender { get; set; }
    public int movePointsLeft { get; set; }
    public List<Coordinates> movementPath { get; set; }
    public string heroId { get; set; }
}
