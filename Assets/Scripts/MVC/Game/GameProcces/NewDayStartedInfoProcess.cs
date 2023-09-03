using Assets.Scripts.Client.GameClient.RquestsAndDTO;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.Game.GameProcces
{
    public class NewDayStartedInfoProcess
    {
        private GameModel _gameModel;

        public NewDayStartedInfoProcess(GameModel gameModel)
        {
            _gameModel = gameModel;
        }

        public void InitProcess(MessageInput messageInput)
        {
            NewDayStartedInfo newDayStartedInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<NewDayStartedInfo>(messageInput.body);

            _gameModel.SetDate(newDayStartedInfo.daysCounter, newDayStartedInfo.weeksCounter, newDayStartedInfo.monthsCounter);

        }

    }
}