using Assets.Scripts.GameResources.MapCreatures;
using Assets.Scripts.MVC.Battle;
using Assets.Scripts.MVC.Battle.BattleLoader;
using Assets.Scripts.MVC.Battle.BattleProcess;
using Assets.Scripts.MVC.Battle.Views;
using Assets.Scripts.MVC.Game;
using System.Collections;
using UnityEngine;

public class BattleCompositeRoot : CompositeRoot
{
    [SerializeField] private LayerMask _hexagonLayerMask;
    [Header("Components")]
    [SerializeField] private BattleModel _battleModel;
    [SerializeField] private BattleTurnOrderProcess _battleTurnOrderProcess;
    [SerializeField] private GameObject _hexagonFramePrefab;
    [SerializeField] private ResultPanelCreatureItem _resultPanelCreatureItemPrefab;
    [SerializeField] private LoadScreen _loadScreen;
    [SerializeField] private ResultPanel _resultPanel;
    [SerializeField] private BattleView _battleView;
    [SerializeField] private HeroModelObjects _heroModelObjects;
    [SerializeField] private CommonData _commonData;
    [SerializeField] private HexagonPicker _hexagonPicker;
    [SerializeField] private BattleController _battleController;
    [SerializeField] private CreaturePathMover _creaturePathMover;
    [SerializeField] private ProgramState _programState;
    [SerializeField] private GameBattleProcessResponseHandler _gameProcessResponseHandler;
    [SerializeField] private BattleInitalProcess _battleInitalProcess;
    [SerializeField] private HexagonGenerator _hexagonGenerator;
    [SerializeField] private CreatureSpawner _creatureSpawner;
    [SerializeField] private GameCompositeRoot _gameCompositeRoot;

    [Header("Preafabs")]
    [SerializeField] private Hexagon _hexagonPrefab;
    [SerializeField] private ModelCreatures _modelCreatures;

    private HexagonFieldSelector _hexagonFieldSelecter;
    private BattleMoveProcess _battleMoveProcess;
    private BattleHitAndMoveProcess _battleHitAndMoveProcess;
    private BattleBlockActivetedProcess _battleBlockActivetedProcess;
    private BattleWaitActivatedProcess _battleWaitActivatedProcess;
    private BattleHitResultInfoProcess _battleHitResultInfoProcess;
    private EndGameProcess _endGameProcess;

    public override void Composite()
    {
        _battleModel.Init(_modelCreatures, _commonData, _hexagonGenerator);
        _endGameProcess = new EndGameProcess(_battleModel, _loadScreen, _gameCompositeRoot.GameModel);
        _resultPanel.Init( _battleModel ,_endGameProcess, _resultPanelCreatureItemPrefab, _modelCreatures);
        _hexagonGenerator.Init(_hexagonPrefab, _hexagonFramePrefab);
        _creatureSpawner.Init(_commonData, _modelCreatures, _battleModel, _resultPanel);
        _battleInitalProcess.Init(_battleModel, _hexagonGenerator, _creatureSpawner, _heroModelObjects);
        _creaturePathMover.Init(_battleModel, 3);
        _battleWaitActivatedProcess = new BattleWaitActivatedProcess(_battleModel);
        _battleBlockActivetedProcess = new BattleBlockActivetedProcess(_battleModel);
        _battleMoveProcess = new BattleMoveProcess(_battleModel, _creaturePathMover);
        _battleHitResultInfoProcess = new BattleHitResultInfoProcess(_battleModel);
        _battleHitAndMoveProcess = new BattleHitAndMoveProcess(_creaturePathMover, _battleModel);
        _gameCompositeRoot.GameAndBattleCommands.Init(_battleModel);
        _battleView.Init(_modelCreatures, _gameCompositeRoot.GameAndBattleCommands);
        _battleTurnOrderProcess.Init(_battleView, _battleModel);
        _gameProcessResponseHandler.Init(_battleTurnOrderProcess, _battleModel, _battleInitalProcess, _battleMoveProcess, _battleHitAndMoveProcess , _battleBlockActivetedProcess, _battleWaitActivatedProcess, _resultPanel , _battleHitResultInfoProcess);
        _hexagonFieldSelecter = new HexagonFieldSelector( _modelCreatures,_hexagonPicker, _battleModel, _hexagonLayerMask);
        _battleController.Init(_battleModel,_programState, _hexagonPicker, _gameCompositeRoot.GameAndBattleCommands, _hexagonFieldSelecter, _loadScreen);
        _battleInitalProcess.OnBattleInited += _programState.BattleStartHandler;
        //_battleModel.OnInitedCreatures += _battleView.InitBattle;
        _battleModel.OnSelectedCurrentActiveCreature += _battleView.SetCurrentAcriveCreature;
    }
} 