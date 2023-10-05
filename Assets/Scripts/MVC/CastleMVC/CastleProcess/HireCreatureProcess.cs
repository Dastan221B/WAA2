using Assets.Scripts.MVC.CastleMVC.View;
using Assets.Scripts.MVC.CastleSlots;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.CastleMVC.CastleProcess
{
    public class HireCreatureProcess
    {
        private SlotsModel _slotsModel;
        private CastleModel _castleModel;
        private CommonData _commonData;
        private HireCreatureBuildingWindow _hireCreatureBuildingWindow;

        public HireCreatureProcess(HireCreatureBuildingWindow hireCreatureBuildingWindow, SlotsModel slotsModel, CastleModel castleModel, CommonData commonData)
        {
            _slotsModel = slotsModel;
            _castleModel = castleModel;
            _commonData = commonData;
            _hireCreatureBuildingWindow = hireCreatureBuildingWindow;
        }

        public void HireCreature(MessageInput message)
        {
            HireCastleCreatureResult hireCastleCreatureResult = Newtonsoft.Json.JsonConvert.DeserializeObject<HireCastleCreatureResult>(message.body);
            _castleModel.DecreaseCreatureAvailable(_castleModel.LevelLastHiredCreature, _castleModel.AmountHiredCreature);
            _slotsModel.AddCreaturesToCastleSlot(hireCastleCreatureResult.creatureMap);
            _hireCreatureBuildingWindow.SetupCreaturesMaxCount();
            _hireCreatureBuildingWindow.UpdateUI();
        }
    }
}