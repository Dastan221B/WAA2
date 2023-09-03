using System;

[Serializable]
public class LobbySessionParticipantInfo
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public int Ordinal { get; set; }
    public int UserId { get; set; }
    public int DicCastleId { get; set; }
    public int DicHeroId { get; set; }
}
