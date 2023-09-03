using Assets.Scripts.MVC.CastleSlots;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.CastleMVC.CastleProcess
{
    public class HireCreatureProcess
    {
        private SlotsModel _slotsModel;

        public HireCreatureProcess(SlotsModel slotsModel)
        {
            _slotsModel = slotsModel;
        }

        public void HireCreature(MessageInput message)
        {
            HireCastleCreatureResult hireCastleCreatureResult = Newtonsoft.Json.JsonConvert.DeserializeObject<HireCastleCreatureResult>(message.body);
            _slotsModel.AddCreaturesToCastleSlot(hireCastleCreatureResult.creatureMap);
        }
    }
}