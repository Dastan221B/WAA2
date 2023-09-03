using System;

[Serializable]
public class MoveHeroRequest : GameSessionIdBasedRequest
{
    public string objectId;
    public Coordinates coordinates;

    public MoveHeroRequest(string gameSessionId, string objectId, int x, int y) : base(gameSessionId)
    {
        this.gameSessionId = gameSessionId;
        this.objectId = objectId;
        coordinates = new Coordinates(x, y);
    }
}