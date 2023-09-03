using UnityEngine;

public class GetUpdatedFeeDictionary : MonoBehaviour
{
    public bool GetUpdatedFee(MessageInput message, out SetParticipantFeeInfo setParticipantFeeInfo)
    {
        setParticipantFeeInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<SetParticipantFeeInfo>(message.body);
        if (setParticipantFeeInfo.result)
        {
            return true;
        }
        return false;
    }
}