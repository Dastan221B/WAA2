using Assets.Scripts.GameResources;
using Assets.Scripts.MVC.Battle;
using Assets.Scripts.MVC.Battle.BattleProcess;
using Assets.Scripts.MVC.Battle.Views;
using Assets.Scripts.MVC.CastleMVC.CastleProcess;
using Assets.Scripts.MVC.CastleSlots;
using Assets.Scripts.MVC.Game;
using Assets.Scripts.MVC.Game.GameProcces;
using Assets.Scripts.MVC.Game.Path;
using Assets.Scripts.MVC.Game.Views;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBattleProcessResponseHandler : MonoBehaviour, IGameProcessHandler
{
    public event System.Action OnBattleStarted;
    public event System.Action OnGameStarted;

    private LoadScreen _loadScreen;
    private GameModel _gameModel;
    private TerrainMapProcess _terrainMapProcess;
    private GameLoadedData _gameLoadedData;
    private MapObjectListResponseProcess _mapObjectListResponseProcess;
    private ResourcesMapResponseProcess _resourcesMapResponseProcess;
    private StartGameInfo _currentStartGameInfo;
    private TurnNotificationInfoProcess _turnNotificationInfoProcess;
    private TurnStartedResultProcess _turnStartedResultProcess;
    private TurnEndedInfoProcess _turnEndedInfoProcess;
    private GameTimer _gameTimer;
    private BattleTimer _battleTurnTimer;
    private CurrentTurnPlayerInfoProcess _currentTurnPlayerInfoProcess;
    private MoveHeroInfoWithMovePointsProcess _moveHeroInfoWithMovePointsProcess;
    private FlaggedMineResultProcess _flaggedMineResultProcess;
    private FlaggedMineInfoProcess _flaggedMineInfoProcess;
    private BattleInitalProcess _battleInitalProcess;
    private MessageInput _turnNotificationMessageInput;
    private MessageInput _currentTurnPlayerInfoProcessMessageInput;
    private BattleModel _battleModel;
    private BattleTurnNotificationProcess _battleTurnNotificationProcess;
    private BattleMoveProcess _battleMoveProcess;
    private BattleHitAndMoveProcess _battleHitAndMoveProcess;
    private BattleTurnOrderProcess _battleTurnOrderProcess;
    private BattleBlockActivetedProcess _battleBlockActivetedProcess;
    private BattleWaitActivatedProcess _battleWaitActivatedProcess;
    private ResultPanel _resultPanel;
    private OpenCastleProcess _openCastleProcess;
    private AddBuildingToCastleProcess _addBuildingToCastleProcess;
    private HireCreatureProcess _hireCreatureProcess;
    private HeroEnterInCastleResultProcess _heroEnterInCastleResultProcess;
    private GameCastleObjectsChangeService _gameCastleObjectsChangeService;
    private LeaveCastleProcess _leaveCastleProcess;
    private PickResourceProcees _pickResourceProcees;
    private PickResourceInfoProcess _pickResourceInfoProcess;
    private TradeStartedResultProcess _tradeStartedResultProcess;
    private SubmitTradeResultProcess _submitTradeResultProcess;
    private BattleHitResultInfoProcess _battleHitResultInfoProcess;
    private GameController _gameController;
    private HeroMoveToCastleProcess _heroMoveToCastleProcess;
    private HeroMoveToGarissonProcess _heroMoveToGarissonProcess;
    private NewDayStartedInfoProcess _newDayStartedInfoProcess;
    private GameTurnView _gameTurnView;

    public bool IsStartedGameFlag
    {
        get
        {
            if (_currentStartGameInfo != null)
                return _currentStartGameInfo.result;
            return false;
        }
    }
    private bool _isLoadedGame;

    public void Init(GameTurnView gameTurnView,NewDayStartedInfoProcess newDayStartedInfoProcess,
        HeroPathMover heroPathMover , GameAndBattleCommandsSender gameAndBattleCommandsSender,
        GameController gameController,SubmitTradeResultProcess submitTradeResultProcess,
        TradeStartedResultProcess tradeStartedResultProcess,ResourcesDataService resourcesDataService, 
        GameLoadedData gameLoadedData , GameModel gameModel,
        MoveHeroInfoWithMovePointsProcess moveHeroInfoWithMovePointsProcess, FlaggedMineInfoProcess flaggedMineInfoProcess, FlaggedMineResultProcess flaggedMineResultProcess, LoadScreen loadScreen, 
        GameTimer gameTimer, BattleTimer battleTimer)
    {
        _gameTurnView = gameTurnView;
        _newDayStartedInfoProcess = newDayStartedInfoProcess;
        _gameController = gameController;
        _tradeStartedResultProcess = tradeStartedResultProcess;
        _battleTurnTimer = battleTimer;
        _gameTimer = gameTimer;
        _loadScreen = loadScreen;
        _gameLoadedData = gameLoadedData;
        _gameModel = gameModel;
        _terrainMapProcess = new TerrainMapProcess();
        _mapObjectListResponseProcess = new MapObjectListResponseProcess(_gameLoadedData);
        _resourcesMapResponseProcess = new ResourcesMapResponseProcess(resourcesDataService,_gameLoadedData);
        _turnNotificationInfoProcess = new TurnNotificationInfoProcess();
        _turnStartedResultProcess = new TurnStartedResultProcess();
        _turnEndedInfoProcess = new TurnEndedInfoProcess(_gameModel);
        _currentTurnPlayerInfoProcess = new CurrentTurnPlayerInfoProcess(gameModel);
        _moveHeroInfoWithMovePointsProcess = moveHeroInfoWithMovePointsProcess;
        _flaggedMineInfoProcess = flaggedMineInfoProcess;
        _flaggedMineResultProcess = flaggedMineResultProcess;
        _pickResourceProcees = new PickResourceProcees(heroPathMover,gameModel, moveHeroInfoWithMovePointsProcess, resourcesDataService, gameAndBattleCommandsSender);
        _pickResourceInfoProcess = new PickResourceInfoProcess(_gameTimer,gameModel);
        _submitTradeResultProcess = submitTradeResultProcess;
    }

    public void Init(BattleTurnOrderProcess battleTurnOrderProcess,BattleModel battleModel, BattleInitalProcess battleInitalProcess, 
        BattleMoveProcess battleMoveProcess, BattleHitAndMoveProcess battleHitAndMoveProcess
        , BattleBlockActivetedProcess battleBlockActivetedProcess, BattleWaitActivatedProcess battleWaitActivatedProcess, ResultPanel resultPanel,
        BattleHitResultInfoProcess battleHitResultInfoProcess)
    {
        _battleTurnOrderProcess = battleTurnOrderProcess;
        _battleHitResultInfoProcess = battleHitResultInfoProcess;
        _resultPanel = resultPanel;
        _battleModel = battleModel;
        _battleInitalProcess = battleInitalProcess;
        _battleTurnNotificationProcess = new BattleTurnNotificationProcess(_battleModel);
        _battleMoveProcess = battleMoveProcess;
        _battleHitAndMoveProcess = battleHitAndMoveProcess;
        _battleBlockActivetedProcess = battleBlockActivetedProcess;
        _battleWaitActivatedProcess = battleWaitActivatedProcess;
    }

    public void Init(OpenCastleProcess openCastleProcess, AddBuildingToCastleProcess addBuildingToCastleProcess,
        HireCreatureProcess hireCreatureProcess, HeroEnterInCastleResultProcess heroEnterInCastleResultProcess,
        LeaveCastleProcess leaveCastleProcess , SlotsModel slotsModel)
    {
        _leaveCastleProcess = leaveCastleProcess;
        _hireCreatureProcess = hireCreatureProcess;
        _addBuildingToCastleProcess = addBuildingToCastleProcess;
        _openCastleProcess = openCastleProcess;
        _heroEnterInCastleResultProcess = heroEnterInCastleResultProcess;
        _heroMoveToCastleProcess = new HeroMoveToCastleProcess(_gameTurnView,_gameModel, slotsModel);
        _heroMoveToGarissonProcess = new HeroMoveToGarissonProcess(_gameTurnView,_gameModel, slotsModel);
    }

    public void InitBeforeLoaded(GameCastleObjectsChangeService gameCastleObjectsChangeService)
    {
        _gameCastleObjectsChangeService = gameCastleObjectsChangeService;
        StartCoroutine(WaitBeforeAllDataLoaded());
    }

    private IEnumerator WaitBeforeAllDataLoaded()
    {
        while (_turnNotificationMessageInput == null)
        {
            yield return null;
        }
        _turnStartedResultProcess.SetTimer(_gameTimer);
        _turnEndedInfoProcess.InitBeforeLoaded(_gameTimer);
        if (_turnNotificationMessageInput != null && _turnNotificationInfoProcess.TryGetTurnInfo(_turnNotificationMessageInput, out TurnNotificationInfo turnNotificationInfo))
            _gameModel.EnterInTurn(turnNotificationInfo);
        _currentTurnPlayerInfoProcess.EnterPlayerInTurn(_currentTurnPlayerInfoProcessMessageInput);
        _isLoadedGame = true;
    }

    public void ProccessResponseFromServer(MessageInput messageInput)
    {
        Debug.Log((InputGameHeaders)messageInput.header + " Response from server");
        switch ((InputGameHeaders)messageInput.header)
        {
            case InputGameHeaders.START_GAME_INFO:
                _currentStartGameInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<StartGameInfo>(messageInput.body);
                _gameModel.SetGameSessionID(_currentStartGameInfo.sessionId);
                break;
            case InputGameHeaders.TERRAIN_MAP_INFO:
                TerrainMapInfo terrainMapInfo = _terrainMapProcess.ResolveTerrainMap(messageInput);
                _gameLoadedData.AddToData(terrainMapInfo.terrainMap, typeof(TerrainCellsData));
                break;
            case InputGameHeaders.GAME_OBJECT_INFO:
                _mapObjectListResponseProcess.InitData(messageInput);
                break;
            case InputGameHeaders.RESOURCES_INFO:
                _resourcesMapResponseProcess.InitData(messageInput, _isLoadedGame);
                _gameController.UpdateResources();
                break;
            case InputGameHeaders.GAME_RESULT_INFO:
                break;
            case InputGameHeaders.GAME_PARTICIPANT_LOST_INFO:
                break;
            case InputGameHeaders.LEAVE_GAME_RESULT:
                break;
            case InputGameHeaders.GAME_INITIAL_INFO:
                _gameController.LoadGameScene();
                break;
            case InputGameHeaders.GAME_CLOSED_INFO:
                break;
            case InputGameHeaders.TURN_NOTIFICATION_INFO:
                if (!_isLoadedGame)
                {
                    _turnNotificationMessageInput = messageInput;
                }
                else if (_turnNotificationInfoProcess.TryGetTurnInfo(messageInput, out TurnNotificationInfo turnNotificationInfo))
                {
                    _gameModel.EnterInTurn(turnNotificationInfo);
                }
                break;
            case InputGameHeaders.TURN_STARTED_RESULT:
                if(_turnStartedResultProcess.TryGetTurnStartTurnResult(messageInput, out TurnStartedResult turnStartedResult))
                    _gameModel.PlayerStartTurn();
                break;
            case InputGameHeaders.TURN_ENDED_INFO:
                if(_turnEndedInfoProcess.TryGetTurnEndTurnResult(messageInput, out TurnEndedInfo turnEndedInfo))
                    _gameModel.ExitFromTurn();
                break;
            case InputGameHeaders.MOVE_HERO_INFO:
                _moveHeroInfoWithMovePointsProcess.RecieveMoveHeroInfoWithMovePoints(messageInput);
                break;
            case InputGameHeaders.MOVE_HERO_FULL_INFO:
                _moveHeroInfoWithMovePointsProcess.RecieveMoveHeroInfoWithMovePoints(messageInput);
                break;
            case InputGameHeaders.CURRENT_TURN_PLAYER_INFO:
                if (!_isLoadedGame)
                {
                    _currentTurnPlayerInfoProcessMessageInput = messageInput;
                }
                //else
                //    _currentTurnPlayerInfoProcess.EnterPlayerInTurn(messageInput);
                break;
            case InputGameHeaders.HERO_ENTER_CASTLE_RESULT:
                _heroEnterInCastleResultProcess.EnterInCastle(messageInput);
                break;
            case InputGameHeaders.GET_CASTLE_RESULT:
                _openCastleProcess.TryOpenCastle(messageInput);
                _gameCastleObjectsChangeService.EnterCastle();
                break;
            case InputGameHeaders.ADD_CASTLE_BUILDING_RESULT:
                _addBuildingToCastleProcess.AddBuilding(messageInput);
                break;
            case InputGameHeaders.HIRE_CASTLE_CREATURE_RESULT:
                _hireCreatureProcess.HireCreature(messageInput);
                break;
            case InputGameHeaders.TRADE_STARTED_RESULT:
                _tradeStartedResultProcess.TradeStartedResultHandler(messageInput);
                break;
            case InputGameHeaders.SUBMIT_TRADE_RESULT:
                _submitTradeResultProcess.SubmitTradeResultHandler(messageInput);
                break;
            case InputGameHeaders.UPGRADE_CREATURE_STACK_RESULT:
                break;
            case InputGameHeaders.PICK_RESOURCE_INFO:
                _pickResourceInfoProcess.PickResourcesHandler(messageInput);
                break;
            case InputGameHeaders.PICK_RESOURCE_RESULT:
                _pickResourceProcees.PickResourcesHandler(messageInput);
                break;
            case InputGameHeaders.RELEASE_CREATURE_STACK_RESULT:
                break;
            case InputGameHeaders.LEAVE_CASTLE_RESULT:
                _gameCastleObjectsChangeService.EnterGame();
                _leaveCastleProcess.LeaveCastle(messageInput);
                break;
            case InputGameHeaders.MOVE_HERO_TO_CASTLE_RESULT:
                _heroMoveToCastleProcess.HeroMoveToCastle(messageInput);
                break;
            case InputGameHeaders.MOVE_HERO_TO_GARRISON_RESULT:
                _heroMoveToGarissonProcess.HeroMoveToGarisson(messageInput);
                break;
            case InputGameHeaders.BATTLE_INITIAL_INFO:
                _battleInitalProcess.SetBattleInfo(messageInput, out BattleInitialInfo battleInitialInfo);
                _moveHeroInfoWithMovePointsProcess.MoveHeroAndAndActionBeforeEndingMove(battleInitialInfo.movementPath,
                    battleInitialInfo.heroId,
                    battleInitialInfo.movePointsLeft, () => {
                        _gameTimer.PauseTimer();
                        SceneManager.LoadSceneAsync("Battle" , LoadSceneMode.Additive);
                        _loadScreen.OpenLoadBar(StatesOfProgram.Battle);
                        _gameModel.EnterInBattleScene();
                        _battleModel.EnterInBattle();
                        _battleTurnTimer.StartTimer(battleInitialInfo.turnSeconds, false);
                    });
                if (_gameModel.IsAttacked)
                {
                    if(_gameModel.TryGetHeroModelObject(battleInitialInfo.assaulter.mapObjectId , out HeroModelObject heroModelObject))
                        _gameModel.SetHeroInFight(heroModelObject);
                    if (_gameModel.TryGetHeroModelObject(battleInitialInfo.defender.mapObjectId, out HeroModelObject heroModelObject1))
                        _gameModel.SetHeroOpponent(heroModelObject1);
                }
                else
                {
                    if (_gameModel.TryGetHeroModelObject(battleInitialInfo.defender.mapObjectId, out HeroModelObject heroModelObject))
                        _gameModel.SetHeroInFight(heroModelObject);
                    if (_gameModel.TryGetHeroModelObject(battleInitialInfo.assaulter.mapObjectId, out HeroModelObject heroModelObject1))
                        _gameModel.SetHeroOpponent(heroModelObject1);
                }
                break;
            case InputGameHeaders.BATTLE_TURN_NOTIFICATION_INFO:
                _battleTurnNotificationProcess.InitBattleTurnNotificationProcess(messageInput);
                break;
            case InputGameHeaders.BATTLE_HIT_RESULT_INFO:
                _battleModel.EnterCreatureInAction();
                _battleHitResultInfoProcess.InitHit(messageInput);
                break;
            case InputGameHeaders.BATTLE_MOVE_RESULT_INFO:
                _battleModel.EnterCreatureInAction();
                _battleMoveProcess.InitBattleMove(messageInput);
                break;
            case InputGameHeaders.BATTLE_MOVE_AND_HIT_RESULT_INFO:
                _battleModel.EnterCreatureInAction();
                _battleHitAndMoveProcess.StartBattleHitAndMoveProcess(messageInput);
                break;
            case InputGameHeaders.BATTLE_RESULT_INFO:
                _battleModel.EndGame(() =>
                {
                    BattleResultInfo battleResultInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<BattleResultInfo>(messageInput.body);
                    _resultPanel.OpenPanel(battleResultInfo.isWin);
                    _gameModel.SetLastFightResult(battleResultInfo.isWin);
                });

                break;
            case InputGameHeaders.BATTLE_ACTION_RESULT:
                break;
            case InputGameHeaders.BATTLE_TURN_ORDER_INFO:
                _battleTurnOrderProcess.InitTurn(messageInput);
                break;
            case InputGameHeaders.BATTLE_BLOCK_ACTIVATED_RESULT_INFO:
                _battleBlockActivetedProcess.BlockProcess(messageInput);
                break;
            case InputGameHeaders.BATTLE_WAIT_ACTIVATED_RESULT_INFO:
                _battleWaitActivatedProcess.WaitProcess(messageInput);
                break;
            case InputGameHeaders.NEW_DAY_STARTED_INFO:
                _newDayStartedInfoProcess.InitProcess(messageInput);
                break;
            case InputGameHeaders.FLAGGED_MINE_RESULT:
                _flaggedMineResultProcess.FlaggedMineResultHandler(messageInput);
                break;
            case InputGameHeaders.FLAGGED_MINE_INFO:
                _flaggedMineInfoProcess.FlaggedMineInfoHandler(messageInput);
                break;
            default:
                Debug.Log("Unhandled message");
                break;
        }
    }
}