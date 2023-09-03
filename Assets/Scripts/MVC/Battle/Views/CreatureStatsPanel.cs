using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Assets.Scripts.MVC.Battle.Views
{
    public class CreatureStatsPanel : MonoBehaviour
    {
        [SerializeField] private Transform _enemySidePanel;
        [SerializeField] private Transform _playerSidePanel;
        [SerializeField] private GameObject _panel;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _attackText;
        [SerializeField] private TMP_Text _deffenceText;
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _knowleadge;
        [SerializeField] private TMP_Text _minDamageText;
        [SerializeField] private TMP_Text _maxDamageText;
        [SerializeField] private TMP_Text _attackTypeText;
        [SerializeField] private TMP_Text _movmentTypeText;
        [SerializeField] private TMP_Text _speedText;

        public void Open(DicCreatureDTO dicCreatureDTO,CreatureSide creatureSide ,Sprite icon, int attack, int deffence , int health , int knowleadge)
        {
            if(creatureSide == CreatureSide.Enemy)
            {
                _panel.transform.position = _playerSidePanel.position;
            }
            else
            {
                _panel.transform.position = _enemySidePanel.position;
            }
            _icon.sprite = icon;
            _attackText.text = "Attack: " + dicCreatureDTO.attack;
            _healthText.text = "Health: " + dicCreatureDTO.healthPoints;
            //_knowleadge.text = "Knowleadge: " + knowleadge;
            _knowleadge.text = "";
            _nameText.text = dicCreatureDTO.name;
            _maxDamageText.text = "Max Damage: " + dicCreatureDTO.maxDmg;
            _minDamageText.text = "Min Damage: " + dicCreatureDTO.minDmg;
            _attackTypeText.text = "Attack type: " + dicCreatureDTO.attackType.ToString();
            _movmentTypeText.text = "Move type: " + dicCreatureDTO.movementType.ToString();
            _speedText.text = "Speed: " + dicCreatureDTO.speed;
            _levelText .text = "Level: " + dicCreatureDTO.level;
            _panel.SetActive(true);
        }

        public void Close()
        {
            _panel.SetActive(false);
        }
    }
}