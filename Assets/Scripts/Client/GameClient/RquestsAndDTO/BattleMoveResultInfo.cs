using System;
using System.Collections.Generic;

[Serializable]
public class BattleMoveResultInfo : SessionIdBasedBooleanResult
{
    public int activeCreatureStackBattleObjectId { get; set; }
    public List<BattleFieldCoordinates> path { get; set; }
    public int turnSeconds { get; set; }
}
