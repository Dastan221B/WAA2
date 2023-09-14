using Assets.Scripts.MVC.CastleMVC.Buildinngs;
using Assets.Scripts.MVC.CastleMVC.View;
using Assets.Scripts.MVC.Game;
using Assets.Scripts.MVC.TradeMVC;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.MVC.CastleMVC
{
    public class CastleController : MonoBehaviour
    {
        private CastleModel _castleModel;
        private CastleCommandsSender _castleCommandsSender;
        private ProgramState _programState;
        private BuildingsPicker _buildingsPicker;
        private HireCreatureBuildingWindow _hireCreatureBuildingWindow;
        private BuildingsListWindow _buildingsListWindow;
        private CommonData _commonData;
        private TradeController _tradeController;


        public void Init(TradeController tradeController,CastleModel castleModel,
            CastleCommandsSender castleCommandsSender,
            ProgramState programState,
            BuildingsPicker buildingsPicker,
            HireCreatureBuildingWindow hireCreatureBuildingWindow,
            BuildingsListWindow buildingsListWindow,
            CommonData commonData)
        {
            _tradeController = tradeController;
            _castleModel = castleModel;
            _castleCommandsSender = castleCommandsSender;
            _programState = programState;
            _buildingsPicker = buildingsPicker;
            _hireCreatureBuildingWindow = hireCreatureBuildingWindow;
            _buildingsListWindow = buildingsListWindow;
            _commonData = commonData;
        }


        public void SetBuildings(List<Building> buildings)
        {
            _castleModel.SetBuildings(buildings);
        }

        private void Update()
        {
            if (_programState.StatesOfProgram != StatesOfProgram.Castle || _tradeController.InTrade)
                return;
            if (CheckToUI())
                return;
            if (Input.GetMouseButtonDown(0))
                TrySelectBuilding();
        }

        private bool CheckToUI()
        {
            try
            {
                PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
                eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
                if (results.Count > 0)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public void OpenBuilding(Building building)
        {
            if (building.BuildingType == BuildingType.BuyBilding)
            {
                _buildingsListWindow.Open();
            }
            else
            {
                if (_commonData.TryGetDicBuildingDTOByID(building.Id, out DicBuildingDTO buildingDTO))
                {
                    if (_commonData.TryGetDicCreatureDTOByID((int)buildingDTO.creatureId, out DicCreatureDTO creature))
                    {
                        DicCreatureDTO dicCreatureDTO = creature.Clone();
                        List<DicCreatureDTO> dicCreatureDTOs = new List<DicCreatureDTO>();
                        if (dicCreatureDTO.upgradeToId == 0)
                        {
                            dicCreatureDTOs = _castleModel.CurrentDicCastleDTO.creatureSet
                              .Select(cId => _commonData.CreaturesDictianory[cId])
                              .Where(c => c.level == dicCreatureDTO.level).ToList();
                        }
                        else
                        {
                            dicCreatureDTOs.Add(dicCreatureDTO);
                        }

                        _hireCreatureBuildingWindow.Init(dicCreatureDTOs);
                        _hireCreatureBuildingWindow.Open();
                    }
                }
            }
        }

        private void TrySelectBuilding()
        {
            if(_buildingsPicker.TryPickBuilding(out Building building))
            {
                if(building.BuildingType == BuildingType.BuyBilding)
                {
                    _buildingsListWindow.Open();
                }
                else
                {
                    if (_commonData.TryGetDicBuildingDTOByID(building.Id, out DicBuildingDTO buildingDTO))
                    {
                        if (_commonData.TryGetDicCreatureDTOByID((int)buildingDTO.creatureId, out DicCreatureDTO creature))
                        {
                            DicCreatureDTO dicCreatureDTO = creature.Clone();
                            List<DicCreatureDTO> dicCreatureDTOs = new List<DicCreatureDTO>();
                            if (dicCreatureDTO.upgradeToId == 0)
                            {
                                dicCreatureDTOs = _castleModel.CurrentDicCastleDTO.creatureSet
                                  .Select(cId => _commonData.CreaturesDictianory[cId])
                                  .Where(c => c.level == dicCreatureDTO.level).ToList();
                            }
                            else
                            {
                                dicCreatureDTOs.Add(dicCreatureDTO);
                            }
  
                            _hireCreatureBuildingWindow.Init(dicCreatureDTOs);
                            _hireCreatureBuildingWindow.Open();
                        }
                    }
                }
            }
        }

    }
}