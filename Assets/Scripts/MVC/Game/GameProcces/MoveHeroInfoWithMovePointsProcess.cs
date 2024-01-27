using Assets.Scripts.GameResources;
using Assets.Scripts.GameResources.MapCreatures;
using Assets.Scripts.MVC.Game.Path;
using Assets.Scripts.MVC.Game.Views;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        private SystemColors _systemColors;

        public MoveHeroInfoWithMovePointsProcess(SystemColors systemColors,GameTurnView gameTurnView,GameModel gameModel, HeroPathMover heroPathMover , PathFinder pathFinder, PathDrawer pathDrawer)
        {
            _systemColors = systemColors;
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

                    if (cell.GameMapObjectType == GameMapObjectType.CASTLE)
                    {
                        if (!(cell.CreatureModelObject is HeroModelObject))
                        {
                            if (cell.Castle != null)
                            {
                                if (cell.Castle.Oridinal != heroModelObject.Ordinal)
                                {
                                    _gameModel.RemoveCasle(cell.Castle);
                                    cell.Castle.SetupColorCube(_systemColors.GetColorByOrdinal(heroModelObject.Ordinal));
                                }
                            }
                        }
                        else if((cell.CreatureModelObject is HeroModelObject))
                        {
                            if(cell.CreatureModelObject != null && cell.CreatureModelObject is HeroModelObject modelObject)
                            {
                                if (cell.Castle != null)
                                {
                                    _gameModel.SetAttakedCastle(cell.Castle);
                                }
                            }
                        }
                    }
                    _gameTurnView.UpdateTurnView();
                }
            }
            else
            {
                _gameModel.StopHeroMove();
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
                _gameTurnView.UpdateTurnView();
                var path = _pathFinder.ConvertPositionToCellPath(movementPath);
                Cell cell = path[path.Count - 1];
                if (cell.GameMapObjectType == GameMapObjectType.CASTLE)
                {
                    if (!(cell.CreatureModelObject is HeroModelObject heroModelObject1))
                    {
                        if (cell.Castle != null)
                        {
                            if (cell.Castle.Oridinal != heroModelObject.Ordinal)
                            {
                                Debug.Log("added castle");
                                _gameModel.AddCasstle(cell.Castle);
                            }
                            else
                            {
                                if(_gameModel.CastlesTurn.Contains(cell.Castle))
                                    _gameModel.RemoveCasle(cell.Castle);
                            }

                            cell.Castle.SetupColorCube(_systemColors.GetColorByOrdinal(heroModelObject.Ordinal));
                        }
                    }
                    else if ((cell.CreatureModelObject is HeroModelObject))
                    {
                        if (cell.CreatureModelObject != null && cell.CreatureModelObject is HeroModelObject modelObject)
                        {
                            if (cell.Castle != null)
                            {
                                _gameModel.SetAttakedCastle(cell.Castle);
                            }
                        }
                    }
                }
                _pathDrawer.DrawPath(path);
                _gameModel.HeroStartMove();
                _heroPathMover.MoveHeroOnPath(heroModelObject, path, beforeMove);

            }
        }
    }
}