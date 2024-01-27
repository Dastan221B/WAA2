using Assets.Scripts.MVC.CastleMVC;
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
        private CastleModel _castleModel;

        public HeroMoveToCastleProcess(CastleModel castleModel,GameTurnView gameTurnView,GameModel gameModel , SlotsModel slotsModel)
        {
            _castleModel = castleModel;
            _gameModel = gameModel;
            _slotsModel = slotsModel;
            _turnView = gameTurnView;
        }

        public void HeroMoveToCastle(MessageInput message)
        {
            MoveHeroToCaslteResult moveHeroToCaslteResult = Newtonsoft.Json.JsonConvert.DeserializeObject<MoveHeroToCaslteResult>(message.body);
            if (_gameModel.TryGetHeroModelObject(moveHeroToCaslteResult.heroId, out HeroModelObject heroModelObject))
            {
                if(heroModelObject != null)
                {
                    heroModelObject.gameObject.SetActive(false);
                    _slotsModel.ResetGarissonCreature();
                    if(moveHeroToCaslteResult.heroInCastle != null)
                    {
                        _slotsModel.AddCreaturesToCastleSlot(moveHeroToCaslteResult.heroInCastle.army);
                        if(heroModelObject.HeroObjectFullInfo != null)
                        {
                            heroModelObject.HeroObjectFullInfo.army = moveHeroToCaslteResult.heroInCastle.army;
                        }
                    }
                    _castleModel.SetHeroInCastle(heroModelObject.HeroObjectFullInfo);
                    _castleModel.SetHeroInGarrison(null);
                    heroModelObject.EnterInCastle();
                    _gameModel.ReplaceToLastPlaceInTurn(heroModelObject);
                    _turnView.ResetDisplayHeroes();
                }
            }
        }

    }
}