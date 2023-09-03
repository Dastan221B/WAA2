using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


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

        private bool _canAddBuilding;
        private BuildingBuyWindow _buildingBuyWindow;
        private DicBuildingDTO _currentBuildingDTO;
       
        public void Init(BuildingBuyWindow buildingBuyWindow)
        {
            _buildingBuyWindow = buildingBuyWindow;
        }

        public void UpdateView(Sprite sprite ,DicBuildingDTO building, bool haveResourcesToAddBuilding, int currentLevelBuildingId , bool erectedAllBuildingsOfThisLevel, bool isBuilded)
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