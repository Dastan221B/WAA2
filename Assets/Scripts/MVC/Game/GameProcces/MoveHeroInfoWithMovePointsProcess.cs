using Assets.Scripts.MVC.Game.Path;
using Assets.Scripts.MVC.Game.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MVC.Game.GameProcces
{
    public class MoveHeroInfoWithMovePointsProcess
    {
        private GameModel _gameModel;
        private HeroPathMover _heroPathMover;
        private PathFinder _pathFinder;
        private PathDrawer _pathDrawer;
        private GameTurnView _gameTurnView;

        public MoveHeroInfoWithMovePointsProcess(GameTurnView gameTurnView,GameModel gameModel, HeroPathMover heroPathMover , PathFinder pathFinder, PathDrawer pathDrawer)
        {
            _gameTurnView = gameTurnView;
            _gameModel = gameModel;
            _heroPathMover = heroPathMover;
            _pathFinder = pathFinder;
            _pathDrawer = pathDrawer;
        }

        public void RecieveMoveHeroInfoWithMovePoints(MessageInput message)
        {
            MoveHeroInfoWithMovePoints moveHeroInfoWithMovePoints = Newtonsoft.Json.JsonConvert.DeserializeObject<MoveHeroInfoWithMovePoints>(message.body);
            if (moveHeroInfoWithMovePoints.result)
            {
                if(_gameModel.TryGetHeroModelObject(moveHeroInfoWithMovePoints.heroId, out HeroModelObject heroModelObject))
                {
                    heroModelObject.SetMovePointsLeft(moveHeroInfoWithMovePoints.movePointsLeft);

                    var path = _pathFinder.ConvertPositionToCellPath(moveHeroInfoWithMovePoints.movementPath);
                    _pathDrawer.DrawPath(path);
                    _gameModel.HeroStartMove();
                    _heroPathMover.MoveHeroOnPath(heroModelObject, path);
                    Cell cell = path[path.Count - 1];
                    Debug.Log(1);
                    if(cell.GameMapObjectType == GameMapObjectType.CASTLE)
                    {
                        Debug.Log(2);
                        if (!cell.CheckHero())
                        {
                            Debug.Log(3);
                            if (cell.Castle != null)
                            {
                                Debug.Log(4);
                                _gameModel.RemoveCasle(cell.Castle);
                            }
                        }
                    }
                    _gameTurnView.UpdateTurnView();
                }
            }
            else
            {
                _gameModel.StopHeroMove();
                Debug.Log(moveHeroInfoWithMovePoints.reason);
            }
        }

        public void MoveHeroAndAndActionBeforeEndingMove(List<Coordinates> movementPath, string heroId, int movePointsLeft)
        {
            if (_gameModel.TryGetHeroModelObject(heroId, out HeroModelObject heroModelObject))
            {
                heroModelObject.SetMovePointsLeft(movePointsLeft);

                var path = _pathFinder.ConvertPositionToCellPath(movementPath);
                _pathDrawer.DrawPath(path);
                _gameModel.HeroStartMove();
                _heroPathMover.MoveHeroOnPath(heroModelObject, path);
            }
        }

        public void MoveHeroAndAndActionBeforeEndingMove(List<Coordinates> movementPath, string heroId , int movePointsLeft, System.Action beforeMove)
        {
            if (_gameModel.TryGetHeroModelObject(heroId, out HeroModelObject heroModelObject))
            {
                heroModelObject.SetMovePointsLeft(movePointsLeft);

                var path = _pathFinder.ConvertPositionToCellPath(movementPath);
                _pathDrawer.DrawPath(path);
                _gameModel.HeroStartMove();
                _heroPathMover.MoveHeroOnPath(heroModelObject, path, beforeMove);

            }
        }
    }
}