using UnityEngine;

public class GetUpdatedParticipantSlotOrdinal : MonoBehaviour
{

    public bool GetUpdatedOrdinal(MessageInput message, out SetPlayerOrdinalResult setPlayerOrdinalResult)
    {
        setPlayerOrdinalResult = Newtonsoft.Json.JsonConvert.DeserializeObject<SetPlayerOrdinalResult>(message.body);
        if (setPlayerOrdinalResult.result)
        {
            return true;
        }
        return false;
    }
}