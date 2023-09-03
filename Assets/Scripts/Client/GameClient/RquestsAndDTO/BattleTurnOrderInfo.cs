using System.Collections.Generic;

public class BattleTurnOrderInfo : SessionIdBasedResponse
{
    public List<CreatureStackBattleObjectFullInfo> turnOrder { get; set; }
}
