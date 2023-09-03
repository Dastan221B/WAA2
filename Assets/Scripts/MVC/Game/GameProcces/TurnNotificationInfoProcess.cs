using System.Collections;
using UnityEngine;

public class TurnNotificationInfoProcess : MonoBehaviour
{
    public bool TryGetTurnInfo(MessageInput messageInput , out TurnNotificationInfo turnNotificationInfo)
    {
        turnNotificationInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<TurnNotificationInfo>(messageInput.body);
        if (turnNotificationInfo.result)
        {
            return true;
        }
        return false;
    }
    
}