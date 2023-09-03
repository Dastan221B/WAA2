using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.MVC.CastleSlots;

namespace Assets.Scripts.MVC.HeroPanel
{
    public class HeroPanelView : MonoBehaviour
    {
        private HeroPanelStatsWindow _heroPanelStatsWindow;
        private HeroPanelStatsWindow _inGameBarHeroPanelStatsWindow;

        private Button _closeButton;
        private GameObject _panel;
        private List<CreatureSlotHeroPanel> _castleCreaturesSlots;

        private HeroPanelController _heroPanelController;
        private HeroPanelModel _heroModel;
        private ModelCreatures _modelCreatures;

        public void InitSlots(HeroPanelStatsWindow inGameBarHeroPanelStatsWindow,HeroPanelStatsWindow heroPanelStatsWindow,GameObject panel , List<CreatureSlotHeroPanel> castleCreaturesSlots, Button closeButton)
        {
            _inGameBarHeroPanelStatsWindow = inGameBarHeroPanelStatsWindow;
            _panel = panel;
            _castleCreaturesSlots = castleCreaturesSlots;
            _closeButton = closeButton;
            _heroPanelStatsWindow = heroPanelStatsWindow;
            for (int i = 0; i < _castleCreaturesSlots.Count; i++)
                _castleCreaturesSlots[i].SetSlotID(i);

            _closeButton.onClick.AddListener(() => ClosePanel());
        }


        public void OpenPanel()
        {
            _panel.SetActive(true);
            _heroPanelStatsWindow.SetValues(_heroModel.Icon, _heroModel.Name, _heroModel.Desctiption, _heroModel.Attack, _heroModel.Defence, _heroModel.Power, _heroModel.Knowledge , _heroModel.CastleArmy);
            UpdateCreaturesSlots();
        }

        public void ClosePanel()
        {
            _heroPanelController.Closed();
            _panel.SetActive(false);
        }

        public void SetGameBarHero(HeroModelObject heroModelObject , Sprite icon)
        {
            _inGameBarHeroPanelStatsWindow.SetValues(icon,heroModelObject.Hero.Name, " ", heroModelObject.Attack, heroModelObject.Defence, heroModelObject.Power, heroModelObject.Knowledge , heroModelObject.ArmySlotInfos);
        }

        public void Init(HeroPanelController heroPanelController,HeroPanelModel slotsModel, ModelCreatures modelCreatures)
        {
            _heroPanelController = heroPanelController;
            _heroModel = slotsModel;
            _modelCreatures = modelCreatures;
        }

        public void UpdateCreaturesSlots()
        {
            foreach (var item in _castleCreaturesSlots)
                item.ResetSlot();

            for (int i = 0; i < _heroModel.CastleArmy.Count; i++)
            {
                if (_heroModel.CastleArmy[i] != null)
                {
                    _heroModel.CastleArmy[i].stackSlot = _castleCreaturesSlots[i].SlotID;
                    _castleCreaturesSlots[i].SetCreatureInSlot(_modelCreatures.GetIconById((int)_heroModel.CastleArmy[i].dicCreatureId - 1), _heroModel.CastleArmy[i]);
                }
            }
        }
    }
}