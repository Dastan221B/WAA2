using System;

[Serializable]
public class ExitLobbySessionInfo : SessionIdBasedResponse
{
    public int participantOrdinal { get; set; }
}
