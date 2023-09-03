using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.Game.GameProcces
{
    public class TurnStartedResultProcess 
    {
        private TurnTimer _turnTimer;

        public void SetTimer(TurnTimer turnTimer)
        {
            _turnTimer = turnTimer;
        }

        public bool TryGetTurnStartTurnResult(MessageInput message, out TurnStartedResult turnStartedResult)
        {
            turnStartedResult = Newtonsoft.Json.JsonConvert.DeserializeObject<TurnStartedResult>(message.body);
            if (turnStartedResult.result)
            {
                _turnTimer.StopTimer();
                _turnTimer.StartTimer(turnStartedResult.turnSeconds);

                return true;
            }
            return false;
        }
    }
}