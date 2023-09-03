using System.Collections;
using UnityEngine;

public class EnterInLobbySession 
{
    private IUserDTO _userDTO;

    public EnterInLobbySession(IUserDTO userDTO)
    {
        _userDTO = userDTO;
    }

    public bool TryGetEnteredLobbySession(MessageInput message, out SessionParticipant sessionParticipant , out EnterLobbySessionResult enterLobbySessionResult)
    {
        sessionParticipant = null;
        enterLobbySessionResult = Newtonsoft.Json.JsonConvert.DeserializeObject<EnterLobbySessionResult>(message.body);
        if (enterLobbySessionResult.result)
        {
            sessionParticipant = new SessionParticipant(_userDTO.UserDTO.UserName, _userDTO.UserDTO.Email,
                enterLobbySessionResult.ordinal, 0, 0, 0);
            return true;
        }
        return false;
    }
}