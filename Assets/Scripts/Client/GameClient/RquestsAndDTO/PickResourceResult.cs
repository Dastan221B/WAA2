using System;
using System.Collections.Generic;

[Serializable]
public class PickResourceResult : TurnSecondsBasedBooleanInfo
{
    public string resourceid { get; set; }
    public int amount { get; set; }
    public List<Coordinates> movementPath { get; set; }
    public int movePointsLeft { get; set; }
    public string heroId { get; set; }
}
