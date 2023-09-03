using System;

[Serializable]
public class BattleActionRequest : GameSessionIdBasedRequest
{
    public int creatureBattleFieldObjectId;
    public string battleActionType;

    public BattleActionRequest(string gameSessionId, int creatureBattleFieldObjectId) : base(gameSessionId)
    {
        this.creatureBattleFieldObjectId = creatureBattleFieldObjectId;
    }
}
