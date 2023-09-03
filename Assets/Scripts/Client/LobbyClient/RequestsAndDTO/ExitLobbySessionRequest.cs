using System;

[Serializable]
public class ExitLobbySessionRequest : SessionIdBasedRequest
{
    public ExitLobbySessionRequest(string sessionId) : base(sessionId)
    {
        this.sessionId = sessionId;
    }
}