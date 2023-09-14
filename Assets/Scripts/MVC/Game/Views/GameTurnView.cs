using Assets.Scripts.MVC.Game.Views.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Scripts.Interfaces.Game;
using UnityEngine.UI;
using Assets.Scripts.GameResources;
using System.Linq;
using Assets.Scripts.MVC.HeroPanel;

namespace Assets.Scripts.MVC.Game.Views
{
    public class GameTurnView : MonoBehaviour
    {
        private GameObject _canvasGameObject;
        private Sprite _baseImage;
        private Button _startTurnButton;
        private TurnInfoPanel _turnInfoPanel;
        private List<HeroModelObjectIcon> _heroModelObjectIcons;
        private List<CastleIcon> _castleIcons;
        private List<CastleIcon> _castleIconsInCastle;
        private SystemColors _systemColors;
        private HeroPanelController _heroPanelController;
        private GameController _gameController;

        private ISendStartTurnRequest _sendStartTurnRequest;
        private GameModel _gameModel;

        public void Init(GameController gameController,HeroPanelController heroPanelController,GameModel gameModel,
            SystemColors systemColors,
            ISendStartTurnRequest sendStartTurnRequest)
        {
            _gameController = gameController;
            _heroPanelController = heroPanelController;
            _sendStartTurnRequest = sendStartTurnRequest;
            _systemColors = systemColors;
            _gameModel = gameModel;
        }

        public void InitLoadedScene(List<CastleIcon> castleIconsInCastle, List<HeroModelObjectIcon> heroModelObjectIcons, List<CastleIcon> castleIcons,Button startTurnButton,TurnInfoPanel turnInfoPanel, GameObject canvas)
        {
            _castleIconsInCastle = castleIconsInCastle;
            _turnInfoPanel = turnInfoPanel;
            _heroModelObjectIcons = heroModelObjectIcons;
            _castleIcons = castleIcons;
            _startTurnButton = startTurnButton;
            _canvasGameObject = canvas;
            _startTurnButton.onClick.AddListener(StartTurn);
        }

        public void SelectHeroObject(HeroModelObject heroModelObject)
        {
            HeroModelObjectIcon heroModelObjectIcon = _heroModelObjectIcons.FirstOrDefault(item => item.HeroModelObject == heroModelObject);

            if (heroModelObjectIcon != null)
            {
                heroModelObjectIcon.OnFrame();
            }
        }

        public void ResetDisplayHeroes()
        {
            for (int i = 0; i < _heroModelObjectIcons.Count; i++)
                _heroModelObjectIcons[i].SetBaseIcon(_baseImage);

            for (int i = 0; i < _gameModel.HeroModelObjectsTurn.Count; i++)
                if (i < _heroModelObjectIcons.Count && !_gameModel.HeroModelObjectsTurn[i].InCastle)
                    _heroModelObjectIcons[i].SetHeroModelObject(_gameModel.HeroModelObjectsTurn[i]);

            for(int i = 0; i < _gameModel.HeroModelObjectsTurn.Count; i++)
            {
                if (i < _heroModelObjectIcons.Count && !_gameModel.HeroModelObjectsTurn[i].InCastle)
                {
                    _heroPanelController.SelectPlayer(_heroModelObjectIcons[i]);
                    _gameController.SelectHero(_gameModel.HeroModelObjectsTurn[i]);
                    break;
                }
            }

        }

        public void UpdateTurnView()
        {
            for (int i = 0; i < _castleIcons.Count; i++)
                    _castleIcons[i].SetBaseIcon(_baseImage);

            for (int i = 0; i < _castleIconsInCastle.Count; i++)
                _castleIconsInCastle[i].SetBaseIcon(_baseImage);

            for (int i = 0; i < _gameModel.HeroModelObjectsTurn.Count; i++)
                if (i < _heroModelObjectIcons.Count)
                    _heroModelObjectIcons[i].SetHeroModelObject(_gameModel.HeroModelObjectsTurn[i]);

            for (int i = 0; i < _gameModel.CastlesTurn.Count; i++)
                if (i < _castleIcons.Count)
                    _castleIcons[i].SetCastle(_gameModel.CastlesTurn[i]);

            Debug.Log("_gameModel.CastlesTurn " + _gameModel.CastlesTurn.Count);
            for (int i = 0; i < _gameModel.CastlesTurn.Count; i++)
                if (i < _castleIconsInCastle.Count)
                    _castleIconsInCastle[i].SetCastle(_gameModel.CastlesTurn[i]);

            for (int i = 0; i < _gameModel.HeroModelObjectsTurn.Count; i++)
            {
                if (i < _heroModelObjectIcons.Count && !_gameModel.HeroModelObjectsTurn[i].InCastle)
                {
                    _heroPanelController.SelectPlayer(_heroModelObjectIcons[i]);
                    _gameController.SelectHero(_gameModel.HeroModelObjectsTurn[i]);
                    break;
                }
            }
        }

        public void EnteredInTurn(Player player)
        {
            Debug.Log("Plyer entered in turn " + player.Ordinal);
            _turnInfoPanel.OpenForSelf(_systemColors.GetColorByOrdinal(player.Ordinal) ,player.UserInfo.UserName);
            
        }

        public void ExitFromTurn()
        {
            //for (int i = 0; i < _battleModel.HeroModelObjectsTurn.Count; i++)
            //    _heroModelObjectIcons[i].SetBaseIcon(_baseImage);
            //for (int i = 0; i < _battleModel.CastlesTurn.Count; i++)
            //    _castleIcons[i].SetBaseIcon(_baseImage);
            //for (int i = 0; i < _battleModel.CastlesTurn.Count; i++)
            //    _castleIconsInCastle[i].SetBaseIcon(_baseImage);
        }

        public void EnterInBattleSceneHandler()
        {
            _turnInfoPanel.Close();
            _canvasGameObject.SetActive(false);
        }

        public void EnterInGameSceneFromBattleSceneHandler()
        {
            _turnInfoPanel.Close();
            _canvasGameObject.SetActive(true);
        }

        public void DisplayPlayerTurnInfo(Player player)
        {
            _turnInfoPanel.OpenForSelf(_systemColors.GetColorByOrdinal(player.Ordinal), player.UserInfo.UserName);
        }

        public void StartTurn()
        {
            _sendStartTurnRequest.SendStartTurnRequest();
        }

        public void CloseOffPanel()
        {
            _turnInfoPanel.Close();
        }

    }
}