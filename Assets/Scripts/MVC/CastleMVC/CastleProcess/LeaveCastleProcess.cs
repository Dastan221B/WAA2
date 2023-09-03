using Assets.Scripts.MVC.CastleMVC.View;
using Assets.Scripts.MVC.HeroPanel;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.CastleMVC.CastleProcess
{
    public class LeaveCastleProcess
    {
        private GameModel _gameModel;
        private CastleView _castleView;
        private ProgramState _programState;
        private HeroPanelView _heroPanelView;

        public LeaveCastleProcess(HeroPanelView heroPanelView, ProgramState programState, GameModel gameModel, CastleView castleView)
        {
            _heroPanelView = heroPanelView;
            _programState = programState;
            _gameModel = gameModel;
            _castleView = castleView;
        }

        public void LeaveCastle(MessageInput message)
        {
            LeaveCastleResult leaveCastleResult = Newtonsoft.Json.JsonConvert.DeserializeObject<LeaveCastleResult>(message.body);
            if(_gameModel.SelectedHero != null)
                _gameModel.SelectedHero.SetArmySlots(leaveCastleResult.garrisonArmy);
            _castleView.Close();
            _programState.GameStartHandler();
            if (_gameModel.SelectedHero != null)
                _heroPanelView.SetGameBarHero(_gameModel.SelectedHero, _gameModel.SelectedHero.Hero.Icon);
        }
    }
}