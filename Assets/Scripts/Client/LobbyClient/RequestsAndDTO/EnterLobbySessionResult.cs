using System;

[Serializable]
public class EnterLobbySessionResult : SessionIdBasedBooleanResult
{
    public int ordinal { get; set; }
}
