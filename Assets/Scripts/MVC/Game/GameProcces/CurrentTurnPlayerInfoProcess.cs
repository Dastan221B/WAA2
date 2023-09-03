using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.Game.GameProcces
{
    public class CurrentTurnPlayerInfoProcess : MonoBehaviour
    {
        private GameModel _gameModel;

        public CurrentTurnPlayerInfoProcess(GameModel gameModel)
        {
            _gameModel = gameModel;
        }

        public void EnterPlayerInTurn(MessageInput message)
        {
            CurrentTurnPlayerInfo currentTurnPlayerInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<CurrentTurnPlayerInfo>(message.body);
            _gameModel.PlayerEnterInTurn(currentTurnPlayerInfo.ordinal);
        }

    }
}