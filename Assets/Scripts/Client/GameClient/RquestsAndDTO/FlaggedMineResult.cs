using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FlaggedMineResult : TurnSecondsBasedBooleanInfo
{
    public string mineObjectId { get; set; }
    public int dicResourceId { get; set; }
    public int movePointsLeft { get; set; }
    public List<Coordinates> movementPath { get; set; }
    public string heroId { get; set; }
}
