using System.Collections.Generic;

public interface IBroadcastUpdatedFee
{
    void BroadcastUpdatedFeeRequest(Dictionary<int, int> participantFeeMap, string sessionID);
}