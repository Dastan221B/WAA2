using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.MVC.CastleSlots;

namespace Assets.Scripts.MVC.HeroPanel
{
    public class HeroPanelStatsWindow : MonoBehaviour
    {
        [SerializeField] private ModelCreatures _modelCreatures;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _attack;
        [SerializeField] private TMP_Text _defence;
        [SerializeField] private TMP_Text _power;
        [SerializeField] private TMP_Text _knowledge;
        [SerializeField] private List<CreatureSlot> _creatureSlots;

        private void Awake()
        {
            if (_creatureSlots.Count > 0)
            {
                foreach (var item in _creatureSlots)
                    item.ResetSlot();
            }
        }

        public void UpdateSlots(IReadOnlyList<ArmySlotInfo> armySlotInfos)
        {
            //if (_creatureSlots.Count > 0)
            //{
            //    for (int i = 0; i < armySlotInfos.Count; i++)
            //    {
            //        if (armySlotInfos[i] != null)
            //        {
            //            _creatureSlots[i].SetCreatureInSlot(_modelCreatures.GetIconById((int)armySlotInfos[i].dicCreatureId - 1), armySlotInfos[i]);
            //        }
            //    }
            //}
        }

        public void SetValues(Sprite icon,string name , string desctiption , int attack , int defence , int power, int knowledge , IReadOnlyList<ArmySlotInfo> armySlotInfos)
        {
            _icon.enabled = true;
            _icon.sprite = icon;
            _name.text = name;
            _attack.text = attack.ToString();
            _defence.text = defence.ToString();
            _power.text = power.ToString();
            _knowledge.text = knowledge.ToString();
            if (_creatureSlots.Count > 0)
            {
                foreach (var item in _creatureSlots)
                    item.ResetSlot();
            }
            if (armySlotInfos != null && _creatureSlots.Count > 0)
            {
                for (int i = 0; i < armySlotInfos.Count; i++)
                {
                    if (armySlotInfos[i] != null)
                    {
                        _creatureSlots[i].SetCreatureInSlot(_modelCreatures.GetIconById((int)armySlotInfos[i].dicCreatureId - 1), armySlotInfos[i]);
                    }
                }
            }

        }


    }
}