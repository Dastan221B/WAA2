using System.Collections;
using UnityEngine;

public class ExitFromLobbySessionParticipant : MonoBehaviour
{

    public ExitLobbySessionInfo GetExitLobbySessionInfo(MessageInput message)
    {
        return Newtonsoft.Json.JsonConvert.DeserializeObject<ExitLobbySessionInfo>(message.body);
    }
}