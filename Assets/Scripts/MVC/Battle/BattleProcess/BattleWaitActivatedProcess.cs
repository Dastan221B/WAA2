using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.Battle.BattleProcess
{
    public class BattleWaitActivatedProcess
    {

        private BattleModel _battleModel;

        public BattleWaitActivatedProcess(BattleModel battleModel)
        {
            _battleModel = battleModel;
        }

        public void WaitProcess(MessageInput message)
        {
            BattleWaitActivatedResult battleCreatureBlockActivationResult = Newtonsoft.Json.JsonConvert.DeserializeObject<BattleWaitActivatedResult>(message.body);

            BattleFieldCoordinates cordinates = battleCreatureBlockActivationResult.creatureStack.battleFieldCoordinates;
            if (_battleModel.TryGetHexagonByCoordinates(cordinates.x, cordinates.y, out Hexagon hexagon))
            {
                _battleModel.EnterCreatureInAction();
                //hexagon.BattleCreature.Block();
            }
        }
    }
}