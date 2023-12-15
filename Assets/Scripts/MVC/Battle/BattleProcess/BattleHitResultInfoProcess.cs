using Assets.Scripts.GameResources.MapCreatures;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.Battle.BattleProcess
{
    public class BattleHitResultInfoProcess
    {

        private BattleModel _battleModel;

        private CreatureModelObject _attakerBattleCreature;
        private CreatureModelObject _targetToAttackBattleCreature;
        private bool _isKilledCreature;
        private int _attakDamage;
        private int _amount;
        private BattleTimer _battleTimer;

        public BattleHitResultInfoProcess(BattleModel battleModel, BattleTimer battleTimer)
        {
            _battleModel = battleModel;
            _battleTimer = battleTimer;
        }

        public void InitHit(MessageInput message)
        {
            BattleHitResultInfo battleMoveResultInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<BattleHitResultInfo>(message.body);
            _battleTimer.TimerTime = battleMoveResultInfo.turnSeconds;
            if (_battleModel.TryGetCreatureByID(battleMoveResultInfo.activeCreatureStackBattleObjectId, out _attakerBattleCreature))
            {
                BattleFieldCoordinates coords = battleMoveResultInfo.targetCreatureStack.battleFieldCoordinates;
                if (_battleModel.TryGetHexagonByCoordinates(coords.x, coords.y, out Hexagon hexagon))
                {
                    _targetToAttackBattleCreature = hexagon.BattleCreature;
                    _amount = battleMoveResultInfo.targetCreatureStack.amount;
                    _isKilledCreature = battleMoveResultInfo.hitLog.creatureKilled == 1;
                    _attakDamage = battleMoveResultInfo.targetCreatureStack.currentHealthPoint;
                    _attakerBattleCreature.SetHealthPoints(battleMoveResultInfo.attackerCreatureStack.currentHealthPoint);
                    if(_targetToAttackBattleCreature != null && battleMoveResultInfo.targetCreatureStack != null)
                        _targetToAttackBattleCreature.SetHealthPoints(battleMoveResultInfo.targetCreatureStack.currentHealthPoint);
                    HitCreature();
                }
            }
        }

        private void HitCreature()
        {
            _attakerBattleCreature.Attack(_targetToAttackBattleCreature, _isKilledCreature, _attakDamage, _amount);
        }
    }
}