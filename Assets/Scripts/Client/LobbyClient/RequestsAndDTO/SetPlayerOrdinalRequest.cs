using System;

[Serializable]
public class SetPlayerOrdinalRequest : SessionIdBasedRequest
{
    public int currentOrdinal;
    public int newOrdinal;

    public SetPlayerOrdinalRequest(string sessionId, int currentOrdinal, int newOrdinal) : base(sessionId)
    {
        this.sessionId = sessionId;
        this.currentOrdinal = currentOrdinal;
        this.newOrdinal = newOrdinal;
    }
}
