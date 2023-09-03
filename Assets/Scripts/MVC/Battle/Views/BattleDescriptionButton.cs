using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace Assets.Scripts.MVC.Battle.Views
{
    public class BattleDescriptionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private string _textName;
        [SerializeField] private Image _buttonIcon;
        [SerializeField] [TextArea(4, 6)] private string _description;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private Image _descriptionIcon;
        [SerializeField] private TMP_Text _buttonNameText;
        public void OnPointerEnter(PointerEventData eventData)
        {
            _descriptionText.text = _description;
            _descriptionIcon.sprite = _buttonIcon.sprite;
            _buttonNameText.text = _textName;
            _descriptionIcon.enabled = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _descriptionText.text = "";
            _buttonNameText.text = "";
            _descriptionIcon.enabled = false;
        }
    }
}