﻿using Assets.Scripts.MVC.CastleMVC.View;
using Assets.Scripts.MVC.CastleSlots;
using Assets.Scripts.MVC.HeroPanel;
using Assets.Scripts.MVC.TradeMVC;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.Game.GameProcces
{
    public class SubmitTradeResultProcess
    {
        private GameModel _gameModel;
        private TradeController _tradeController;
        private SlotsModel _slotModel;
        private CastleView _castleView;
        private HeroPanelView _heroPanelView;

        public SubmitTradeResultProcess(HeroPanelView heroPanelView,SlotsModel slotsModel, CastleView castleView,GameModel gameModel , TradeController tradeController)
        {
            _heroPanelView = heroPanelView;
            _slotModel = slotsModel;
            _castleView = castleView;
            _gameModel = gameModel;
            _tradeController = tradeController;
        }

        public void SubmitTradeResultHandler(MessageInput message)
        {
            SubmitTradeResult submitTradeResult = Newtonsoft.Json.JsonConvert.DeserializeObject<SubmitTradeResult>(message.body);
            if (submitTradeResult.result)
            {
                _tradeController.ExitFromTradePanel();
                if (_gameModel.TryGetHeroModelObject(submitTradeResult.requesterHeroObjectId,out HeroModelObject heroModelObject1))
                {
                    heroModelObject1.SetArmySlots(submitTradeResult.requesterArmy);
                    if (_castleView.OpenUI)
                    {
                        _slotModel.AddCreaturesToGarrisonSlot(submitTradeResult.requesterArmy);
                    }
                }
                if (_gameModel.TryGetHeroModelObject(submitTradeResult.receiverHeroObjectId, out HeroModelObject heroModelObject2))
                {
                    heroModelObject2.SetArmySlots(submitTradeResult.receiverArmy);
                    if (_castleView.OpenUI)
                    {
                        _slotModel.AddCreaturesToCastleSlot(submitTradeResult.receiverArmy);
                    }
                }
                _heroPanelView.SetGameBarHero(_gameModel.SelectedHero, _gameModel.SelectedHero.Hero.Icon);
            }

        }
    }
}