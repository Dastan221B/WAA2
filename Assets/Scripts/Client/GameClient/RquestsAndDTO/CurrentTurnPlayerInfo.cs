using System;

[Serializable]
public class CurrentTurnPlayerInfo : SessionIdBasedResponse
{
    public int ordinal { get; set; }
}
