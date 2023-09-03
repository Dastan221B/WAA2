using Assets.Scripts.MVC.Game.Views.UI;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.Game.Views
{
    public class GameDateView : MonoBehaviour
    {
        private GameDateUI _gameDateUI;
        private GameModel _gameModel;

        public void Init(GameModel gameModel)
        {
            _gameModel = gameModel;
            _gameModel.OnUpdateGameDate += UpdateDateUI;
        }

        public void SetGameDataUI(GameDateUI gameDateUI)
        {
            _gameDateUI = gameDateUI;
        }

        public void UpdateDateUI()
        {
            _gameDateUI.SetDate(_gameModel.DaysCounter, _gameModel.WeeksCounter, _gameModel.MonthsCounter);
        }

    }
}