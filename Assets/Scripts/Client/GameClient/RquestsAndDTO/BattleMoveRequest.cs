using System;

public class BattleMoveRequest : BattleActionRequest
{
    public BattleFieldCoordinates battleFieldCoordinates;

    public BattleMoveRequest(string gameSessionId, int creatureBattleFieldObjectId, BattleFieldCoordinates battleFieldCoordinates) : base(gameSessionId, creatureBattleFieldObjectId)
    {
        this.battleFieldCoordinates = battleFieldCoordinates;
        battleActionType = BattleActionType.MOVE;
    }
}
