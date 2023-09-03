
public class MoveHeroRequestWithInteractive : MoveHeroRequest
{
    public string interactiveMapObjectId;
    public string interactiveMapObjectType;
    public string gameMapObjectType;

    public MoveHeroRequestWithInteractive(string gameSessionId, string objectId, int x, int y, string interactiveMapObjectId, string gameMapObjectType)
        : base(gameSessionId, objectId, x, y)
    {
        this.interactiveMapObjectId = interactiveMapObjectId;
        this.interactiveMapObjectType = gameMapObjectType;
        this.gameMapObjectType = "HERO";
    }
}
