using System;

[Serializable]
public class SetPlayerHeroRequest : SessionIdBasedRequest
{
    public long dicHeroId;
    public int playerOrdinal;

    public SetPlayerHeroRequest(string sessionId, long dicHeroId, int playerOrdinal) : base(sessionId)
    {
        this.sessionId = sessionId;
        this.dicHeroId = dicHeroId;
        this.playerOrdinal = playerOrdinal;
    }
}