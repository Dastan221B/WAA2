using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Scripts.MVC.CastleMVC.View.SceneUIPack;

namespace Assets.Scripts.MVC.CastleMVC.View
{
    public class BuildingBuyWindow : MonoBehaviour
    {
        public event Action OnBuyed;

        private GameObject _panel;
        private Button _confirm;
        private Button _cancel;
        private TMP_Text _buildingDependencies;
        private TMP_Text _buildingName;
        private Image _buildingImg;
        private BuildingBuyWindowPriceItem[] _buildingPriceResources;

        private ProgramState _programState;
        private CastleCommandsSender _castleCommandsSender;
        private Buildings _buildings;
        private CommonData _commonData;
        private CastleModel _castleModel;
        private int _buidldingID;


        public void Init(BuildingBuyWindowUIPack buildingBuyWindowUIPack)
        {
            _panel = buildingBuyWindowUIPack.Panel;
            _confirm = buildingBuyWindowUIPack.Confirm;
            _cancel = buildingBuyWindowUIPack.Cancel;
            _buildingDependencies = buildingBuyWindowUIPack.BuildingDependencies;
            _buildingName = buildingBuyWindowUIPack.BuildingName;
            _buildingImg = buildingBuyWindowUIPack.BuildingImg;
            _buildingPriceResources = buildingBuyWindowUIPack.BuildingPriceResources;

            for (int i = 0; i < _buildingPriceResources.Length; i++)
            {
                _buildingPriceResources[i].SetResouceID(i + 1);
            }

            _cancel.onClick.AddListener(() => Close());
            _confirm.onClick.AddListener(() => Buy());
        }

        public void Init(ProgramState programState, CastleCommandsSender castleCommandsSender, Buildings buildings, CommonData commonData, CastleModel castleModel)
        {
            _programState = programState;
            _castleCommandsSender = castleCommandsSender;
            _buildings = buildings;
            _commonData = commonData;
            _castleModel = castleModel;
        }

        public void Buy()
        {
            _castleCommandsSender.SendAddCastleBuildingRequest(_castleModel.CurrentCastleID, _buidldingID);
            Close();
            OnBuyed?.Invoke();
        }

        public void Open(DicBuildingDTO building, bool canAddBuilding, int buildingID)
        {
            _panel.SetActive(true);
            string dependenciesText = null;
            _buidldingID = buildingID;
            List<int> buildingsInCastle = _castleModel.CurrentCastleFullOjbectInfo.buildings;
            var notBuiltBuildings = building.dependencySet.Except(buildingsInCastle);
            var similarBuildings = notBuiltBuildings.Intersect(_commonData.BuildingDictianory.Values.Select(b => (int)b.id));

            foreach (int id in similarBuildings)
            {
                dependenciesText += _commonData.BuildingDictianory[id].name + ", ";
            }

            if (dependenciesText != null)
            {
                dependenciesText = dependenciesText.Remove(dependenciesText.Length - 2, 2);
                dependenciesText += ".";
                _buildingDependencies.text = dependenciesText;
            }
            else
            {
                _buildingDependencies.text = "All buildings are erected.";
            }

            var builingPrices = _buildingPriceResources.Select(r => r.ResourceId).Intersect(building.price.Select(o => o.id));
            builingPrices.ToList().ForEach(p =>
            {
                _buildingPriceResources[p - 1].SetPrice(building.price.Where(price => price.id == p).First().amount);
                _buildingPriceResources[p - 1].gameObject.SetActive(true);
            });
            _buildingPriceResources.Select(r => r.ResourceId).Except(builingPrices).ToList().ForEach(p =>
            {
                _buildingPriceResources[p - 1].gameObject.SetActive(false);
            });

            _confirm.interactable = canAddBuilding;

            _buildingImg.sprite = _buildings.GetSpriteByID((int)building.id - 1);
            _buildingName.text = building.name;
        }

        public void Close()
        {
            _panel.SetActive(false);
        }

    }
}