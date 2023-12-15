using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.MVC.CastleMVC.View
{
    public class BuildingSlot : MonoBehaviour, IPointerDownHandler
    {
        public int CurrentLevelBuildingId { get; private set; }
        [SerializeField] private TMP_Text _buildingName;
        [SerializeField] private Image _buildingImage;
        [SerializeField] private Image _statusImage;
        [SerializeField] private Sprite _canBuySprite;
        [SerializeField] private Sprite _notAvalableResourcesSprite;
        [SerializeField] private Sprite _cantBuySprite;
        public IEnumerable<int> BuildingIds { get; set; }

        private bool _canAddBuilding;
        private BuildingBuyWindow _buildingBuyWindow;
        private DicBuildingDTO _currentBuildingDTO;
        private CommonData _commonData;
        private Buildings _buildings;
        private BuildingsListWindow _buildingsListWindow;

        public void Init(BuildingBuyWindow buildingBuyWindow,
            CommonData commonData, 
            Buildings buildings,
            BuildingsListWindow buildingsListWindow)
        {
            _buildingsListWindow = buildingsListWindow;
            _buildingBuyWindow = buildingBuyWindow;
            _commonData = commonData;
            _buildings = buildings;
        }

        //public void UpdateView(IEnumerable<int> buildingsWithSameLevelInCastle)
        //{
        //    bool erectedAllBuildingsOfThisLevel = BuildingIds.Count() == buildingsWithSameLevelInCastle.Count();

        //    if (buildingsWithSameLevelInCastle.Count() == 0)
        //    {
        //        CurrentLevelBuildingId = BuildingIds.ToList()[0];
        //    }
        //    else if (erectedAllBuildingsOfThisLevel)
        //    {
        //        CurrentLevelBuildingId = buildingsWithSameLevelInCastle.Last();
        //    }
        //    else
        //    {
        //        CurrentLevelBuildingId = BuildingIds.ToList()[Array.IndexOf(BuildingIds.ToArray(), buildingsWithSameLevelInCastle.Last())];
        //    }

        //    DicBuildingDTO building = _commonData.BuildingDictianory[CurrentLevelBuildingId];

        //    bool haveResourcesToAddBuilding = _buildingsListWindow.CanAddBuilding(building.dependencySet, building.price);
        //    _canAddBuilding = haveResourcesToAddBuilding && !erectedAllBuildingsOfThisLevel;

        //    _buildingName.text = building.name;
        //    _buildingImage.sprite = _buildings.GetSpriteByID(CurrentLevelBuildingId - 1);

        //    if (_canAddBuilding)
        //    {
        //        _statusImage.sprite = _canBuySprite;
        //        return;
        //    }

        //    if (!haveResourcesToAddBuilding && !erectedAllBuildingsOfThisLevel)
        //    {
        //        _statusImage.sprite = _cantBuySprite;
        //        return;
        //    }
        //    if (erectedAllBuildingsOfThisLevel)
        //    {
        //        _statusImage.sprite = _notAvalableResourcesSprite;
        //        return;
        //    }
        //    if (haveResourcesToAddBuilding && !erectedAllBuildingsOfThisLevel)
        //    {
        //        _statusImage.sprite = _canBuySprite;
        //    }
        //}

        public void UpdateView(Sprite sprite, DicBuildingDTO building, bool haveResourcesToAddBuilding, int currentLevelBuildingId, bool erectedAllBuildingsOfThisLevel, bool isBuilded)
        {
            _currentBuildingDTO = building;
            CurrentLevelBuildingId = currentLevelBuildingId;
            _canAddBuilding = haveResourcesToAddBuilding && !erectedAllBuildingsOfThisLevel;

            _buildingName.text = building.name;
            _buildingImage.sprite = sprite;
            _statusImage.color = Color.white;
            if (isBuilded)
            {
                _statusImage.sprite = _canBuySprite;
                return;
            }

            if (!haveResourcesToAddBuilding && !erectedAllBuildingsOfThisLevel)
            {
                _statusImage.sprite = _cantBuySprite;
                return;
            }
            if (erectedAllBuildingsOfThisLevel)
            {
                _statusImage.sprite = _notAvalableResourcesSprite;
                return;
            }
            if (haveResourcesToAddBuilding && !erectedAllBuildingsOfThisLevel)
            {
                _statusImage.sprite = _notAvalableResourcesSprite;
                _statusImage.color = Color.green;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _buildingBuyWindow.Open(_currentBuildingDTO, _canAddBuilding, CurrentLevelBuildingId);
        }
    }

}