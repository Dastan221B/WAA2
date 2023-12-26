using Assets.Scripts.MVC.CastleMVC;
using Assets.Scripts.MVC.CastleMVC.Buildinngs;
using Assets.Scripts.MVC.CastleMVC.View;
using Assets.Scripts.MVC.CastleMVC.View.SceneUIPack;
using Assets.Scripts.MVC.CastleSlots;
using Assets.Scripts.MVC.Game;
using Assets.Scripts.MVC.Game.Views;
using Assets.Scripts.MVC.Game.Views.UI;
using Assets.Scripts.MVC.Ground;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.MVC.HeroPanel;
using Assets.Scripts.MVC.TradeMVC;
using Assets.Scripts.MVC.Game.Path;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSceneLoaderTrigger : MonoBehaviour
{
    [SerializeField] private Button _tradeButton;
    [SerializeField] private Button _divideButton;
    [SerializeField] private HeroSlot _heroGarrisonSlot;
    [SerializeField] private HeroSlot _heroCasstleSlot;
    [SerializeField] private GameDateUI _gameDateUI;
    [SerializeField] private HeroSlot _heroSlotInGarisson;
    [SerializeField] private HeroSlot _heroSlotInCasstle;
    [SerializeField] private Button _surrenderButton;
    [SerializeField] private StrategyCamera _strategyCamera;
    //[SerializeField] private Camera _camera;
    [SerializeField] private Camera _mapCamera;

    [SerializeField] private GameObject _playerPanel;
    [SerializeField] private Button _sumbitButton;
    [SerializeField] private GameObject _tradePanel;
    [SerializeField] private HeroStats _requesterHeroStats;
    [SerializeField] private HeroStats _recieverHeroHeroStats;
    [SerializeField] private HeroCreaturesInventory _requesterHeroCreaturesInventory;
    [SerializeField] private HeroCreaturesInventory _recieverHeroCreaturesInventory;

    [SerializeField] private HeroPanelStatsWindow _inGameBarHeroPanelStatsWindow;
    [SerializeField] private Button _heroPanelCloseButton;
    [SerializeField] private GameObject _heroPanel;
    [SerializeField] private List<CreatureSlotHeroPanel> _heroPanelCreaturesSlots;

    [SerializeField] private HeroPanelStatsWindow _heroPanelStatsWindow;
    [SerializeField] private CreatureInfoWindow _creatureInfoWindow;
    [SerializeField] private List<Building> _buildings;
    [SerializeField] private GameCastleObjectsChangeService _gameCastleObjectsChangeService;
    [SerializeField] private CreatureStatsInfoWindow _creatureStatsInfoWindow;
    [SerializeField] private ResourcesViewUIPack _resourcesViewUIPack;
    [SerializeField] private BuildingBuyWindowUIPack _buildingBuyWindowUIPack;
    [SerializeField] private BuildingsListWindowUIPack _buildingsListWindowUIPack;
    [SerializeField] private HireCreatureBuildingWindowUIPack _hireCreatureBuildingWindowUIPack;
    [SerializeField] private List<CreatureSlot> _castleCreaturesSlots;
    [SerializeField] private List<CreatureSlot> _garrisonCreaturesSlots;
    [SerializeField] private CanHireCreatureSlots _canHireCreatureSlots;
    [SerializeField] private GameObject _buildingsObjects;
    [SerializeField] private GameObject _castle;
    [SerializeField] private TMP_Text _castleName;
    [SerializeField] private Button _castleExitButton;
    [SerializeField] private Button _councilButton;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Transform _terrainObjectsParent;
    [SerializeField] private Transform _sceneObjectParent;
    [SerializeField] private TerrainGenerator _terrainGenerator;
    [SerializeField] private Terrain _terrain;
    [SerializeField] private List<HeroModelObjectIcon> _heroModelObjectIcons;
    [SerializeField] private List<CastleIcon> _castleIcons;
    [SerializeField] private List<CastleIcon> _castleIconsInCastle;
    [SerializeField] private Button _startTurnButton;
    [SerializeField] private TurnInfoPanel _turnInfoPanel;
    public GameObject TradePanel => _tradePanel;
    public HeroStats RequesterHeroStats => _requesterHeroStats;
    public HeroStats RecieverHeroHeroStats => _recieverHeroHeroStats;
    private static bool _isLoaded;

    private void OnEnable()
    {
        if (_isLoaded)
            return;

        HeroPanelView heroPanelView = FindObjectOfType<HeroPanelView>();
        CastleView castleView = FindObjectOfType<CastleView>();
        ResourcesView resourcesView = FindObjectOfType<ResourcesView>();
        BuildingBuyWindow buildingBuyWindow = FindObjectOfType<BuildingBuyWindow>();
        BuildingsListWindow buildingsListWindow = FindObjectOfType<BuildingsListWindow>();
        HireCreatureBuildingWindow hireCreatureBuildingWindow = FindObjectOfType<HireCreatureBuildingWindow>();
        SlotsView slotsView = FindObjectOfType<SlotsView>();
        GameLoaderController gameLoaderController = FindObjectOfType<GameLoaderController>();
        GameTurnView gameTurnView = FindObjectOfType<GameTurnView>();
        GameBattleProcessResponseHandler gameProcessResponseHandler = FindObjectOfType<GameBattleProcessResponseHandler>();
        GroundController groundController = FindObjectOfType<GroundController>();
        GameController gameController = FindObjectOfType<GameController>();
        GameTimer turnTimer = FindObjectOfType<GameTimer>();
        SlotsController slotsController = FindObjectOfType<SlotsController>();
        CastleController castleController = FindObjectOfType<CastleController>();
        TradeView tradeView = FindObjectOfType<TradeView>();
        FindObjectOfType<HeroPanelController>().Init( _mapCamera,_heroModelObjectIcons,_creatureInfoWindow, _strategyCamera.GetComponent<Camera>());
        //FindObjectOfType<CoursorService>().SetCamera(_camera);
        FindObjectOfType<CastleUIIconPicker>().Init(_strategyCamera.GetComponent<Camera>(), _mapCamera);
        FindObjectOfType<HeroPathMover>().Init(_strategyCamera.GetComponent<Camera>(), _mapCamera);
        FindObjectOfType<GameButtonsView>().Init(_surrenderButton);
        tradeView.Init(_requesterHeroCreaturesInventory, _recieverHeroCreaturesInventory, _tradePanel, _sumbitButton);
        castleController.SetBuildings(_buildings);
        castleView.Init(_gameCastleObjectsChangeService,_canHireCreatureSlots, _heroGarrisonSlot, _heroCasstleSlot,_strategyCamera,_castleExitButton, _councilButton, _castle, _terrainObjectsParent, _buildingsObjects, _playerPanel, _castleName);
        slotsController.SetCreatureStatsInfoWindow(_creatureStatsInfoWindow);
        buildingBuyWindow.Init(_buildingBuyWindowUIPack);
        buildingsListWindow.Init(_buildingsListWindowUIPack);
        hireCreatureBuildingWindow.Init(_hireCreatureBuildingWindowUIPack);
        slotsView.InitSlots(_castleCreaturesSlots, _garrisonCreaturesSlots);
        groundController.SetTerrain(_terrain);
        groundController.SetTerrainGenerator(_terrainGenerator);
        heroPanelView.InitSlots(_inGameBarHeroPanelStatsWindow,_heroPanelStatsWindow, _heroPanel, _heroPanelCreaturesSlots, _heroPanelCloseButton);

        FindObjectOfType<SlotsController>().Init(_divideButton, _tradeButton, _heroSlotInGarisson, _heroSlotInCasstle);
        FindObjectOfType<GameDateView>().SetGameDataUI(_gameDateUI);
        turnTimer.SetTimerText(_timerText);

        if (gameController != null)
            gameController.SetSceneObjectsParent(_sceneObjectParent, _terrainObjectsParent, resourcesView);
        else
            Debug.LogError("Gamecontroller doesn't exist");

        if (gameLoaderController != null)
            gameLoaderController.LoadGame();
        else
            Debug.LogError("GameLoaderController doesn't exist");

        if (gameTurnView != null)
            gameTurnView.InitLoadedScene(_castleIconsInCastle,_heroModelObjectIcons, _castleIcons, _startTurnButton, _turnInfoPanel, _canvas);
        else
            Debug.LogError("Game view doesn't exist");

        if (gameProcessResponseHandler != null)
            gameProcessResponseHandler.InitBeforeLoaded(_gameCastleObjectsChangeService);
        else
            Debug.LogError("Game view doesn't exist");
        resourcesView.Init(_resourcesViewUIPack);
        _isLoaded = true;
    }

}