using System;

[Serializable]
public class SessionParticipant
{
    public UserDTO UserInfo { get; private set; }
    public int Ordinal { get; set; }
    public long UserId { get; private set; }
    public int DicCastleId { get; set; }
    public int DicHeroId { get; set; }

    public SessionParticipant(string userName, string email, int ordinal, int dicCastleId, int dicHeroId, int userId)
    {
        Ordinal = ordinal;
        UserId = userId;
        DicCastleId = dicCastleId;
        DicHeroId = dicHeroId;
        UserDTO userInfo = new UserDTO(userId,userName, email);
        UserInfo = userInfo;
    }

    public void ChangeOrdinal(int ordinal)
    {
        Ordinal = ordinal;
    }
}
