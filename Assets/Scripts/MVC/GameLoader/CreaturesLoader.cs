using Assets.Scripts.GameResources.MapCreatures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaturesLoader
{
    private ModelCreatures _mapCreatures;
    private GameModel _gameModel;

    public CreaturesLoader(ModelCreatures mapCreatures, GameModel gameModel)
    {
        _mapCreatures = mapCreatures;
        _gameModel = gameModel;
    }

    public List<CreatureModelObject> CreateCreatures(Dictionary<string, CreatureObject> creatures)
    {
        List<CreatureModelObject> mapCreatures = new List<CreatureModelObject>();

        foreach (var creature in creatures)
        {
            if (creature.Value.DicCreatureId - 1 == 11)
            {
                CreatureModelObject mapCreature = MonoBehaviour.Instantiate(_mapCreatures.GetMapCreatureByID(creature.Value.DicCreatureId - 1),
                new Vector3(creature.Value.coordinates.x, 0.55f, creature.Value.coordinates.y), Quaternion.Euler(0, 0, 0), _gameModel.TerrainObjectsParent);
                mapCreature.transform.localScale = new Vector3(0.36f, 0.36f, 0.36f);
                mapCreature.SetDicCreatureID(creature.Value.DicCreatureId, creature.Value.Amount);
                mapCreature.SetMapObjectID(creature.Key);
                mapCreatures.Add(mapCreature);
                Debug.Log("transform" + mapCreature.transform.localScale.x + " " + mapCreature.transform.localScale.y + " " + mapCreature.transform.localScale.z);
            }
            else
            {
                CreatureModelObject mapCreature = MonoBehaviour.Instantiate(_mapCreatures.GetMapCreatureByID(creature.Value.DicCreatureId - 1),
                new Vector3(creature.Value.coordinates.x, 0.5f, creature.Value.coordinates.y), Quaternion.Euler(-90, 0, 0), _gameModel.TerrainObjectsParent);
                mapCreature.transform.localScale = new Vector3(0.09f, 0.09f, 0.09f);
                mapCreature.SetDicCreatureID(creature.Value.DicCreatureId, creature.Value.Amount);
                mapCreature.SetMapObjectID(creature.Key);
                mapCreatures.Add(mapCreature);
                Debug.Log("transform" + mapCreature.transform.localScale.x + " " + mapCreature.transform.localScale.y + " " + mapCreature.transform.localScale.z);

            }
        }

        return mapCreatures;
    }
}