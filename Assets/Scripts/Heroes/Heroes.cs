using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroesIcons", menuName = "ScriptableObjects/HeroesIcons")]
public class Heroes : ScriptableObject
{
    [SerializeField] private List<Hero> _heroes;
    public IEnumerable<Sprite> Icons => _heroes.Select(item => item.Icon);
    public IReadOnlyList<Hero> HeroesList => _heroes;

    public Hero GetHeroByID(int id)
    {
        if (id > _heroes.Count)
            return _heroes[0];

        Hero hero = _heroes.FirstOrDefault(item => item.HeroID == id);
        if(hero != null)
            return hero;
        return _heroes[0];
    }

    public Hero GetHeroByIDOrNull(int id)
    {
        Hero hero = _heroes.FirstOrDefault(item => item.HeroID == id);
        if (hero != null)
            return hero;
        return null;
    }

    public Hero GetHeroForCastleIconByID(int id)
    {
        //if (id > _heroes.Count)
        //    return _heroes[0];
        Debug.Log(id + " id22");
        Hero hero = _heroes.FirstOrDefault(item => item.HeroID == id);
        Debug.Log(hero.HeroID + " hero22");
        return hero;
    }
}