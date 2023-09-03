using Assets.Scripts.MVC.TradeMVC;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.Game.GameProcces
{
    public class TradeStartedResultProcess 
    {
        private MoveHeroInfoWithMovePointsProcess _moveHeroInfoWithMovePointsProcess;
        private TradeStartedResult _currentTradeStartedResult;
        private TradeController _tradeController;
        private GameModel _gameModel;

        public TradeStartedResultProcess(GameModel gameModel,MoveHeroInfoWithMovePointsProcess moveHeroInfoWithMovePointsProcess,TradeController tradeController)
        {
            _gameModel = gameModel;
            _moveHeroInfoWithMovePointsProcess = moveHeroInfoWithMovePointsProcess;
            _tradeController = tradeController;
        }

        public void TradeStartedResultHandler(MessageInput message)
        {
            _currentTradeStartedResult = Newtonsoft.Json.JsonConvert.DeserializeObject<TradeStartedResult>(message.body);
            Debug.Log(_currentTradeStartedResult.requesterHeroInfo.mapObjectId);
            Debug.Log(_currentTradeStartedResult.receiverHeroInfo.mapObjectId);
            Debug.Log("Request trade " + _currentTradeStartedResult.requesterHeroInfo.mapObjectId + " " + _currentTradeStartedResult.receiverHeroInfo.mapObjectId);
            _moveHeroInfoWithMovePointsProcess.MoveHeroAndAndActionBeforeEndingMove(_currentTradeStartedResult.movementPath,
                _currentTradeStartedResult.heroId, _currentTradeStartedResult.movePointsLeft, () => EnterInTrade());
        }

        private void EnterInTrade()
        {
            Sprite requestIcon = null;
            Sprite receiverIcon = null;

            if(_gameModel.TryGetHeroModelObject(_currentTradeStartedResult.requesterHeroInfo.mapObjectId, out HeroModelObject heroModelObject1))
            {
                Debug.Log(_currentTradeStartedResult.requesterHeroInfo.dicHeroId);
                Debug.Log(heroModelObject1.Hero.HeroID);
                requestIcon = heroModelObject1.Hero.Icon;
            }

            if (_gameModel.TryGetHeroModelObject(_currentTradeStartedResult.receiverHeroInfo.mapObjectId, out HeroModelObject heroModelObject2))
            {
                Debug.Log(_currentTradeStartedResult.receiverHeroInfo.dicHeroId);
                Debug.Log(heroModelObject2.Hero.HeroID);
                receiverIcon = heroModelObject2.Hero.Icon;
            }
            _tradeController.EnterInTradeAndSetPariticipants(_currentTradeStartedResult.requesterHeroInfo, _currentTradeStartedResult.receiverHeroInfo, requestIcon, receiverIcon);
        }

        public void OpenTrade(HeroModelObject heroModelObject1 , HeroModelObject heroModelObject2)
        {
            Sprite requestIcon = null;
            Sprite receiverIcon = null;

            if (heroModelObject1 == null || heroModelObject2 == null)
                return;

            requestIcon = heroModelObject1.Hero.Icon;
            receiverIcon = heroModelObject2.Hero.Icon;
            _tradeController.EnterInTradeAndSetPariticipants(heroModelObject1.HeroObjectFullInfo, heroModelObject2.HeroObjectFullInfo, requestIcon, receiverIcon);
        }

    }
}