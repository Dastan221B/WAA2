using System.Collections;
using UnityEngine;

public class AddJoiningParicipant
{
    public SessionParticipant GetNewParticipant(MessageInput message)
    {
        LobbySessionJoinNotification joinInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<LobbySessionJoinNotification>(message.body);
        return new SessionParticipant(joinInfo.userName, joinInfo.email, joinInfo.ordinal,0, 0, joinInfo.userId);
    }

}