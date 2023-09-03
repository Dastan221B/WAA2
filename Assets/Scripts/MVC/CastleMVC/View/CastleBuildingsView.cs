using Assets.Scripts.MVC.CastleMVC.Buildinngs;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MVC.CastleMVC.View
{
    public class CastleBuildingsView : MonoBehaviour
    {
        private CastleModel _castleModel;

        public void Init(CastleModel castleModel)
        {
            _castleModel = castleModel;
        }

        public void DisplayBuildings(CastleObjectFullInfo castleObjectFullInfo)
        {
            for(int i = 0; i < castleObjectFullInfo.buildings.Count; i++)
            {
                if(_castleModel.TryGetBuildingByID(castleObjectFullInfo.buildings[i], out Building building))
                {
                    building.Open();
                }
            }   
        }

    }
}