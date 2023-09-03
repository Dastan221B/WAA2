using System;

[Serializable]
public class SessionIdBasedRequest
{
    public string sessionId;

    public SessionIdBasedRequest(string sessionId)
    {
        this.sessionId = sessionId;
    }
}
