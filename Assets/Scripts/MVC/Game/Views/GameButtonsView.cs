using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MVC.Game.Views
{
    public class GameButtonsView : MonoBehaviour
    {
        private Button _surrenderButton;
        private GameModel _gameModel;

        public void Init(GameModel gameModel)
        {
            _gameModel = gameModel;
        }

        public void Init(Button surrenderButton)
        {
            _surrenderButton = surrenderButton;
            _surrenderButton.onClick.AddListener(_gameModel.Surrender);
        }
    }
}