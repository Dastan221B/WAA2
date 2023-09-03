using System;
using System.Collections.Generic;

[Serializable]
public class MoveHeroInfo : TurnSecondsBasedBooleanInfo
{
    public List<Coordinates> movementPath { get; set; }
    public string heroId { get; set; }
}
