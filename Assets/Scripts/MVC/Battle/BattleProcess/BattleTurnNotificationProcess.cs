using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.MVC.Battle.BattleProcess
{
    public class BattleTurnNotificationProcess 
    {
        private BattleModel _battleModel;

        private Action _action;
        private MessageInput _messageInput;

        public BattleTurnNotificationProcess(BattleModel battleModel)
        {
            _battleModel = battleModel;
        }

        public void InitMap()
        {
            BattleTurnNotificationInfo battleTurnNotificationInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<BattleTurnNotificationInfo>(_messageInput.body);
            _battleModel.SetCreatureStackBattleObjectFullInfo(battleTurnNotificationInfo.activeCreatureStack);
            _battleModel.InitHexagonsForCreature();
        }

        public void InitBattleTurnNotificationProcess(MessageInput messageInput)
        {
            _messageInput = messageInput;
            int isCreatureInIDLE = _battleModel.CreatureModelObjectsFull.Where(item => item.IsIdle).ToList().Count;

            if(isCreatureInIDLE == _battleModel.CreatureModelObjectsFull.Count)
            {
                BattleTurnNotificationInfo battleTurnNotificationInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<BattleTurnNotificationInfo>(messageInput.body);
                _battleModel.SetCreatureStackBattleObjectFullInfo(battleTurnNotificationInfo.activeCreatureStack);
                _battleModel.InitHexagonsForCreature();
            }
            else
            {
                _battleModel.ActionBeforeMove += InitMap;
            }
        }
    }
}