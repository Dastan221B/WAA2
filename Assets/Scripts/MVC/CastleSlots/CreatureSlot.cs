using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Assets.Scripts.MVC.CastleSlots
{
    public class CreatureSlot : MonoBehaviour
    {
        [SerializeField] private SlotTypes _slotTypes;
        [SerializeField] private TMP_Text _count;
        [SerializeField] private Image _icon;

        public int SlotID { get; private set; }
        public ArmySlotInfo ArmySlotInfo { get; private set; }
        public SlotTypes SlotTypes => _slotTypes;

        public void SetSlotID(int id)
        {
            SlotID = id;
        }

        public void SetCreatureInSlot(Sprite icon , ArmySlotInfo armySlotInfo)
        {
            _icon.color = Color.white;
            ArmySlotInfo = armySlotInfo;
            if(_count != null)
                _count.text = armySlotInfo.amount.ToString();
            _icon.sprite = icon;
        }

        public void ResetSlot()
        {
            _icon.color = new Color(0, 0, 0, 0);
            if (_count != null)
                _count.text = "";
            ArmySlotInfo = null;
        }

    }
}