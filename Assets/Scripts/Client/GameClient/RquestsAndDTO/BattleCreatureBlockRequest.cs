using System;

[Serializable]
public class BattleCreatureBlockRequest : BattleActionRequest
{
    public BattleCreatureBlockRequest(string gameSessionId, int creatureBattleFieldObjectId) : base(gameSessionId, creatureBattleFieldObjectId)
    {
        battleActionType = BattleActionType.BLOCK;
    }
}
