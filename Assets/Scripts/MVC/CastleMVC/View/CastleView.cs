using Assets.Scripts.MVC.CastleSlots;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MVC.CastleMVC.View
{
    public class CastleView : MonoBehaviour
    {
        private TMP_Text _castleName;
        private CanHireCreatureSlots _canHireCreatureSlots;
        private StrategyCamera _strategyCamera;
        private GameObject _castle;
        private CastleCommandsSender _castleCommandsSender;
        private CastleModel _castleModel;
        private GameModel _gameModel;
        private SlotsModel _slotsModel;
        private Transform _terrainObjectsParent;
        private GameObject _buildings;
        private GameObject _playerPanel;
        private BuildingsListWindow _buildingsListWindow;
        private HeroSlot _heroGarrisonSlot;
        private HeroSlot _heroCasstleSlot;
        private Heroes _heroes;
        private CastleController _castleController;

        public bool OpenUI { get; private set; }

        public void Init(CastleController castleController,GameModel gameModel ,Heroes heroes,BuildingsListWindow buildingsListWindow,CastleCommandsSender castleCommandsSender, CastleModel castleModel, SlotsModel slotsModel)
        {
            _castleController = castleController;
            _gameModel = gameModel;
            _heroes = heroes;
            _buildingsListWindow = buildingsListWindow;
            _castleCommandsSender = castleCommandsSender;
            _castleModel = castleModel;
            _slotsModel = slotsModel;
        }

        public void Init(CanHireCreatureSlots canHireCreatureSlots, HeroSlot garrisonSlot, HeroSlot castleSlot, StrategyCamera strategyCamera, Button exitButton, Button openCouncilButton, GameObject castle, Transform terrainObjectsParent, GameObject buildings, GameObject playerPanel, TMP_Text castleName)
        {
            _canHireCreatureSlots = canHireCreatureSlots;
            _heroCasstleSlot = castleSlot;
            _heroGarrisonSlot = garrisonSlot;
            _strategyCamera = strategyCamera;
            _terrainObjectsParent = terrainObjectsParent;
            _castle = castle;
            _buildings = buildings;
            _playerPanel = playerPanel;
            _castleName = castleName;
            exitButton.onClick.AddListener(() => ExitFromCastle());
            openCouncilButton.onClick.AddListener(() => OpenCouncil());
        }


        public void SetAvilableCreatures(List<DicCreaturePack> dicCreaturePacks)
        {
            _canHireCreatureSlots.SetSpriteCreature(dicCreaturePacks, _castleController);
        }

        public void HandleHeroInGarrison(CastleObjectFullInfo castleObjectFullInfo)
        {
            if(castleObjectFullInfo.heroInCastle == null)
            {
                _heroCasstleSlot.ExitHero();
            }
            else
            {
                if(_gameModel.TryGetHeroModelObject(castleObjectFullInfo.heroInCastle.mapObjectId, out HeroModelObject heroModelObject))
                    _heroCasstleSlot.EnterHero(_heroes.GetHeroByID(castleObjectFullInfo.heroInCastle.dicHeroId).Icon , heroModelObject);
            }

            if (castleObjectFullInfo.heroInGarrison == null)
            {
                _heroGarrisonSlot.ExitHero();
            }
            else
            {
                if(_gameModel.TryGetHeroModelObject(castleObjectFullInfo.heroInGarrison.mapObjectId, out HeroModelObject heroModelObject))
                    _heroGarrisonSlot.EnterHero(_heroes.GetHeroByID(castleObjectFullInfo.heroInGarrison.dicHeroId).Icon , heroModelObject);
            }
        }

        public void Open()
        {
            OpenUI = true;
            _strategyCamera.enabled = false;
            _terrainObjectsParent.gameObject.SetActive(false);
            _castle.SetActive(true);
            _buildings.SetActive(true); 
            _playerPanel.SetActive(false);
            _castleName.text = _castleModel.CurrentDicCastleDTO.name;
        }

        public void Close()
        {
            OpenUI = false;
            _strategyCamera.enabled = true;
            _terrainObjectsParent.gameObject.SetActive(true);
            _castle.SetActive(false);
            _buildings.SetActive(false);
            _playerPanel.SetActive(true);
        }

        public void OpenCouncil()
        {
            _buildingsListWindow.Open();
        }

        public void ExitFromCastle()
        {
            var garrison = new List<ArmySlotInfo>(_slotsModel.GarrisonArmy).ExcludeNull();
            _castleCommandsSender.SendLeaveCastleRequest(_castleModel.CurrentCastleID, _castleModel.GarrisonID,
                new List<ArmySlotInfo>(_slotsModel.CastleArmy).ExcludeNull(), garrison);
        }

    }
}