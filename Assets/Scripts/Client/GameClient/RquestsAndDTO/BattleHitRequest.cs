
using System;

[Serializable]
public class BattleHitRangeRequest : BattleActionRequest
{
    public int targetBattleFieldObjectId;


    public BattleHitRangeRequest(string gameSessionId, int creatureBattleFieldObjectId, int targetBattleFieldObjectId) : base(gameSessionId, creatureBattleFieldObjectId)
    {
        this.targetBattleFieldObjectId = targetBattleFieldObjectId;
        battleActionType = BattleActionType.RANGED_HIT;
    }
}

[Serializable]
public class BattleHitMeleeRequest : BattleActionRequest
{
    public int targetBattleFieldObjectId;
    public BattleFieldCoordinates battleFieldCoordinates;


    public BattleHitMeleeRequest(string gameSessionId, int creatureBattleFieldObjectId, int targetBattleFieldObjectId , BattleFieldCoordinates battleFieldCoordinates) : base(gameSessionId, creatureBattleFieldObjectId)
    {
        this.targetBattleFieldObjectId = targetBattleFieldObjectId;
        this.battleFieldCoordinates = battleFieldCoordinates;
        battleActionType = BattleActionType.MELEE_HIT;
    }
}
