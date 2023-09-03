using System;
using System.Collections.Generic;

[Serializable]
public class Player : SessionParticipant
{
    public int turnSeconds { get; set; }
    public int beforeBattleTurnSeconds { get; set; }
    public int daysToCaptureCastle { get; set; }
    public PlayerStatus playerStatus { get; set; }

    public Dictionary<string, HeroObjectFullInfo> heroesInfo { get; set; }
    public Dictionary<string, CastleObjectFullInfo> castlesInfo { get; set; }

    public Player(string userName, string email, int ordinal, int dicCastleId, int dicHeroId, long socketId)
        : base(userName, email, ordinal, dicCastleId, dicHeroId, (int)socketId)
    {

    }
}
