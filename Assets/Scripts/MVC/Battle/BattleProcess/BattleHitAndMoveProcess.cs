using Assets.Scripts.GameResources.MapCreatures;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.Battle.BattleProcess
{
    public class BattleHitAndMoveProcess
    {
        private CreaturePathMover _creaturePathMover;
        private BattleModel _battleModel;


        private CreatureModelObject _attakerBattleCreature;
        private CreatureModelObject _targetToAttackBattleCreature;
        private bool _isKilledCreature;
        private int _attakDamage;
        private int _amount;

        public BattleHitAndMoveProcess(CreaturePathMover creaturePathMover , BattleModel battleModel)
        {
            _creaturePathMover = creaturePathMover;
            _battleModel = battleModel;
        }

        public void StartBattleHitAndMoveProcess(MessageInput message)
        {
            BattleMoveAndHitResultInfo battleMoveAndHitResultInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<BattleMoveAndHitResultInfo>(message.body);
            ResizeArray(ref battleMoveAndHitResultInfo);
            if(_battleModel.TryGetCreatureByID(battleMoveAndHitResultInfo.activeCreatureStackBattleObjectId, out _attakerBattleCreature))
            {
                BattleFieldCoordinates coords = battleMoveAndHitResultInfo.targetCreatureStack.battleFieldCoordinates;
                Debug.Log("Coords " + coords.x + " " + coords.y);
                if (_battleModel.TryGetHexagonByCoordinates(coords.x, coords.y, out Hexagon hexagon))
                {
                    _targetToAttackBattleCreature = hexagon.BattleCreature;
                    _isKilledCreature = battleMoveAndHitResultInfo.hitLog.creatureKilled == 1;
                    _amount = battleMoveAndHitResultInfo.targetCreatureStack.amount;
                    Debug.Log("_isKilledCreature " + _isKilledCreature);
                    _attakDamage = battleMoveAndHitResultInfo.targetCreatureStack.currentHealthPoint;
                    _attakerBattleCreature.SetHealthPoints(battleMoveAndHitResultInfo.attackerCreatureStack.currentHealthPoint);
                    _creaturePathMover.OnEndedMove += HitCreature;
                    _creaturePathMover.StartMove(_attakerBattleCreature, battleMoveAndHitResultInfo.path);
                }
            }
        }

        private void HitCreature()
        {
            _creaturePathMover.OnEndedMove -= HitCreature;
            _attakerBattleCreature.Attack(_targetToAttackBattleCreature, _isKilledCreature, _attakDamage , _amount);
        }

        private void ResizeArray(ref BattleMoveAndHitResultInfo battleMoveAndHitResultInfo)
        {
            for (int i = 0; i < battleMoveAndHitResultInfo.path.Count; i++)
            {
                if (battleMoveAndHitResultInfo.path[i] == battleMoveAndHitResultInfo.targetCreatureStack.battleFieldCoordinates)
                {
                    battleMoveAndHitResultInfo.path.Remove(battleMoveAndHitResultInfo.path[i]);
                    break;
                }
            }
        }
    }
}