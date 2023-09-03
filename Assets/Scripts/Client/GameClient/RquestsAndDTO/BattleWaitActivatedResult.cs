using System;

[Serializable]
public class BattleWaitActivatedResult : SessionIdBasedResponse
{
    public CreatureStackObjectFullInfo creatureStack { get; set; }
}
