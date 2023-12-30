using Assets.Scripts.GameResources.MapCreatures;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.Battle.BattleProcess
{
    public class BattleMoveProcess 
    {
        private BattleModel _battleModel;
        private CreaturePathMover _creaturePathMover;
        private BattleTimer _battleTimer;

        public BattleMoveProcess(BattleModel battleModel, CreaturePathMover creaturePathMover, BattleTimer battleTimer)
        {
            _battleModel = battleModel;
            _creaturePathMover = creaturePathMover;
            _battleTimer = battleTimer;
        }

        public void InitBattleMove(MessageInput message)
        {
            BattleMoveResultInfo battleMoveResultInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<BattleMoveResultInfo>(message.body);
            _battleTimer.TimerTime = battleMoveResultInfo.turnSeconds;
            if(battleMoveResultInfo.result == true && _battleModel.TryGetCreatureByID(battleMoveResultInfo.activeCreatureStackBattleObjectId, out CreatureModelObject battleCreature))
            {
                if(battleCreature.CurrentHexagon != null)
                    battleCreature.CurrentHexagon.SetCreature(null);
                _creaturePathMover.StartMove(battleCreature, battleMoveResultInfo.path);
            }
            else
            {
            }
        }
    }
}