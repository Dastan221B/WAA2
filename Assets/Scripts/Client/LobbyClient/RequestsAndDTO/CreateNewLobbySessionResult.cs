using System;
using System.Collections.Generic;

[Serializable]
public class CreateNewLobbySessionResult : SessionIdBasedBooleanResult
{
    public string Name { get; set; }
    public int MaxPlayerCount { get; set; }
    public int TotalParticipantAmount { get; set; }
    public int TemplateId { get; set; }
    public int SizeId { get; set; }
    public int CreatorId { get; set; }
    public bool IsAIAllowed { get; set; }
    public LobbySessionStatus Status { get; set; }
    public List<LobbySessionParticipantInfo> Participants { get; set; }
}
