public interface IBroadcastChangeOrdinal
{
    void BroadcastChangeParticipantOrdinalRequest(int currentOrdinal, int newOrdinal, string sessionId);
}