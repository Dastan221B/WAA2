using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.Battle.BattleProcess
{
    public class BattleBlockActivetedProcess
    {
        private BattleModel _battleModel;

        public BattleBlockActivetedProcess(BattleModel battleModel)
        {
            _battleModel = battleModel;
        }

        public void BlockProcess(MessageInput message)
        {
            BattleCreatureBlockActivationResult battleCreatureBlockActivationResult = Newtonsoft.Json.JsonConvert.DeserializeObject<BattleCreatureBlockActivationResult>(message.body);

            BattleFieldCoordinates cordinates = battleCreatureBlockActivationResult.creatureStack.battleFieldCoordinates;
            if(_battleModel.TryGetHexagonByCoordinates(cordinates.x, cordinates.y, out Hexagon hexagon))
            {
                _battleModel.EnterCreatureInAction();
                hexagon.BattleCreature.Block();
            }
        }
    }
}