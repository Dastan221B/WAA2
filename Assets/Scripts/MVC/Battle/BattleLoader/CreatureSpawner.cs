using Assets.Scripts.GameResources;
using Assets.Scripts.GameResources.MapCreatures;
using Assets.Scripts.MVC.Battle.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MVC.Battle.BattleLoader
{
    public class CreatureSpawner : MonoBehaviour
    {
        private ModelCreatures _modelCreatures;
        private BattleModel _battleModel;
        private CommonData _commonData;
        private ResultPanel _resultPanel;

        public void Init(CommonData commonData , ModelCreatures modelCreatures, BattleModel battleModel, ResultPanel resultPanel)
        {
            _modelCreatures = modelCreatures;
            _battleModel = battleModel;
            _commonData = commonData;
            _resultPanel = resultPanel;
        }

        public List<CreatureModelObject> CreateBattleCreatures(Dictionary<int, CreatureStackObjectFullInfo> mapObjects)
        {
            List<CreatureModelObject> battleCreatures = new List<CreatureModelObject>();
            Quaternion quaternion = Quaternion.Euler(0, 0, 0);
            CreatureSide creatureSide;
            foreach (var creature in mapObjects)
            {
                if(_battleModel.TryGetHexagonByCoordinates(creature.Value.battleFieldCoordinates.x, creature.Value.battleFieldCoordinates.y, out Hexagon hexagon))
                {
                    CreatureModelObject creatureModelObject = _modelCreatures.GetMapCreatureByID((int)creature.Value.dicCreatureId - 1);
                    if (hexagon.transform.position.x < 0)
                    {
                        quaternion = Quaternion.Euler(creatureModelObject.transform.eulerAngles.x, creatureModelObject.transform.eulerAngles.y - 270, 0);
                        creatureSide = CreatureSide.Self;
                    }
                    else
                    {
                        quaternion = Quaternion.Euler(creatureModelObject.transform.eulerAngles.x, creatureModelObject.transform.eulerAngles.y - 90, 0);
                        creatureSide = CreatureSide.Enemy;
                    }
                    var creatureFullObject = Instantiate(creatureModelObject, hexagon.transform.position + new Vector3(0, 0.152f, 0), quaternion);
                    if(_commonData.TryGetDicCreatureDTOByID((int)creature.Value.dicCreatureId, out DicCreatureDTO dicCreatureDTO))
                    {
                        DicCreatureDTO dicCreatureDTOClone = dicCreatureDTO.Clone();
                        creatureFullObject.Init(creature.Value, dicCreatureDTOClone, (int)creature.Key, creatureSide, quaternion , (int)creature.Value.dicCreatureId);
                    }
                    hexagon.SetCreature(creatureFullObject);
                    battleCreatures.Add(creatureFullObject);
                }
            }
            return battleCreatures;
        }

    }
}