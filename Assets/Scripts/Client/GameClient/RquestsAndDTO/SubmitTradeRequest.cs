using System.Collections.Generic;

public class SubmitTradeRequest : GameSessionIdBasedRequest
{
    public SubmitTradeRequest(string gameSessionId) : base(gameSessionId)
    {
        this.gameSessionId = gameSessionId;
    }
    public string objectId { get; set; }
    public List<ArmySlotInfo> requesterHeroArmy { get; set; }
    public string receiverHeroObjectId { get; set; }
    public List<ArmySlotInfo> receiverHeroArmy { get; set; }
}
