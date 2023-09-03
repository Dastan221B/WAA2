using Assets.Scripts.MVC.Game.Views.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MVC.HeroPanel
{
    public class HeroPanelController : MonoBehaviour
    {
        private HeroPanelSelector _panelSelector;
        private CreatureInfoWindow _creatureStatsInfoWindow;
        private ProgramState _programState;
        private HeroPanelModel _slotsModel;
        private CommonData _commonData;
        private ModelCreatures _modelCreatures;
        private HeroPanelView _heroPanelView;
        private Camera _mainCamera;
        private List<HeroModelObjectIcon> _heroModelObjectIcons;

        private float _clicked = 0;
        private float _clicktime = 0;
        private float _clickdelay = 0.5f;

        public void Init(HeroPanelView heroPanelView,ModelCreatures modelCreatures, CommonData commonData,
            ProgramState programState, HeroPanelSelector panelSelector, HeroPanelModel heroModel)
        {
            _heroPanelView = heroPanelView;
            _modelCreatures = modelCreatures;
            _commonData = commonData;
            _programState = programState;
            _panelSelector = panelSelector;
            _slotsModel = heroModel;
            _heroPanelView = heroPanelView;
        }

        public void Init(List<HeroModelObjectIcon> heroModelObjectIcons, CreatureInfoWindow creatureStatsInfoWindow , Camera camera)
        {
            _heroModelObjectIcons = heroModelObjectIcons;
            _mainCamera = camera;
            _creatureStatsInfoWindow = creatureStatsInfoWindow;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                PickHeroAndOpenPanel();
            }
            if (_programState.StatesOfProgram != StatesOfProgram.HeroPanel)
            return;


            if (Input.GetMouseButtonDown(0))
            {
                PickSlot();
            }
        }

        public void Closed()
        {
            _programState.GameStartHandler();
        }

        public void SelectPlayer(HeroModelObjectIcon heroModelObjectIcon)
        {
            foreach (var item in _heroModelObjectIcons)
                if (item.HeroModelObject != null)
                    item.OffFrame();

            if (heroModelObjectIcon.HeroModelObject != null)
                heroModelObjectIcon.OnFrame();

            _heroPanelView.SetGameBarHero(heroModelObjectIcon.HeroModelObject, heroModelObjectIcon.Icon);
        }

        private void PickHeroAndOpenPanel()
        {
            if (_panelSelector.TryPickHeroSlot(out HeroModelObjectIcon heroModelObjectIcon))
            {
                _mainCamera.GetComponent<StrategyCamera>().enabled = false;
                if (heroModelObjectIcon.HeroModelObject != null)
                {
                    _mainCamera.transform.position = new Vector3(heroModelObjectIcon.HeroModelObject.transform.position.x,
                                          _mainCamera.transform.position.y,
                                          heroModelObjectIcon.HeroModelObject.transform.position.z - 5);
                }

                _mainCamera.GetComponent<StrategyCamera>().enabled = true;
                SelectPlayer(heroModelObjectIcon);

                _clicked++;
                if (_clicked == 1) _clicktime = Time.time;

                if (_clicked > 1 && Time.time - _clicktime < _clickdelay)
                {
                    _clicked = 0;
                    _clicktime = 0;
                    _slotsModel.SetHeroModelObject(heroModelObjectIcon.HeroModelObject, heroModelObjectIcon.Icon);
                    _heroPanelView.OpenPanel();
                    _programState.HeroPanelStartHandler();
                }
                else if (_clicked > 2 || Time.time - _clicktime > 1) _clicked = 0;

            }
        }

        private void PickSlot()
        {
            if (_panelSelector.TryPickSlot(out CreatureSlotHeroPanel creatureSlotHeroPanel))
            {
                if (creatureSlotHeroPanel != null)
                {
                    if (_commonData.TryGetDicCreatureDTOByID((int)creatureSlotHeroPanel.ArmySlotInfo.dicCreatureId, out DicCreatureDTO dicCreatureDTO))
                    {
                        _creatureStatsInfoWindow.Open(dicCreatureDTO, _modelCreatures.GetIconById((int)creatureSlotHeroPanel.ArmySlotInfo.dicCreatureId - 1));

                    }
                }
            }
        }
    }
}