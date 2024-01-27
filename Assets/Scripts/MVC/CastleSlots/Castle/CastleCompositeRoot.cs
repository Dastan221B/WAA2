using Assets.Scripts.Client.GameClient;
using Assets.Scripts.GameResources;
using Assets.Scripts.MVC.CastleMVC;
using Assets.Scripts.MVC.CastleMVC.Buildinngs;
using Assets.Scripts.MVC.CastleMVC.CastleProcess;
using Assets.Scripts.MVC.CastleMVC.View;
using Assets.Scripts.MVC.CastleSlots;
using Assets.Scripts.MVC.Game.Views;
using Assets.Scripts.MVC.HeroPanel;
using Assets.Scripts.MVC.TradeMVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleCompositeRoot : CompositeRoot
{
    [SerializeField] private GameTurnView _turnView;
    [SerializeField] private TradeController _tradeController;
    [SerializeField] private SlotsModel _slotsModel;
    [SerializeField] private HeroPanelView _heroPanel;
    [SerializeField] private Heroes _heroes;
    [SerializeField] private CastleUIIconPicker _castleUIIconPicker;
    [SerializeField] private CastleView _castleView;
    [SerializeField] private BuildingsPicker _buildingsPicker;
    [SerializeField] private CastleController _castleController;
    [SerializeField] private GameBattleProcessResponseHandler _gameBattleProcessResponseHandler;
    [SerializeField] private ResourcesDataService _resourcesDataService;
    [SerializeField] private ResourcesView _resourcesView;
    [SerializeField] private ModelCreatures _modelCreatures;
    [SerializeField] private SlotsView _slotsView;
    [SerializeField] private ProgramState _programState;
    [SerializeField] private SlotPicker _slotPicker;
    [SerializeField] private SlotsController _slotsController;
    [SerializeField] private HireCreatureBuildingWindow _hireCreatureBuildingWindow;
    [SerializeField] private CastleBuildingsView _castleBuildingsView;
    [SerializeField] private BuildingsListWindow _buildingsListWindow;
    [SerializeField] private Buildings _buildings;
    [SerializeField] private GameMessageSender _gameMessageSender;
    [SerializeField] private BuildingBuyWindow _buildingBuyWindow;
    [SerializeField] private CommonData _commonData;
    [SerializeField] private GameCompositeRoot _gameCompositeRoot;

    private OpenCastleProcess _openCastleProcess;
    private AddBuildingToCastleProcess _addBuildingToCastleProcess;
    private HireCreatureProcess _hireCreatureProcess;
    private CastleCommandsSender _castleCommandsSender;
    private CastleModel _castleModel;
    private HeroEnterInCastleResultProcess _heroEnterInCastleResultProcess;
    private LeaveCastleProcess _leaveCastleProcess;

    public SlotsModel SlotsModel => _slotsModel;

    public override void Composite()
    {
        _castleCommandsSender = new CastleCommandsSender(_gameMessageSender,_gameCompositeRoot.GameModel);
        _castleModel = new CastleModel(_castleView,_commonData, _gameCompositeRoot.GameModel, _slotsController, _heroes);
        _buildingBuyWindow.Init( _programState,_castleCommandsSender, _buildings, _commonData, _castleModel);
        _buildingsListWindow.Init(_programState, _resourcesDataService,_buildingBuyWindow, _castleModel, _commonData, _buildings);
        _castleBuildingsView.Init(_castleModel);
        _castleController.Init(_tradeController,_castleModel, _castleCommandsSender, _programState, _buildingsPicker, _hireCreatureBuildingWindow, _buildingsListWindow, _commonData);
        _heroEnterInCastleResultProcess = new HeroEnterInCastleResultProcess(_castleCommandsSender, _gameCompositeRoot.MoveHeroInfoWithMovePointsProcess);
        _slotsView.Init(_slotsModel, _modelCreatures);
        _slotsController.Init(_gameCompositeRoot.TradeStartedResultProcess,_gameCompositeRoot.GameAndBattleCommands,_castleCommandsSender,_modelCreatures,_commonData,_programState, _slotPicker, _slotsModel, _heroes, _castleModel);
        _leaveCastleProcess = new LeaveCastleProcess(_heroPanel, _programState, _gameCompositeRoot.GameModel, _castleView);
        _openCastleProcess = new OpenCastleProcess(_turnView,_leaveCastleProcess, _gameCompositeRoot.GameTimer,_commonData,_programState,_slotsModel, _castleView,_gameCompositeRoot.GameModel, _castleModel, _castleBuildingsView);
        _addBuildingToCastleProcess = new AddBuildingToCastleProcess(_commonData,_castleModel);
        _hireCreatureProcess = new HireCreatureProcess(_hireCreatureBuildingWindow, _slotsModel, _castleModel, _commonData);
        _castleView.Init(_leaveCastleProcess, _castleController,_gameCompositeRoot.GameModel,_heroes,_buildingsListWindow,_castleCommandsSender, _castleModel, _slotsModel);
        _resourcesView.Init(_resourcesDataService);
        _gameCompositeRoot.GameController.Init(_castleCommandsSender, _castleModel);
        _castleUIIconPicker.Init(_castleView,_gameCompositeRoot.GameModel,_programState, _castleCommandsSender);
        _hireCreatureBuildingWindow.Init(_programState, _castleCommandsSender, _castleModel, _modelCreatures, _slotsModel);
        _gameBattleProcessResponseHandler.Init(_castleModel,_openCastleProcess, _addBuildingToCastleProcess, _hireCreatureProcess, _heroEnterInCastleResultProcess, _leaveCastleProcess, _slotsModel, _castleView);
        _slotsModel.OnUpdatedCastleArmy += _slotsView.UpdateCastleCreaturesSlots;
        _slotsModel.OnUpdatedGarrisonArmy += _slotsView.UpdateGarrisonCreaturesSlots;
        _resourcesDataService.OnUpdatedResources += _resourcesView.UpdateResources;
        //_commonData.OnEndedLoadingBuildings += _castleModel.InitBuildingsIDS;
    }
}