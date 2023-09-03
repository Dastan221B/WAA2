using System;

[Serializable]
public class GameSessionIdBasedRequest
{
    public string gameSessionId;

    public GameSessionIdBasedRequest(string gameSessionId)
    {
        this.gameSessionId = gameSessionId;
    }
}
