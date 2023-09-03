using System;

[Serializable]
public class SetPlayerCastleRequest : SessionIdBasedRequest
{
    public int dicCastleId;
    public int playerOrdinal;

    public SetPlayerCastleRequest(string sessionId, int dicCastleId, int playerOrdinal) : base(sessionId)
    {
        this.sessionId = sessionId;
        this.dicCastleId = dicCastleId;
        this.playerOrdinal = playerOrdinal;
    }
}
