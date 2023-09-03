using System;

[Serializable]
public class BattleMoveAndHitRequest : BattleActionRequest
{
    public int targetBattleFieldObjectId;
    public BattleFieldCoordinates battleFieldCoordinates;

    public BattleMoveAndHitRequest(string gameSessionId, int creatureBattleFieldObjectId, int targetBattleFieldObjectId, BattleFieldCoordinates battleFieldCoordinates) : base(gameSessionId, creatureBattleFieldObjectId)
    {
        this.targetBattleFieldObjectId = targetBattleFieldObjectId;
        this.battleFieldCoordinates = battleFieldCoordinates;
        battleActionType = BattleActionType.MOVE_AND_MELEE_HIT;
    }
}
