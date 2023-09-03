using System;

[Serializable]
public class SendMessageToLobbySessionChatRequest : SessionIdBasedRequest
{
    public string message;

    public SendMessageToLobbySessionChatRequest(string sessionId, string message) : base(sessionId)
    {
        this.sessionId = sessionId;
        this.message = message;
    }
}
