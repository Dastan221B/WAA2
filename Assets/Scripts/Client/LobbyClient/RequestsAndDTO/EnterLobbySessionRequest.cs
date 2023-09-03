using System;

[Serializable]
public class EnterLobbySessionRequest : SessionIdBasedRequest
{
    public EnterLobbySessionRequest(string sessionId) : base(sessionId)
    {
        this.sessionId = sessionId;
    }
}
