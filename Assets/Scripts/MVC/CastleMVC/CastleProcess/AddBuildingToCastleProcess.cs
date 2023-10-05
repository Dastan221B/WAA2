using Assets.Scripts.MVC.CastleMVC.Buildinngs;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.MVC.CastleMVC.CastleProcess
{
    public class AddBuildingToCastleProcess
    {
        private CastleModel _castleModel;
        private CommonData _commonData;

        public AddBuildingToCastleProcess(CommonData commonData,CastleModel castleModel)
        {
            _commonData = commonData;
            _castleModel = castleModel;
        }

        public void AddBuilding(MessageInput message)
        {
            AddCastleBuildingResult addCastleBuildingResult = Newtonsoft.Json.JsonConvert.DeserializeObject<AddCastleBuildingResult>(message.body);

            if(_castleModel.TryGetBuildingByID(addCastleBuildingResult.buildingId, out Building building))
            {
                if(_commonData.TryGetDicBuildingDTOByID(addCastleBuildingResult.buildingId, out DicBuildingDTO dicBuildingDTO))
                {
                    var sameCreatureLevelBuildings = _castleModel.CurrentDicCastleDTO.buildingSet
                           .Select(bId => _commonData.BuildingDictianory[bId])
                           .Where(b => b.level == dicBuildingDTO.level)
                           .Where(b => b.id != dicBuildingDTO.id)
                           .Select(b => b.id).ToList();

                    if (sameCreatureLevelBuildings.Count() == 1)
                    {
                        _castleModel.Buildings.Where(b => b.Id == sameCreatureLevelBuildings.Single()).Single().gameObject.SetActive(false);
                    }
                    _castleModel.CurrentCastleFullOjbectInfo.buildings.Add(addCastleBuildingResult.buildingId);
                    //_castleModel.CurrentDicCastleDTO.buildingSet = addCastleBuildingResult.buildings;
                    _castleModel.CurrentDicCastleDTO.buildingSet.Add(addCastleBuildingResult.buildingId);
                    _castleModel.CurrentCastleFullOjbectInfo.purchasableCreatureInfoMap = addCastleBuildingResult.purchasableCreatureInfoMap;
                    building.Open();
                    _castleModel.InitActiveCreatures();
                }
            }
        }
    }
}