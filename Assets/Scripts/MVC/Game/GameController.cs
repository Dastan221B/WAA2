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
        if (!_gameModel.IsCurrentTurn || _loadScreen.IsLoaded) return;
        UIEvents();
        if (!_gameModel.IsHeroMove && Input.GetMouseButtonDown(0) && _cellPicker.TryPickCell(out Cell cell) && !CheckToUI() && !FindAnyObjectByType<GameSceneLoaderTrigger>().TradePanel.activeSelf)
        {
            if (cell.IsTargetPoint)
            {
                if (cell.InteractiveMapObjectId == "" && cell.ParentObjectId == "")
                    _gameCommandsSender.SendMoveHeroRequest(new Vector2Int((int)cell.transform.position.x, (int)cell.transform.position.z));
                else if (cell.InteractiveMapObjectId != "")
                {
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
                        _gameModel.StartAttack();
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
                    Debug.Log("cell.Castle " + cell.Castle);
                    if (cell.GameMapObjectType == GameMapObjectType.CASTLE && cell.Castle != null)
                    {
                        Debug.Log("Move 1");
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
                        Debug.Log("Move 2");
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