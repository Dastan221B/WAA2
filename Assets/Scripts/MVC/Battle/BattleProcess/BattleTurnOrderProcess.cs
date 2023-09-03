using Assets.Scripts.GameResources.MapCreatures;
using Assets.Scripts.MVC.Battle.Views;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.MVC.Battle.BattleProcess
{
    public class BattleTurnOrderProcess : MonoBehaviour
    {
        private BattleModel _battleModel;
        private BattleView _battleView;

        public void Init(BattleView battleView,BattleModel battleModel)
        {
            _battleView = battleView;
            _battleModel = battleModel;
        }

        public void InitTurn(MessageInput message)
        {
            BattleTurnOrderInfo battleTurnOrderInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<BattleTurnOrderInfo>(message.body);
            
            if(!_battleModel.IsInited)
            {
                StartCoroutine(DisplayIconsWhileNotInited(battleTurnOrderInfo));
                return;
            }    
            
            List<CreatureModelObject> creatureModelObjects = new List<CreatureModelObject>();

            int count = 0;
            foreach(var item in battleTurnOrderInfo.turnOrder)
            {
                //if (count >= _battleModel.CreatureModelObjects.Count)
                //    break;
                if(_battleModel.TryGetCreatureByID(item.battleFieldObjectId, out CreatureModelObject creature))
                {
                    creatureModelObjects.Add(creature);
                }
                count++;
            }

            _battleView.InitBattle(creatureModelObjects);
        }

        private IEnumerator DisplayIconsWhileNotInited(BattleTurnOrderInfo battleTurnOrderInfo)
        {
            while (!_battleModel.IsInited)
            {
                yield return null;
            }
            List<CreatureModelObject> creatureModelObjects = new List<CreatureModelObject>();
            int count = 0;
            foreach (var item in battleTurnOrderInfo.turnOrder)
            {
                //if (count >= _battleModel.CreatureModelObjects.Count)
                //    break;
                if (_battleModel.TryGetCreatureByID(item.battleFieldObjectId, out CreatureModelObject creature))
                {
                    creatureModelObjects.Add(creature);
                }
                count++;
            }

            _battleView.InitBattle(creatureModelObjects);
        }

    }
}