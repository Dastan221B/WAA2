using System;

[Serializable]
public class BattleHitResultInfo : SessionIdBasedBooleanResult
{
    public int activeCreatureStackBattleObjectId { get; set; }
    public int turnSeconds { get; set; }
    public CreatureStackObjectFullInfo attackerCreatureStack { get; set; }
    public CreatureStackObjectFullInfo targetCreatureStack { get; set; }
    public HitLog hitLog { get; set; }
}
