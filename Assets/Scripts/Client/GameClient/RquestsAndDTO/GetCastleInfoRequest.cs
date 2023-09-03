using System;

[Serializable]
public class GetCastleInfoRequest : GameSessionIdBasedRequest
{
    public string objectId;

    public GetCastleInfoRequest(string gameSessionId, string objectId) : base(gameSessionId)
    {
        this.gameSessionId = gameSessionId;
        this.objectId = objectId;
    }
}
