using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.MVC.ChatMVC
{
    public class ChatItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;


        public void SetText(string text)
        {
            _text.text = text;
        }

    }
}