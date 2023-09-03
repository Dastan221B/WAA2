using System.Collections;
using UnityEngine;

public class LobbyMessageSender : MonoBehaviour
{
    private SessionUserDTO _sessionUserDTO;
    private ClientSender _clientSender;

    public void Init(ClientSender clientSender)
    {
        _clientSender = clientSender;
    }

    public void RegisterDTO(SessionUserDTO sessionUserDTO)
    {
        _sessionUserDTO = sessionUserDTO;
    }
    public void MessageSender(OutputLobbyHeaders header, string body)
    {
        _clientSender.SendMessageToServer((int)ClientHandlers.LOBBY, (int)header, body, _sessionUserDTO.SessionID);
    }
}