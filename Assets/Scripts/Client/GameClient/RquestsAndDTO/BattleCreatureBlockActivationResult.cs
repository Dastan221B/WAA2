using System;

[Serializable]
public class BattleCreatureBlockActivationResult : SessionIdBasedResponse
{
    public CreatureStackObjectFullInfo creatureStack { get; set; }
    public int additionalDefence { get; set; }
}
