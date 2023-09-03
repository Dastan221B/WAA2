using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Assets.Scripts.MVC.Game.Views
{
    public class TurnInfoPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _sendStartTurnRequestButton;
        [SerializeField] private GameObject _closePanelButton;
        [SerializeField] private GameObject _panel;
        [SerializeField] private Image _color;
        [SerializeField] private TMP_Text _nameText;

        public void OpenForSelf(Color color , string name)
        {
            if(name == null || name.Length == 0)
            {
                _nameText.text = "Bot";
            }
            else
            {
                _nameText.text = name;
            }
            _color.color = color;
            _sendStartTurnRequestButton.SetActive(true);
            _closePanelButton.SetActive(false);
            _panel.SetActive(true);
        }

        public void OpenForOtherPlayer(Color color, string name)
        {
            _nameText.text = name;
            _color.color = color;
            _sendStartTurnRequestButton.SetActive(false);
            _closePanelButton.SetActive(true);
            _panel.SetActive(true);
        }

        public void Close()
        {
            _panel.SetActive(false);
        }

    }
}