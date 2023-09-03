using UnityEngine;
using TMPro;

namespace Assets.Scripts.MVC.Lobby.View
{
    public class UserOnlineStatusItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _username;


        public void Init(string username)
        {
            _username.text = username;
        }
    }
}