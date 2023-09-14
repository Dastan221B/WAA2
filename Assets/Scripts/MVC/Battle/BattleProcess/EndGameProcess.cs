using Assets.Scripts.MVC.Game;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.MVC.Battle.BattleProcess
{
    public class EndGameProcess
    {
        public event System.Action OnGameStarted;
        private BattleModel _battleModel;
        private LoadScreen _loadScreen;
        private GameModel _gameModel;
        private GameTimer _gameTimer;

        public EndGameProcess(GameTimer gameTimer,BattleModel battleModel, LoadScreen loadScreen, GameModel gameModel)
        {
            _gameTimer = gameTimer;
            _gameModel = gameModel;
            _loadScreen = loadScreen;
            _battleModel = battleModel;
        }

        public void EndGame()
        {
            OnGameStarted?.Invoke();
            _battleModel.ClearBattleScene();
            _loadScreen.OpenLoadBar(StatesOfProgram.Game, () => {
                if (_gameModel.IsCurrentTurn)
                    _gameTimer.ContinueTimer();
                else
                    _gameTimer.StopTimer();
            });
            SceneManager.UnloadSceneAsync("Battle");
            _gameModel.EnterInGameFromBattleScene();
        }
    }
}