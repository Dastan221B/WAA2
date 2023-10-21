using System.Linq;
using System;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.MVC.Game;
using Assets.Scripts.MVC.CastleMVC;

namespace Assets.Scripts.MVC.CastleSlots
{
    public class SlotsModel : MonoBehaviour
    {
        public event Action OnUpdatedCastleArmy;
        public event Action OnUpdatedGarrisonArmy;

        private ArmySlotInfo[] _castleArmy = new ArmySlotInfo[7];
        private ArmySlotInfo[] _garrisonArmy = new ArmySlotInfo[7];

        public IReadOnlyList<ArmySlotInfo> CastleArmy => _castleArmy;
        public IReadOnlyList<ArmySlotInfo> GarrisonArmy => _garrisonArmy;


        public int GarrisonArmyCount => new List<ArmySlotInfo>(_garrisonArmy).ExcludeNull().Count;

        public void AddCreaturesToGarrisonSlot(List<ArmySlotInfo> armySlotInfos)
        {
            _garrisonArmy = new ArmySlotInfo[7];

            if (armySlotInfos != null)
            {
                //for (int i = 0; i < armySlotInfos.Count; i++)
                //{
                //    for (int j = i + 1; j < armySlotInfos.Count; j++)
                //    {
                //        if (armySlotInfos[i].dicCreatureId == armySlotInfos[j].dicCreatureId)
                //        {
                //            armySlotInfos[i].amount += armySlotInfos[j].amount;
                //            armySlotInfos.RemoveAt(j);
                //        }
                //    }
                //}
                for (int i = 0; i < armySlotInfos.Count; i++)
                {
                    _garrisonArmy[i] = armySlotInfos[i];
                }
            }
            OnUpdatedGarrisonArmy?.Invoke();

        }

        public void AddCreaturesToCastleSlot(List<ArmySlotInfo> armySlotInfos)
        {

            _castleArmy = new ArmySlotInfo[7];
            for (int i = 0; i < armySlotInfos.Count; i++)
            {
                for (int j = i + 1; j < armySlotInfos.Count; j++)
                {
                    if (armySlotInfos[i].dicCreatureId == armySlotInfos[j].dicCreatureId)
                    {
                        armySlotInfos[i].amount += armySlotInfos[j].amount;
                        armySlotInfos.RemoveAt(j);
                    }
                }
            }
            _castleArmy = armySlotInfos.ToArray();
            //for (int i = 0; i < armySlotInfos.Count; i++)
            //{
            //    _castleArmy[i] = armySlotInfos[i];
            //}
            OnUpdatedCastleArmy?.Invoke();
        }

        public void ResetGarissonCreature()
        {
            _garrisonArmy = new ArmySlotInfo[7];
            OnUpdatedGarrisonArmy?.Invoke();
        }

        public void ResetCastleCreature()
        {
            _castleArmy = new ArmySlotInfo[7];
            OnUpdatedCastleArmy?.Invoke();
        }


        public void TrySetArmySlotInCastleSlotIcon(ArmySlotInfo armySlotInfo, int indexInQueue, int previousIndex, SlotTypes previousSlotTypes)
        {
            if (indexInQueue > 7)
                return;

            if (_castleArmy[indexInQueue] == null)
            {
                if (previousSlotTypes == SlotTypes.Castle)
                    _castleArmy[previousIndex] = null;
                else
                    _garrisonArmy[previousIndex] = null;
                bool isHave = false;
                ArmySlotInfo curArmySlot = null;
                foreach (var slot in _castleArmy)
                {
                    if (slot != null)
                    {
                        if (slot.dicCreatureId == armySlotInfo.dicCreatureId)
                        {
                            curArmySlot = slot;
                            isHave = true;
                        }
                    }
                }
                if (isHave)
                {
                    curArmySlot.amount += armySlotInfo.amount;
                }
                else
                {
                    _castleArmy[indexInQueue] = armySlotInfo;
                }
            }
            OnUpdatedCastleArmy?.Invoke();
        }

        public void TrySetArmySlotInGarissonSlotIcon(ArmySlotInfo armySlotInfo, int indexInQueue, int previousIndex, SlotTypes previousSlotTypes)
        {
            if (indexInQueue > 7)
                return;
            if (_garrisonArmy[indexInQueue] == null)
            {
                if (previousSlotTypes == SlotTypes.Garrison)
                    _garrisonArmy[previousIndex] = null;
                else
                    _castleArmy[previousIndex] = null;
                bool isHave = false;
                ArmySlotInfo curArmySlot = null;
                foreach (var slot in _garrisonArmy)
                {
                    if (slot != null)
                    {
                        if (slot.dicCreatureId == armySlotInfo.dicCreatureId)
                        {
                            curArmySlot = slot;
                            isHave = true;
                        }
                    }
                }
                if (isHave)
                {
                    curArmySlot.amount += armySlotInfo.amount;
                }
                else
                {
                    _garrisonArmy[indexInQueue] = armySlotInfo;
                }
                OnUpdatedGarrisonArmy?.Invoke();
            }
        }

            public bool TryGetArmyInSlots(int dicID, out ArmySlotInfo armySlotInfo)
            {
                foreach (var item in _castleArmy)
                {
                    if (item != null && item.dicCreatureId == dicID)
                    {
                        armySlotInfo = item;
                        return true;
                    }
                }
                armySlotInfo = null;
                return false;
            }
        }
}