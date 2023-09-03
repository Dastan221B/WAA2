using System.Collections;
using UnityEngine;

public interface IHeroPickable
{
    void PickHero(out HeroModelObject heroModelObject);
}