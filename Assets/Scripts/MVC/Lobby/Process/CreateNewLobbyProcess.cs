using UnityEngine;

public class CreateNewLobbyProcess
{

    public bool TryCreateNewLobbySession(MessageInput messageInput, out LobbySession lobbySession)
    {
        CreateNewLobbySessionResult createNewLobbySessionsResult = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateNewLobbySessionResult>(messageInput.body);
        if (createNewLobbySessionsResult.result)
        { 
            Debug.Log("reateNewLobbySessionsResult.MaxPlayerCount " + createNewLobbySessionsResult.MaxPlayerCount);

            string creatorName = "";

            foreach (var item in createNewLobbySessionsResult.Participants)
            {
                if (item.UserName != null && item.UserName.Length > 0)
                {
                    creatorName = item.UserName;
                    break;
                }
            }

            LobbySession newSession = new LobbySession(creatorName, createNewLobbySessionsResult.Name, createNewLobbySessionsResult.sessionId,
                createNewLobbySessionsResult.CreatorId, createNewLobbySessionsResult.MaxPlayerCount, createNewLobbySessionsResult.TotalParticipantAmount,
                createNewLobbySessionsResult.TemplateId, createNewLobbySessionsResult.SizeId, createNewLobbySessionsResult.IsAIAllowed,
                createNewLobbySessionsResult.Status, createNewLobbySessionsResult.Participants);
            lobbySession = newSession;
            Debug.Log("createNewLobbySessionsResult.Participants[0] " + createNewLobbySessionsResult.Participants[0].DicCastleId + " " + createNewLobbySessionsResult.Participants[0].DicHeroId);
            return true;
            
        }
        else
        {
            lobbySession = default;
            return false;
        }
    }
}