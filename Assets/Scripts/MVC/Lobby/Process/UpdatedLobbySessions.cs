using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatedLobbySessions : MonoBehaviour
{
    public List<LobbySession> GetLobbySessions(MessageInput message)
    {
        List<LobbySessionResult> newSessionsInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LobbySessionResult>>(message.body);
        List<LobbySession> lobbySessions = new List<LobbySession>();
        foreach (LobbySessionResult sessionInfo in newSessionsInfo)
        {
            string creatorName = "";

            foreach (var item in sessionInfo.participants)
            {
                if (item.UserName != null && item.UserName.Length > 0)
                {
                    creatorName = item.UserName;
                    break;
                }
            }

            LobbySession newSession = new LobbySession(creatorName, sessionInfo.name, sessionInfo.sessionId, sessionInfo.creatorId,
                sessionInfo.maxPlayerCount, sessionInfo.totalParticipantAmount,
                sessionInfo.templateId, sessionInfo.sizeId, sessionInfo.isAIAllowed,
                sessionInfo.status, sessionInfo.participants);

            lobbySessions.Add(newSession);
        }
        return lobbySessions;
    }
}