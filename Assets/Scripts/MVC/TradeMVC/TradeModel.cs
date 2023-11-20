using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets.Scripts.MVC.TradeMVC
{
    public class TradeModel
    {
        public string TradeReceiverId { get; private set; }
        public string TradeRequesterId { get; private set; }
        public Sprite RecieverIcon { get; private set; }
        public Sprite RequesterIcon { get; private set; }
        

        private List<ArmySlotInfo> _tradeRequesterArmy;
        private List<ArmySlotInfo> _tradeReceiverArmy;

        public event Action OnSettedTradeParticipants;
        public event Action OnSumbitTrade;
        public int TradeRequesterArmyCount => _tradeRequesterArmy.Count;
        public int TradeReceiverArmyCount => _tradeReceiverArmy.Count;
        public IEnumerable<ArmySlotInfo> TradeRequesterArmy => _tradeRequesterArmy;
        public IEnumerable<ArmySlotInfo> TradeReceiverArmy => _tradeReceiverArmy;

        private TradeCreatureSlot _currentTradeCreatureSlot;
        private ModelCreatures _modelCreatures;


        public TradeModel(ModelCreatures modelCreatures)
        {
            _modelCreatures = modelCreatures;
        }

        public void PickTradeCreatureSlot(TradeCreatureSlot tradeCreatureSlot)
        {
            Debug.Log("PickTradeCreatureSlot");
            if(_currentTradeCreatureSlot == null && tradeCreatureSlot.ArmySlotInfo != null)
            {
                _currentTradeCreatureSlot = tradeCreatureSlot;
            }
            else if (_currentTradeCreatureSlot != null && tradeCreatureSlot.ArmySlotInfo == null)
            {
                tradeCreatureSlot.SetCreatureInSlot(_modelCreatures.GetIconById((int)_currentTradeCreatureSlot.ArmySlotInfo.dicCreatureId - 1), _currentTradeCreatureSlot.ArmySlotInfo);
                _currentTradeCreatureSlot.ResetSlot();
                _currentTradeCreatureSlot = null;
            }
        }


        public void SumbitTrade()
        {
            if(TradeReceiverArmyCount > 0 && TradeRequesterArmyCount > 0)
                OnSumbitTrade?.Invoke();
        }

        public void SetTradeParticipants(HeroObjectFullInfo requesterHero , HeroObjectFullInfo receiverHero , Sprite requestIcon , Sprite receiverIcon, HeroStats requesterHeroStats, HeroStats receiverHeroStats )
        {
            RequesterIcon = requestIcon;
            RecieverIcon = receiverIcon;
            TradeRequesterId = requesterHero.mapObjectId;
            TradeReceiverId = receiverHero.mapObjectId;

            _tradeReceiverArmy = receiverHero.army;
            _tradeRequesterArmy = requesterHero.army;
            string receiverHeroName = "";
            string requesterHeroName = "";
            if(MonoBehaviour.FindAnyObjectByType<GameController>().GameModel.TryGetHeroModelObject(receiverHero.mapObjectId, out HeroModelObject heroModelObject))
            {
                receiverHeroName = heroModelObject.Hero.Name;
            }
            if (MonoBehaviour.FindAnyObjectByType<GameController>().GameModel.TryGetHeroModelObject(requesterHero.mapObjectId, out HeroModelObject heroModelObject2))
            {
                requesterHeroName = heroModelObject2.Hero.Name;
            }
            receiverHeroStats.SetStatsForHero(receiverHero.attack, receiverHero.defence, receiverHero.power, receiverHero.knowledge, receiverHeroName);
            requesterHeroStats.SetStatsForHero(requesterHero.attack, requesterHero.defence, requesterHero.power, requesterHero.knowledge, requesterHeroName);
            OnSettedTradeParticipants?.Invoke();
        }


    }
}