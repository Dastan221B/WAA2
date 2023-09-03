using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MVC.CastleSlots
{
    public class SlotsView : MonoBehaviour
    {
        private List<CreatureSlot> _castleCreaturesSlots;
        private List<CreatureSlot> _garrisonCreaturesSlots;

        private SlotsModel _slotsModel;
        private ModelCreatures _modelCreatures; 

        public void InitSlots(List<CreatureSlot> castleCreaturesSlots, List<CreatureSlot> garrisonCreaturesSlots)
        {
            _castleCreaturesSlots = castleCreaturesSlots;
            _garrisonCreaturesSlots = garrisonCreaturesSlots;

            for (int i = 0; i < _castleCreaturesSlots.Count; i++)
                _castleCreaturesSlots[i].SetSlotID(i);
            for (int i = 0; i < _garrisonCreaturesSlots.Count; i++)
                _garrisonCreaturesSlots[i].SetSlotID(i);
        }

        public void Init(SlotsModel slotsModel, ModelCreatures modelCreatures)
        {
            _slotsModel = slotsModel;
            _modelCreatures = modelCreatures;
        }

        public void UpdateCastleCreaturesSlots()
        {
            foreach (var item in _castleCreaturesSlots)
                item.ResetSlot();

            for(int i = 0; i < _slotsModel.CastleArmy.Count; i++)
            {
                if(_slotsModel.CastleArmy[i] != null)
                {
                    _slotsModel.CastleArmy[i].stackSlot = _castleCreaturesSlots[i].SlotID;
                    _castleCreaturesSlots[i].SetCreatureInSlot(_modelCreatures.GetIconById((int)_slotsModel.CastleArmy[i].dicCreatureId - 1), _slotsModel.CastleArmy[i]);
                }
            }
        }
        public void UpdateGarrisonCreaturesSlots()
        {
            foreach (var item in _garrisonCreaturesSlots)
                item.ResetSlot();

            for (int i = 0; i < _slotsModel.GarrisonArmy.Count; i++)
            {
                if (_slotsModel.GarrisonArmy[i] != null)
                {
                    _slotsModel.GarrisonArmy[i].stackSlot = _garrisonCreaturesSlots[i].SlotID;
                    _garrisonCreaturesSlots[i].SetCreatureInSlot(_modelCreatures.GetIconById((int)_slotsModel.GarrisonArmy[i].dicCreatureId - 1), _slotsModel.GarrisonArmy[i]);
                }
            }
        }

    }
}