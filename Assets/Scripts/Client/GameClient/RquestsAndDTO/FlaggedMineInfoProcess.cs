using Assets.Scripts.MVC.Game;
using Assets.Scripts.MVC.Game.Path;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaggedMineInfoProcess : MonoBehaviour
{
    private FlaggedMineInfo _currentFlaggedMineInfo;
    private GameAndBattleCommandsSender _gameAndBattleCommandsSender;
    private GameModel _gameModel;
    private HeroPathMover _heroPathMover;
    private PathFinder _pathFinder;
    public FlaggedMineInfoProcess(GameAndBattleCommandsSender gameAndBattleCommandsSender, GameModel gameModel, HeroPathMover heroPathMover, PathFinder pathFinder)
    {
        _gameAndBattleCommandsSender = gameAndBattleCommandsSender;
        _gameModel = gameModel;
        _heroPathMover = heroPathMover;
        _pathFinder = pathFinder;
    }
    public void FlaggedMineInfoHandler(MessageInput messageInput)
    {
        FlaggedMineInfo flaggedMineInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<FlaggedMineInfo>(messageInput.body);
        _currentFlaggedMineInfo = flaggedMineInfo;
        if (_gameModel.TryGetHeroModelObject(flaggedMineInfo.heroId, out HeroModelObject heroModelObject))
        {

            var path = _pathFinder.ConvertPositionToCellPath(flaggedMineInfo.movementPath);
            _gameModel.HeroStartMove();
            _heroPathMover.MoveHeroOnPath(heroModelObject, path);
            if (_gameModel.TryGetMine(flaggedMineInfo.mineObjectId, out MineStructure mineStructure))
            {
                if (_gameModel.TryGetPlayerByHeroID(heroModelObject.MapObjectID, out Player minePlayer))
                {
                    Debug.Log("MinePlayer" + minePlayer.Ordinal);
                    mineStructure.SetCubeColor(minePlayer.Ordinal);
                }
            }
        }
    }
}
