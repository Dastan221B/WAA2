using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Client.CommonDTO
{
    public class CommonMessageSender : MonoBehaviour
    {

        private SessionUserDTO _sessionUserDTO;
        private ClientSender _clientSender;

        public enum OutputCommonHeaders
        {
            GET_CASTLE_LIST = 0,
            GET_HERO_LIST_REQUEST = 1,
            GET_BUILDING_LIST_REQUEST = 2,
            GET_CREATURE_LIST_REQUEST = 3
        }


        public void Init(ClientSender clientSender)
        {
            _clientSender = clientSender;
        }

        public void RegisterDTO(SessionUserDTO sessionUserDTO)
        {
            _sessionUserDTO = sessionUserDTO;
        }

        public void SendAllRequestForCommonData(SessionUserDTO sessionUserDTO)
        {
            for (int i = 0; i < 4; i++)
            {
                SendMessage((OutputCommonHeaders)i);
            }
        }

        private void SendMessage(OutputCommonHeaders header)
        {
            EmptyObject body = new EmptyObject();
            _clientSender.SendMessageToServer((int)ClientHandlers.COMMON,(int)header, Newtonsoft.Json.JsonConvert.SerializeObject(body), _sessionUserDTO.SessionID);
        }
    }
}