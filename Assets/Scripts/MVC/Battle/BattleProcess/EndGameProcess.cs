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

        public EndGameProcess(BattleModel battleModel, LoadScreen loadScreen, GameModel gameModel)
        {
            _gameModel = gameModel;
            _loadScreen = loadScreen;
            _battleModel = battleModel;
        }

        public void EndGame()
        {
            OnGameStarted?.Invoke();
            _battleModel.ClearBattleScene();
            _loadScreen.OpenLoadBar(StatesOfProgram.Game);
            SceneManager.UnloadSceneAsync("Battle");
            _gameModel.EnterInGameFromBattleScene();
        }
    }
}