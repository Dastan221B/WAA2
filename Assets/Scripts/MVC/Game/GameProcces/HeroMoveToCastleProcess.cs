using Assets.Scripts.MVC.CastleSlots;
using Assets.Scripts.MVC.Game.Views;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.Game.GameProcces
{
    public class HeroMoveToCastleProcess : MonoBehaviour
    {
        private GameModel _gameModel;
        private SlotsModel _slotsModel;
        private GameTurnView _turnView;

        public HeroMoveToCastleProcess(GameTurnView gameTurnView,GameModel gameModel , SlotsModel slotsModel)
        {
            _gameModel = gameModel;
            _slotsModel = slotsModel;
            _turnView = gameTurnView;
        }

        public void HeroMoveToCastle(MessageInput message)
        {
            MoveHeroToCaslteResult moveHeroToCaslteResult = Newtonsoft.Json.JsonConvert.DeserializeObject<MoveHeroToCaslteResult>(message.body);
            if (_gameModel.TryGetHeroModelObject(moveHeroToCaslteResult.heroId, out HeroModelObject heroModelObject))
            {
                heroModelObject.gameObject.SetActive(false);
                _slotsModel.ResetGarissonCreature();
                _slotsModel.AddCreaturesToCastleSlot(moveHeroToCaslteResult.heroInCastle.army);
                heroModelObject.HeroObjectFullInfo.army = moveHeroToCaslteResult.heroInCastle.army;
                heroModelObject.EnterInCastle();
                _gameModel.ReplaceToLastPlaceInTurn(heroModelObject);
                _turnView.ResetDisplayHeroes();
            }
        }

    }
}