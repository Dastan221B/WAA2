using System.Collections.Generic;
using System;

[Serializable]
public class SetParticipantFeeRequest : SessionIdBasedRequest
{
    public Dictionary<int, int> participantFeeMap;

    public SetParticipantFeeRequest(string sessionId, Dictionary<int, int> participantFeeMap) : base(sessionId)
    {
        this.sessionId = sessionId;
        this.participantFeeMap = participantFeeMap;
    }
}
