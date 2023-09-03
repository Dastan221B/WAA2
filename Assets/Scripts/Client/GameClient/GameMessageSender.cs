using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Client.GameClient
{
    public class GameMessageSender : MonoBehaviour
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

        public void SendMessage(OutputGameHeaders header, string body)
        {
            _clientSender.SendMessageToServer((int)ClientHandlers.GAME, (int)header, body, _sessionUserDTO.SessionID);
        }
    }
}