using System.Collections;
using UnityEngine;

public class ExitFromLobbySession : MonoBehaviour
{
    public bool TryExitFromLobbySession(MessageInput messageInput)
    {
        ExitLobbySessionResult exitResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ExitLobbySessionResult>(messageInput.body);
        if (exitResult.result)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}