using System.Collections.Generic;
using System;
using System.Linq;

[Serializable]
public class LobbySession
{
    public string Name { get; set; }
    public string Id { get; set; }
    public long CreatorId { get; set; }
    public int MaxPlayerCount { get; set; }
    public int TotalParticipantAmount { get; set; }
    public int TemplateId { get; set; }
    public int SizeId { get; set; }
    public bool IsAiAllowed { get; set; }
    public string Creator { get; set; }

    public LobbySessionStatus Status { get; set; }

    private List<SessionParticipant> _sessionParticipants = new List<SessionParticipant>();
    public Dictionary<int, int> ParticipantFeeMap { get; set; }

    public IReadOnlyList<SessionParticipant> SessionParticipants => _sessionParticipants;

    public LobbySession(string creator,string name, string id, long creatorId, int maxPlayerCount, int totalParticipantAmount, int templateId, int sizeId, bool isAiAllowed,
        LobbySessionStatus status, List<LobbySessionParticipantInfo> participants)
    {
        Creator = creator;
        Name = name;
        Id = id;
        CreatorId = creatorId;
        MaxPlayerCount = maxPlayerCount;
        TotalParticipantAmount = totalParticipantAmount;
        TemplateId = templateId;
        SizeId = sizeId;
        IsAiAllowed = isAiAllowed;
        Status = status;
        if(TotalParticipantAmount > MaxPlayerCount)
            MaxPlayerCount = TotalParticipantAmount;

        foreach (LobbySessionParticipantInfo participantInfo in participants)
        {
            SessionParticipant participant = new SessionParticipant(participantInfo.UserName, participantInfo.Email, participantInfo.Ordinal,
                participantInfo.DicCastleId, participantInfo.DicHeroId, participantInfo.UserId);
            _sessionParticipants.Add(participant);
        }
    }

    public void RemoveParticipant(SessionParticipant sessionParticipant)
    {
        _sessionParticipants.Remove(sessionParticipant);
        _sessionParticipants.OrderBy(item => item.Ordinal);
    }

    public void AddParticipant(SessionParticipant sessionParticipant)
    {
        _sessionParticipants.Add(sessionParticipant);
        _sessionParticipants.OrderBy(item => item.Ordinal);
    }
}
