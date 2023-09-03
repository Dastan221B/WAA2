using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class StartGameRequest : SessionIdBasedRequest
{
    public StartGameRequest(string sessionId) : base(sessionId)
    {
        this.sessionId = sessionId;
    }
}
