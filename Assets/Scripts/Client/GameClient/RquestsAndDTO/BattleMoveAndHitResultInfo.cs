using System;
using System.Collections.Generic;

[Serializable]
public class BattleMoveAndHitResultInfo : SessionIdBasedBooleanResult
{
    public int activeCreatureStackBattleObjectId { get; set; }
    public List<BattleFieldCoordinates> path { get; set; }
    public int turnSeconds { get; set; }
    public CreatureStackObjectFullInfo attackerCreatureStack { get; set; }
    public CreatureStackObjectFullInfo targetCreatureStack { get; set; }
    public HitLog hitLog { get; set; }
}
