using System;
using System.Collections.Generic;

[Serializable]
public class LobbySessionResult : SessionIdBasedBooleanResult
{
    public string name { get; set; }
    public int maxPlayerCount { get; set; }
    public int creatorId { get; set; }
    public int totalParticipantAmount { get; set; }
    public int templateId { get; set; }
    public int sizeId { get; set; }
    public bool isAIAllowed { get; set; }
    public LobbySessionStatus status { get; set; }
    public List<LobbySessionParticipantInfo> participants { get; set; }
}
