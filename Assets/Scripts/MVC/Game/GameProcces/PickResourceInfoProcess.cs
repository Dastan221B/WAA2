using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.Game.GameProcces
{
    public class PickResourceInfoProcess 
    {
        private GameModel _gameModel;
        private GameTimer _timer;

        public PickResourceInfoProcess(GameTimer gameTimer,GameModel gameModel)
        {
            _timer = gameTimer;
            _gameModel = gameModel;
        }


        public void PickResourcesHandler(MessageInput messageInput)
        {
            PickResourceInfo pickResourceInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<PickResourceInfo>(messageInput.body);
            if (_gameModel.TryGetResourceByResourceObjectID(pickResourceInfo.resourceId, out ResourceSturcture resourceSturcture))
            {
                _timer.TimerTime = pickResourceInfo.turnSeconds;
                resourceSturcture.CellPlace.ResetInteractiveMapObject();
                MonoBehaviour.Destroy(resourceSturcture.gameObject);
                Debug.Log("pickResourceInfo.resourceId " + pickResourceInfo.resourceId);
            }
        }
    }
}