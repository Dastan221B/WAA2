using Assets.Scripts.MVC.CastleMVC;
using Assets.Scripts.MVC.CastleMVC.View;
using Assets.Scripts.MVC.Game;
using Assets.Scripts.MVC.Game.Path;
using Assets.Scripts.MVC.Game.Views;
using Assets.Scripts.MVC.Game.Views.UI;
using Assets.Scripts.MVC.Ground;
using Assets.Scripts.MVC.TradeMVC;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private LoadScreen _loadScreen;
    private GameModel _gameModel;
    private ISessionParticipants _sessionParticipants;
    private GameBattleProcessResponseHandler _gameProcessResponseHandler;
    private GameAndBattleCommandsSender _gameCommandsSender;
    private CellPicker _cellPicker;
    private HeroPicker _heroPicker;
    private PathFinder _pathFinder;
    private PathDrawer _pathDrawer;
    private GroundController _groundController;
    private CastleCommandsSender _castleCommandsSender;
    private CastleModel _castleModel;
    private bool _isFirstInital;
    private GameLoaderController _gameLoaderController;
    private ResourcesView _resourcesView;
    public GameModel GameModel => _gameModel;


    public void Init(GameLoaderController gameLoaderController , LoadScreen loadScreen,GameModel gameModel,GameBattleProcessResponseHandler gameProcessResponseHandler , CellPicker cellPicker ,
        HeroPicker heroPicker ,
        PathFinder pathFinder , 
        PathDrawer pathDrawer, 
        ISessionParticipants 
        sessionParticipants, 
        GameAndBattleCommandsSender gameCommandsSender,
        GroundController groundController)
    {
        _loadScreen = loadScreen;
        _gameModel = gameModel;
        _gameProcessResponseHandler = gameProcessResponseHandler;
        _cellPicker = cellPicker;
        _heroPicker = heroPicker;
        _pathFinder = pathFinder;
        _pathDrawer = pathDrawer;
        _sessionParticipants = sessionParticipants;
        _gameCommandsSender = gameCommandsSender;
        _groundController = groundController;
        _gameLoaderController = gameLoaderController;
    }

    public void Init(CastleCommandsSender castleCommandsSender,CastleModel castleModel)
    {
        _castleCommandsSender = castleCommandsSender;
        _castleModel = castleModel;

    }

    public void LoadGameObjectsInitalOnce()
    {
        if (_isFirstInital)
        {
            _gameLoaderController.LoadGame();
        }
    }

    public void SetSceneObjectsParent(Transform sceneObjectsParent, Transform terrainParent , ResourcesView resourcesView)
    {
        _gameModel.SetSceneObjectsParent(sceneObjectsParent, terrainParent);
        _resourcesView = resourcesView;
    }

    public void UpdateResources()
    {
        if (_resourcesView != null)
            _resourcesView.UpdateResources();
        else
            StartCoroutine(UpdateResourcesCouroutine());
    }

    private IEnumerator UpdateResourcesCouroutine()
    {
        while(_resourcesView == null)
        {
            yield return null;
        }
        _resourcesView.UpdateResources();
    }

    public void LoadedObjectsData()
    {
        _groundController.GenerateTrain();
    }



    public void LoadGameScene()
    {
        if (_gameProcessResponseHandler.IsStartedGameFlag)
        {
            _gameModel.InitGameSession(_sessionParticipants.SessionParticipants);
            _loadScreen.OpenLoadBar(StatesOfProgram.Game);
            SceneManager.LoadScene("Game");
        }
    }

    public void Update()
    {
        if (!_gameModel.IsCurrentTurn || _loadScreen.IsLoaded) 
            return;
        UIEvents();
        if (!_gameModel.IsHeroMove && Input.GetMouseButtonDown(0) && _cellPicker.TryPickCell(out Cell cell) && !CheckToUI() && !FindAnyObjectByType<GameSceneLoaderTrigger>().TradePanel.activeSelf)
        {
            if (cell.IsTargetPoint)
            {
                if (cell.InteractiveMapObjectId == "" && cell.ParentObjectId == "")
                    _gameCommandsSender.SendMoveHeroRequest(new Vector2Int((int)cell.transform.position.x, (int)cell.transform.position.z));
                else if (cell.InteractiveMapObjectId != "")
                {
                    if (cell.GameMapObjectType == GameMapObjectType.CASTLE)
                    {
                        if (_gameModel.SelectedHero.InCastle)
                            return;
                        if (cell.X == _gameModel.SelectedHero.LastCellStayed.X && cell.Y == _gameModel.SelectedHero.LastCellStayed.Y)
                            return;
                        if (cell.X == _gameModel.PreviousHero.LastCellStayed.X && cell.Y == _gameModel.PreviousHero.LastCellStayed.Y)
                            return;
                    }

                    Debug.Log("Working is here");
                    _gameCommandsSender.SendMoveHeroRequestWithInteractable
                        (new Vector2Int((int)cell.transform.position.x,
                        (int)cell.transform.position.z),
                        cell.InteractiveMapObjectId,
                        cell.GameMapObjectType);
                    if(cell.GameMapObjectType == GameMapObjectType.CREATURE || cell.GameMapObjectType == GameMapObjectType.HERO)
                    {
                        _gameModel.StartAttack();
                        _gameModel.SetFightedCreatureSettings(cell.CreatureModelObject, cell);
                    }
                    if (cell.GameMapObjectType == GameMapObjectType.HERO)
                    {
                        _gameModel.StartAttackOnHero();
                    }
                    if (cell.CheckHero())
                    {
                        _gameModel.StartAttack();
                    }
                    if(cell.GameMapObjectType == GameMapObjectType.CASTLE)
                    {
                        if(cell.Castle != null)
                        {
                            _gameModel.SetAttakedCastle(cell.Castle);
                        }
                    }
                }else if(cell.ParentObjectId != "")
                {
                    if (cell.GameMapObjectType == GameMapObjectType.CASTLE && cell.Castle != null)
                    {
                        Debug.Log("Working is here");
                        Vector2Int cellVector = new Vector2Int((int)cell.X, (int)cell.Y);
                        Vector2Int heroVector = new Vector2Int((int)_gameModel.SelectedHero.CellPlace.X, (int)_gameModel.SelectedHero.CellPlace.Y);
                        Debug.Log(cellVector.ToString() + " " + heroVector.ToString());
                        if (Vector2Int.Distance(heroVector, cellVector) <= 1)
                        {
                            Debug.Log("Working is here");
                            return;
                        }
                        if (_gameModel.SelectedHero.LastCellStayed.Castle != null)
                            return;
                        _gameCommandsSender.SendMoveHeroRequestWithInteractable
                             (new Vector2Int((int)cell.Castle.GateCell.transform.position.x,
                             (int)cell.Castle.GateCell.transform.position.z),
                             cell.Castle.GateCell.InteractiveMapObjectId,
                             cell.Castle.GateCell.GameMapObjectType);
                        if (cell.Castle != null)
                        {
                            _gameModel.SetAttakedCastle(cell.Castle);
                        }
                    }
                    else if (cell.GameMapObjectType == GameMapObjectType.MINE && cell.MineStructure != null)
                    {
                        _gameCommandsSender.SendMoveHeroRequestWithInteractable
                             (new Vector2Int((int)cell.MineStructure.GateCell.transform.position.x,
                             (int)cell.MineStructure.GateCell.transform.position.z),
                             cell.MineStructure.GateCell.InteractiveMapObjectId,
                             cell.MineStructure.GateCell.GameMapObjectType);
                    }
                }
            }
            else
            {
                var path = _pathFinder.FindPath(_gameModel.PositionSelectedHeroOnGrid, cell.transform.position.ToVector2IntInHeroPosition(), out List<float> path_lengthes);
                HeroModelObject hero = _gameModel.SelectedHero;
                _pathDrawer.DrawPath(path, path_lengthes, hero.MovePointsLeft);   
            }
        }
    }

    private bool CheckToUI()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        if(results.Count > 0)
            return true;
        return false;
    }

    public void SelectHero(HeroModelObject heroModelObject)
    {
        _pathDrawer.ResetDrawedCells();
        _gameModel.SetSelectedHero(heroModelObject);
    }

    private void UIEvents()
    {
        if(Input.GetMouseButtonDown(0) && _heroPicker.TryPickHero(out HeroModelObject heroModelObject))
        {
            SelectHero(heroModelObject);
        }

    }
}