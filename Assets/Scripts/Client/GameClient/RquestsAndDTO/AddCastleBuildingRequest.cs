using System;

[Serializable]
public class AddCastleBuildingRequest : GameSessionIdBasedRequest
{
    public string objectId;
    public int buildingId;

    public AddCastleBuildingRequest(string gameSessionId, string objectId, int buildingId) : base(gameSessionId)
    {
        this.gameSessionId = gameSessionId;
        this.objectId = objectId;
        this.buildingId = buildingId;
    }
}
