using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroModelObjectLoader : MonoBehaviour
{
    private HeroModelObjects _heroModelObjects;
    private Heroes _heroes;
    private GameModel _gameModel;

    public HeroModelObjectLoader(HeroModelObjects heroModelObjects, Heroes heroes, GameModel gameModel)
    {
        _heroModelObjects = heroModelObjects;
        _heroes = heroes;
        _gameModel = gameModel;
    }

    public List<HeroModelObject> CreateHeroModelObjects(Dictionary<string,HeroObject> heroes)
    {
        List<HeroModelObject> heroModelObjects = new List<HeroModelObject>();

        foreach (var hero in heroes)
        {
            HeroModelObject heroModelObject = MonoBehaviour.Instantiate(_heroModelObjects.GetHeroModelObjectByID((int)hero.Value.DicHeroId),
                new Vector3(hero.Value.coordinates.x, 0.8f, hero.Value.coordinates.y), Quaternion.identity, _gameModel.TerrainObjectsParent);
            Debug.Log("PositionHero " + hero.Value.coordinates.x + " " + hero.Value.coordinates.y);
            heroModelObject.Init(hero.Value, _heroes.GetHeroByID((int)hero.Value.DicHeroId));
            heroModelObject.SetMapObjectID(hero.Key);
            heroModelObjects.Add(heroModelObject);
            heroModelObject.Idle();
        }
        return heroModelObjects;
    }

}