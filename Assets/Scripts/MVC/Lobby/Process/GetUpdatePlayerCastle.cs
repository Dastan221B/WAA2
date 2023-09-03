using System.Collections;
using UnityEngine;

public class GetUpdatePlayerCastle : MonoBehaviour
{
    public bool TryGetUpdatedPlayerCastle(MessageInput message, out SetPlayerCastleResult setPlayerCastleResult)
    {
        setPlayerCastleResult = Newtonsoft.Json.JsonConvert.DeserializeObject<SetPlayerCastleResult>(message.body);
        if (setPlayerCastleResult.result)
        {
            return true;
        }
        return false;
    }
}