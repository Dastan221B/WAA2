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
                    _garrisonArmy[i] = armySlotInfos.FirstOrDefault(p => p.stackSlot == i); ;
                }
            }
            OnUpdatedGarrisonArmy?.Invoke();

        }

        public void AddCreaturesToCastleSlot(List<ArmySlotInfo> armySlotInfos)
        {

            _castleArmy = new ArmySlotInfo[7];
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
                _castleArmy[i] = armySlotInfos.FirstOrDefault(p => p.stackSlot == i);
            }

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
                //bool isHave = false;

                //ArmySlotInfo curArmySlot = null;
                //foreach (var slot in _castleArmy)
                //{
                //    if (slot != null)
                //    {
                //        if (slot.dicCreatureId == armySlotInfo.dicCreatureId)
                //        {
                //            curArmySlot = slot;
                //            isHave = true;
                //        }
                //    }
                //}
                //if (isHave)
                //{
                //    curArmySlot.amount += armySlotInfo.amount;
                //}
                //else
                //{
                _castleArmy[indexInQueue] = armySlotInfo;
                //}
            }
            else
            {
                Debug.Log("21check");
                if (armySlotInfo.dicCreatureId == _castleArmy[indexInQueue].dicCreatureId)
                {
                    if (previousSlotTypes == SlotTypes.Castle)
                        _castleArmy[previousIndex] = null;
                    else
                        _garrisonArmy[previousIndex] = null;

                    _castleArmy[indexInQueue].amount += armySlotInfo.amount;
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
                //bool isHave = false;
                //ArmySlotInfo curArmySlot = null;
                //foreach (var slot in _garrisonArmy)
                //{
                //    if (slot != null)
                //    {
                //        if (slot.dicCreatureId == armySlotInfo.dicCreatureId)
                //        {
                //            curArmySlot = slot;
                //            isHave = true;
                //        }
                //    }
                //}
                //if (isHave)
                //{
                //    curArmySlot.amount += armySlotInfo.amount;
                //}

                _garrisonArmy[indexInQueue] = armySlotInfo;


            }
            else
            {
                Debug.Log("21check");
                if (_garrisonArmy[indexInQueue].dicCreatureId == armySlotInfo.dicCreatureId)
                {

                    if (previousSlotTypes == SlotTypes.Garrison)
                        _garrisonArmy[previousIndex] = null;
                    else
                        _castleArmy[previousIndex] = null;
                    _garrisonArmy[indexInQueue].amount += armySlotInfo.amount;
                }
            }
            OnUpdatedGarrisonArmy?.Invoke();
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
        public void Divide(CreatureSlot currentSlot) 
        {
            if (currentSlot == null)
                return;
            if (currentSlot.SlotTypes == SlotTypes.Castle)
            {
                int count = 0;
                int Amount = currentSlot.ArmySlotInfo.amount;
                foreach (var item in _castleArmy)
                {
                    if (item != null && item != currentSlot.ArmySlotInfo)
                        count++;
                }
                Debug.Log(currentSlot.ArmySlotInfo.amount + count + " Count" + count);
                if (currentSlot.ArmySlotInfo.amount + count > 7)
                    return;
                Debug.Log("its Work blyatCastle");
                for (int i = 0; i < _castleArmy.Length; i++)
                {
                    Debug.Log(" _castleArmy[i].dicCreatureId " + _castleArmy[i].dicCreatureId + " currentSlot.ArmySlotInfo.dicCreatureId " + currentSlot.ArmySlotInfo.dicCreatureId);
                    if (_castleArmy[i].dicCreatureId == currentSlot.ArmySlotInfo.dicCreatureId)
                    {
                        Debug.Log("its Work blyat2");
                        if (_castleArmy[i + 1] != null)
                        {
                            for(int j =  i + currentSlot.ArmySlotInfo.amount; j < count + currentSlot.ArmySlotInfo.amount; j++)
                            {
                                _castleArmy[j] = _castleArmy[i + 1];
                            }
                        }
                        for (int j = 0; j < Amount; j++)
                        {
                            Debug.Log("ItsWorkNahu");
                            _castleArmy[i + j] = currentSlot.ArmySlotInfo;
                            _castleArmy[i + j].amount = 1;
                        }
                        break;
                    }
                }
                OnUpdatedCastleArmy?.Invoke();
            }
            else
            {
                int count = 0;
                int Amount = currentSlot.ArmySlotInfo.amount;
                foreach (var item in _garrisonArmy)
                {
                    if (item != null && item != currentSlot.ArmySlotInfo)
                        count++;
                }

                Debug.Log(currentSlot.ArmySlotInfo.amount + count + " Count");
                if (currentSlot.ArmySlotInfo.amount + count > 7)
                    return;
                for (int i = 0; i < _garrisonArmy.Length; i++)
                {
                    if (_garrisonArmy[i].dicCreatureId == currentSlot.ArmySlotInfo.dicCreatureId)
                    {
                        Debug.Log("its Work blyatGarrison");
                        if (_garrisonArmy[i + 1] != null)
                        {
                            for (int j = i + currentSlot.ArmySlotInfo.amount; j < count + currentSlot.ArmySlotInfo.amount; j++)
                            {
                                _garrisonArmy[j] = _garrisonArmy[i + 1];
                            }
                        }
                        for (int j = 0; j < Amount; j++)
                        {
                            _garrisonArmy[i + j] = currentSlot.ArmySlotInfo;
                            _garrisonArmy[i + j].amount = 1;
                        }
                        break;
                    }
                }
                OnUpdatedGarrisonArmy?.Invoke();
            }
        }
    }
}