using Assets.Scripts.MVC.Game;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MVC.TradeMVC
{
    public class TradeView : MonoBehaviour
    {
        private Button _sumbitButton;
        private GameObject _panel;
        private TradeModel _tradeModel;
        private ModelCreatures _modelCreatures;
        private HeroCreaturesInventory _requesterHeroCreaturesInventory;
        private HeroCreaturesInventory _recieverHeroCreaturesInventory;

        private GameAndBattleCommandsSender _gameAndBattleCommandsSender;

        public void Init(HeroCreaturesInventory requester , HeroCreaturesInventory reciever, GameObject panel, Button sumbitButton)
        {
            _recieverHeroCreaturesInventory = reciever;
            _requesterHeroCreaturesInventory = requester;
            _panel = panel;
            _sumbitButton = sumbitButton;
            _sumbitButton.onClick.AddListener(Sumbit);
        }


        public void Init(ModelCreatures modelCreatures,TradeModel tradeModel , GameAndBattleCommandsSender gameAndBattleCommandsSender)
        {
            _modelCreatures = modelCreatures;
            _tradeModel = tradeModel;
            _gameAndBattleCommandsSender = gameAndBattleCommandsSender;
            _tradeModel.OnSettedTradeParticipants += DisplayStatsHero;
            _tradeModel.OnSettedTradeParticipants += DisplayArmySlots;
            _tradeModel.OnSettedTradeParticipants += OpenPanel;
            _tradeModel.OnSumbitTrade += ClosePanel;
            _tradeModel.OnSumbitTrade += SumbitTradeHandler;
        }

        private void Sumbit()
        {
            _gameAndBattleCommandsSender.SendSumbitTrade(_tradeModel.TradeRequesterId, _tradeModel.TradeReceiverId, 
                _requesterHeroCreaturesInventory.GetArmySlots(), _recieverHeroCreaturesInventory.GetArmySlots());
        }

        private void OpenPanel()
        {
            _panel.SetActive(true);
        }

        private void ClosePanel()
        {
            _panel.SetActive(false);
        }

        public void DisplayArmySlots()
        {
            Debug.Log(_requesterHeroCreaturesInventory);
            foreach(var item in _tradeModel.TradeRequesterArmy)
            {
                _requesterHeroCreaturesInventory.SetCreatureSlots(_modelCreatures.GetIconById((int)item.dicCreatureId - 1), item);
            }
            _requesterHeroCreaturesInventory.SetHeroIcon(_tradeModel.RequesterIcon);
            foreach (var item in _tradeModel.TradeReceiverArmy)
            {
                _recieverHeroCreaturesInventory.SetCreatureSlots(_modelCreatures.GetIconById((int)item.dicCreatureId - 1), item);
            }
            _recieverHeroCreaturesInventory.SetHeroIcon(_tradeModel.RecieverIcon);
        }
        public void DisplayStatsHero()
        {

        }
        public void SumbitTradeHandler()
        {
            _requesterHeroCreaturesInventory.ResetInventory();
            _recieverHeroCreaturesInventory.ResetInventory();
        }

    }
}