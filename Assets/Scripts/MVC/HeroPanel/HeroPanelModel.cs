using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.MVC.HeroPanel
{
    public class HeroPanelModel
    {

        public event Action OnUpdatedArmy;
        public event Action OnUpdatedHeroStats;


        private ArmySlotInfo[] _army = new ArmySlotInfo[7];

        public IReadOnlyList<ArmySlotInfo> CastleArmy => _army;


        public string Name { get; private set; }
        public string Desctiption { get; private set; }
        public int Attack { get; private set; }
        public int Defence { get; private set; }
        public int Power { get; private set; }
        public int Knowledge { get; private set; }
        public Sprite Icon { get; private set; }

        private HeroModelObject _currentHeroModelObject;

        public void SetHeroModelObject(HeroModelObject heroModelObject , Sprite sprite)
        {
            _currentHeroModelObject = heroModelObject;
            Name = _currentHeroModelObject.Hero.Name;
            Attack = _currentHeroModelObject.Attack;
            Defence = _currentHeroModelObject.Defence;
            Power = _currentHeroModelObject.Power;
            Knowledge = _currentHeroModelObject.Knowledge;
            Icon = sprite;
            AddCreaturesToSlots(heroModelObject.ArmySlotInfos.ToList());
            OnUpdatedHeroStats?.Invoke();
        }


        private void AddCreaturesToSlots(List<ArmySlotInfo> armySlotInfos)
        {
            _army = new ArmySlotInfo[7];
            for (int i = 0; i < armySlotInfos.Count; i++)
            {
                _army[i] = armySlotInfos[i];
            }
            OnUpdatedArmy?.Invoke();
        }


        public void TrySetArmySlotInCastleSlotIcon(ArmySlotInfo armySlotInfo, int indexInQueue, int previousIndex)
        {
            if (indexInQueue > 7)
                return;

            _army[indexInQueue] = armySlotInfo;

            OnUpdatedArmy?.Invoke();
        }


        public bool TryGetArmyInSlots(int dicID, out ArmySlotInfo armySlotInfo)
        {
            foreach (var item in _army)
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