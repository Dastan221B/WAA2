using Assets.Scripts.GameResources.MapCreatures;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.Battle.BattleProcess
{
    public class BattleMoveProcess 
    {
        private BattleModel _battleModel;
        private CreaturePathMover _creaturePathMover;

        public BattleMoveProcess(BattleModel battleModel, CreaturePathMover creaturePathMover)
        {
            _battleModel = battleModel;
            _creaturePathMover = creaturePathMover;
        }

        public void InitBattleMove(MessageInput message)
        {
            BattleMoveResultInfo battleMoveResultInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<BattleMoveResultInfo>(message.body);
            if(battleMoveResultInfo.result == true && _battleModel.TryGetCreatureByID(battleMoveResultInfo.activeCreatureStackBattleObjectId, out CreatureModelObject battleCreature))
            {
                _creaturePathMover.StartMove(battleCreature, battleMoveResultInfo.path);
            }
            else
            {
                Debug.Log(battleMoveResultInfo.reason);
            }
        }
    }
}