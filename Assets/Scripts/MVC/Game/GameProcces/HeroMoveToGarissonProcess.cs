using Assets.Scripts.MVC.CastleMVC;
using Assets.Scripts.MVC.CastleSlots;
using Assets.Scripts.MVC.Game.Views;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.Game.GameProcces
{
    public class HeroMoveToGarissonProcess : MonoBehaviour
    {

        private GameModel _gameModel;
        private SlotsModel _slotsModel;
        private GameTurnView _turnView;
        private CastleModel _castleModel;

        public HeroMoveToGarissonProcess(CastleModel castleModel, GameTurnView gameTurnView, GameModel gameModel, SlotsModel slotsModel)
        {
            _castleModel = castleModel;
            _gameModel = gameModel;
            _slotsModel = slotsModel;
            _turnView = gameTurnView;
        }

        public void HeroMoveToGarisson(MessageInput message)
        {
            MoveHeroToGarrisonResult moveHeroToCaslteResult = Newtonsoft.Json.JsonConvert.DeserializeObject<MoveHeroToGarrisonResult>(message.body);
            if (_gameModel.TryGetHeroModelObject(moveHeroToCaslteResult.heroId, out HeroModelObject heroModelObject))
            {
                heroModelObject.gameObject.SetActive(true);
                _slotsModel.ResetCastleCreature();
                _slotsModel.AddCreaturesToGarrisonSlot(moveHeroToCaslteResult.heroInGarrison.army);
                heroModelObject.HeroObjectFullInfo.army = moveHeroToCaslteResult.heroInGarrison.army;
                heroModelObject.ExitFromCastle();
                _castleModel.SetHeroInGarrison(heroModelObject.HeroObjectFullInfo);
                _castleModel.SetHeroInCastle(null);
                _turnView.ResetDisplayHeroes();
            }
        }
    }
}