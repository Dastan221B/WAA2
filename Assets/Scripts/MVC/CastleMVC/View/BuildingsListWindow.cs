using Assets.Scripts.GameResources;
using Assets.Scripts.MVC.CastleMVC.View.SceneUIPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MVC.CastleMVC.View
{
    public class BuildingsListWindow : BuildingWindow
    {
        private Button _close;
        private GameObject _panel;
        private BuildingSlot[] _buildingSlots;
       
        private ResourcesDataService _resourcesDataService;
        private CastleModel _castleModel;       
        private CommonData _commonData;
        private Buildings _buildings;
        private BuildingBuyWindow _buildingBuyWindow;
        private ProgramState _programState;

        public void Init(BuildingsListWindowUIPack buildingsListWindowUIPack)
        {
            _close = buildingsListWindowUIPack.Close;
            _panel = buildingsListWindowUIPack.Panel;
            _buildingSlots = buildingsListWindowUIPack.BuildingSlots;
            foreach (var item in _buildingSlots)
                item.Init(_buildingBuyWindow);

            _close.onClick.AddListener(() => Close());

        }

        public void Init(ProgramState programState ,ResourcesDataService resourcesDataService,BuildingBuyWindow buildingBuyWindow, CastleModel castleModel , CommonData commonData , Buildings buildings)
        {
            _programState = programState;
            _resourcesDataService = resourcesDataService;
               _castleModel = castleModel;
            _commonData = commonData;
            _buildings = buildings;
            _buildingBuyWindow = buildingBuyWindow;
            _buildingBuyWindow.OnBuyed += Close;

        }

        public override void Close()
        {
            _programState.CastleStartHandler();
            _panel.SetActive(false);
        }

        public override void Open()
        {
            _panel.SetActive(true);
            _programState.CastleUIWindowsStartHandler();
            var buildings = _castleModel.CurrentDicCastleDTO.buildingSet.GroupBy(bId => _commonData.BuildingDictianory[bId].level);
            var buildingsEnumerator = buildings.GetEnumerator();
            for (int i = 0; i < _buildingSlots.Length; i++)
            {
                if (buildingsEnumerator.MoveNext())
                {
                    _buildingSlots[i].gameObject.SetActive(true);
                    var buildingIds = buildingsEnumerator.Current;
                    var buildingsWithSameLevelInCastle = _castleModel.CurrentCastleFullOjbectInfo.buildings.Where(bId => _commonData.BuildingDictianory[bId].level == buildingsEnumerator.Current.Key);
                    bool erectedAllBuildingsOfThisLevel = buildingIds.Count() == buildingsWithSameLevelInCastle.Count();
                    int currentLevelBuildingId = 0;
                    bool isBuilded = false;

                    if (buildingsWithSameLevelInCastle.Count() == 0)
                    {
                        currentLevelBuildingId = buildingIds.ToList()[0];
                    }
                    else if (erectedAllBuildingsOfThisLevel)
                    {
                        currentLevelBuildingId = buildingsWithSameLevelInCastle.Last();
                    }
                    else
                    {
                        currentLevelBuildingId = buildingIds.ToList()[Array.IndexOf(buildingIds.ToArray(), buildingsWithSameLevelInCastle.Last())];
                    }

                    if (_castleModel.CurrentCastleFullOjbectInfo.buildings.Contains(currentLevelBuildingId))
                        isBuilded = true;

                    DicBuildingDTO building = _commonData.BuildingDictianory[currentLevelBuildingId];

                    bool haveResourcesToAddBuilding = CanAddBuilding(building.dependencySet, building.price);

                    _buildingSlots[i].UpdateView(_buildings.GetSpriteByID(currentLevelBuildingId - 1), building,haveResourcesToAddBuilding, currentLevelBuildingId, erectedAllBuildingsOfThisLevel, isBuilded);
                }
                else
                {
                    _buildingSlots[i].gameObject.SetActive(false);
                }
            }
        }

        public bool CanAddBuilding(List<int> buildingDependencies, List<ObjectPrice> price)
        {
            List<int> buildingsInCastle = _castleModel.CurrentCastleFullOjbectInfo.buildings;
            var notBuiltBuildings = buildingDependencies.Except(buildingsInCastle);
            var similarBuilings = notBuiltBuildings.Intersect(_commonData.BuildingDictianory.Values.Select(b => (int)b.id));

            if (similarBuilings.Count() > 0)
                return false;

            Dictionary<int, int> playerResources = _resourcesDataService.GetResources();
            foreach (ObjectPrice buildingPrice in price)
            {
                if (buildingPrice.amount > playerResources[buildingPrice.id])
                {
                    return false;
                }
            }

            return true;
        }
    }
}