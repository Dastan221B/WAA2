using Assets.Scripts.GameResources;
using Assets.Scripts.MVC.Game.Path;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.Game.GameProcces
{
    public class PickResourceProcees 
    {
        private MoveHeroInfoWithMovePointsProcess _moveHeroInfoWithMovePointsProcess;
        private GameModel _gameModel;
        private ResourcesDataService _resourcesDataService;
        private PickResourceResult _currentPickResourceResult;
        private GameAndBattleCommandsSender _gameAndBattleCommandsSender;
        private HeroPathMover _heroPathMover;

        public PickResourceProcees(HeroPathMover heroPathMover,GameModel gameModel , MoveHeroInfoWithMovePointsProcess moveHeroInfoWithMovePointsProcess, ResourcesDataService resourcesDataService , GameAndBattleCommandsSender gameAndBattleCommandsSender)
        {
            _heroPathMover = heroPathMover;
            _resourcesDataService = resourcesDataService;
            _gameModel = gameModel;
            _moveHeroInfoWithMovePointsProcess = moveHeroInfoWithMovePointsProcess;
            _gameAndBattleCommandsSender = gameAndBattleCommandsSender;
        }

        public void PickResourcesHandler(MessageInput messageInput)
        {
            PickResourceResult pickResourceResult = Newtonsoft.Json.JsonConvert.DeserializeObject<PickResourceResult>(messageInput.body);
            _currentPickResourceResult = pickResourceResult;

            if (_gameModel.TryGetResourceByResourceObjectID(pickResourceResult.resourceid, out ResourceSturcture resourceSturcture))
            {
                _resourcesDataService.ChangeResourceCountByID(resourceSturcture.DicResourceId - 1, pickResourceResult.amount);
            }
            _gameAndBattleCommandsSender.SendMoveHeroRequest(new Vector2Int(pickResourceResult.movementPath[pickResourceResult.movementPath.Count - 1].x,
                pickResourceResult.movementPath[pickResourceResult.movementPath.Count - 1].y));

            _heroPathMover.AddEventBeforeMove(() => PickResources());

            //_moveHeroInfoWithMovePointsProcess.MoveHeroAndAndActionBeforeEndingMove
            //(pickResourceResult.movementPath, pickResourceResult.heroId,pickResourceResult.movePointsLeft, () => PickResources());
        }

        private void PickResources()
        {
            if (_gameModel.TryGetResourceByResourceObjectID(_currentPickResourceResult.resourceid, out ResourceSturcture resourceSturcture))
            {
                resourceSturcture.CellPlace.ResetInteractiveMapObject();
                MonoBehaviour.Destroy(resourceSturcture.gameObject);
            }
        }
    }
}