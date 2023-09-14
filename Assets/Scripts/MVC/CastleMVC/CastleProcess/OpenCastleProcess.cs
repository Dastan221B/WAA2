using Assets.Scripts.MVC.CastleMVC.View;
using Assets.Scripts.MVC.CastleSlots;
using Assets.Scripts.MVC.Game;
using Assets.Scripts.MVC.Game.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MVC.CastleMVC.CastleProcess
{
    public class OpenCastleProcess
    {
        private GameModel _gameModel;
        private CastleModel _castleModel;
        private CastleBuildingsView _castleBuildingsView;
        private SlotsModel _slotsModel;
        private CastleView _castleView;
        private ProgramState _programState;
        private CommonData _commonData;
        private GameTimer _timer;
        private LeaveCastleProcess _leaveCastleProcess;
        private GameTurnView _turnView;

        public OpenCastleProcess(GameTurnView gameTurnView,LeaveCastleProcess leaveCastleProcess,GameTimer gameTimer,CommonData commonData, ProgramState programState, SlotsModel slotsModel, CastleView castleView, GameModel gameModel, CastleModel castleModel, CastleBuildingsView castleBuildingsView)
        {
            _turnView = gameTurnView;
            _leaveCastleProcess = leaveCastleProcess;
            _timer = gameTimer;
            _commonData = commonData;
            _programState = programState;
            _slotsModel = slotsModel;
            _castleView = castleView;
            _gameModel = gameModel;
            _castleModel = castleModel;
            _castleBuildingsView = castleBuildingsView;
        }

        public void TryOpenCastle(MessageInput message)
        {
            _leaveCastleProcess.ExitCasle();
            GetCastleResult castleInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCastleResult>(message.body);
            CastleObjectFullInfo castleFullInfo = castleInfo.castleDTO;
            if (_gameModel.TryGetCastle(castleFullInfo.mapObjectId, out Castle castle))
            {
                if (_commonData.TryGetDicCastleDTOByID(castleInfo.castleDTO.dicCastleId, out DicCastleDTO castleDTO))
                {
                    _timer.TimerTime = castleInfo.turnSeconds;
                    DicCastleDTO cloneCastleDTO = castleDTO.Clone();
                    castle.SetCastleObjectFullInfo(castleFullInfo);
                    _programState.CastleStartHandler();
                    _castleModel.SetCasttleSettings(cloneCastleDTO, castleFullInfo.heroInGarrison, castleFullInfo.heroInCastle, castleFullInfo.heroInGarrison != null ? castleFullInfo.heroInGarrison.mapObjectId: "", castleFullInfo.mapObjectId, castleFullInfo);
                    //_castleBuildingsView.DisplayBuildings(castleFullInfo);
                    _slotsModel.AddCreaturesToCastleSlot(castleFullInfo.CreaturesInCastle);
                    if (castleFullInfo.heroInGarrison != null && castleFullInfo.heroInGarrison != null)
                        _slotsModel.AddCreaturesToGarrisonSlot(new List<ArmySlotInfo>(castleFullInfo.heroInGarrison.army));
                    else
                        _slotsModel.AddCreaturesToGarrisonSlot(null);
                    
                    _castleModel.DisplayBuildgins(castleFullInfo, cloneCastleDTO);
                    _castleView.HandleHeroInGarrison(castleFullInfo);
                    _castleView.Open();
                    _gameModel.TryCaptureCastle();
                    _turnView.UpdateTurnView();
                }
            }
        }

    }
}