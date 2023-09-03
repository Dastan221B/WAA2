using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FlaggedMineInfo : TurnSecondsBasedBooleanInfo
{
    public string mineObjectId { get; set; }
    public List<Coordinates> movementPath { get; set; }
    public string heroId { get; set; }
}
