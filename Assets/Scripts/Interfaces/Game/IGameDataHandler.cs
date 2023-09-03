using Assets.Scripts.GameResources.MapCreatures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameDataHandler 
{
    void SetCells(Cell[,] cells , Cell[,] cellsInInversionSpace);
    void SetCastless(List<Castle> castles);
    void SetResourcesStructures(List<ResourceSturcture> resourcesSturctures);
    void SetHeroModelObjects(List<HeroModelObject> heroModelObjects);
    void SetMapCreatures(List<CreatureModelObject> mapCreatures);
    void SetMinesStructures(List<MineStructure> mineStructures);
}