using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroModelObjects", menuName = "ScriptableObjects/HeroModelObjects")]
public class HeroModelObjects : ScriptableObject
{
    [SerializeField] private List<HeroModelObject> _heroes = new List<HeroModelObject>();

    public HeroModelObject GetHeroModelObjectByID(int id)
    {
        if (id > 0 && id < _heroes.Count)
            return _heroes[id];

        return _heroes[0];
    }

}