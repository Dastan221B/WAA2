using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.Battle.BattleProcess
{
    public class BattleTurnNotificationProcess 
    {
        private BattleModel _battleModel;

        public BattleTurnNotificationProcess(BattleModel battleModel)
        {
            _battleModel = battleModel;
        }

        public void InitBattleTurnNotificationProcess(MessageInput messageInput)
        {
            BattleTurnNotificationInfo battleTurnNotificationInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<BattleTurnNotificationInfo>(messageInput.body);
            _battleModel.SetCreatureStackBattleObjectFullInfo(battleTurnNotificationInfo.activeCreatureStack);
            _battleModel.InitHexagonsForCreature();
            Debug.Log("Turn notify");
        }
    }
}