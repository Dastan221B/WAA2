using System;

[Serializable]
public class StartEndTurnRequest : GameSessionIdBasedRequest
{
    public StartEndTurnRequest(string gameSessionId) : base(gameSessionId)
    {
        this.gameSessionId = gameSessionId;
    }
}
