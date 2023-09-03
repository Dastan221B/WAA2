using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MVC.Lobby.View
{
    public class OnlineInGameView : MonoBehaviour
    {
        [SerializeField] private UserOnlineStatusItem _userOnlineStatusItemPrefab;
        [SerializeField] private Transform _parrent;
        private List<UserOnlineStatusItem> _userOnlineStatusItems = new List<UserOnlineStatusItem>();

        public void InitPlayer(UserDTO userDTO)
        {
            SetPlayerInOnline(userDTO);
        }

        public void InitPlayers(List<UserDTO> users)
        {
            foreach(var item in users)
            {
                SetPlayerInOnline(item);
            }
        }

        private void SetPlayerInOnline(UserDTO userDTO)
        {
            UserOnlineStatusItem userOnlineStatusItem = Instantiate(_userOnlineStatusItemPrefab, _parrent);
            userOnlineStatusItem.Init(userDTO.UserName);
            _userOnlineStatusItems.Add(userOnlineStatusItem);
        }

    }
}