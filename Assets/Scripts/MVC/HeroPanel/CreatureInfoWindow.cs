using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Assets.Scripts.MVC.HeroPanel
{
    public class CreatureInfoWindow : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private GameObject _panel;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _damage;
        [SerializeField] private TMP_Text _deffence;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _helth;
        [SerializeField] private TMP_Text _speed;

        private void Awake()
        {
            _closeButton.onClick.AddListener(() => Close());
        }

        public void Open(DicCreatureDTO creatureDTO, Sprite icon)
        {
            _icon.sprite = icon;
            _name.text = creatureDTO.name;
            _damage.text = creatureDTO.attack.ToString();
            _deffence.text = creatureDTO.defense.ToString();
            _level.text = creatureDTO.level.ToString();
            _helth.text = creatureDTO.healthPoints.ToString();
            _speed.text = creatureDTO.speed.ToString();
            _panel.SetActive(true);
        }

        public void Close()
        {
            _panel.SetActive(false);
        }

    }
}