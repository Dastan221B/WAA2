public class BattleTurnNotificationInfo : SessionIdBasedResponse
{
    public CreatureStackBattleObjectFullInfo activeCreatureStack { get; set; }
    public int round { get; set; }
}
