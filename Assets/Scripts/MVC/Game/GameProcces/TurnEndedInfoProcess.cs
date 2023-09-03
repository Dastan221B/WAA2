using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.Game.GameProcces
{
    public class TurnEndedInfoProcess : MonoBehaviour
    {
        private TurnTimer _turnTimer;
        private GameModel _gameModel;

        public TurnEndedInfoProcess(GameModel gameModel)
        {
            _gameModel = gameModel;
        }

        public void InitBeforeLoaded(TurnTimer turnTimer)
        {
            _turnTimer = turnTimer;
        }

        public bool TryGetTurnEndTurnResult(MessageInput message, out TurnEndedInfo turnEndInfo)
        {
            turnEndInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<TurnEndedInfo>(message.body);
            if (turnEndInfo.result)
            {
                if(_turnTimer != null)
                {
                    _turnTimer.StopTimer();
                    _turnTimer.StartTimer(turnEndInfo.turnSeconds);
                }
                _gameModel.ExitFromTurn();
                return true;
            }
            return false;
        }
    }
}