using System;

[Serializable]
public class BattleCreatureWaitRequest : BattleActionRequest
{
    public BattleCreatureWaitRequest(string gameSessionId, int creatureBattleFieldObjectId) : base(gameSessionId, creatureBattleFieldObjectId)
    {
        battleActionType = BattleActionType.WAIT;
    }
}
