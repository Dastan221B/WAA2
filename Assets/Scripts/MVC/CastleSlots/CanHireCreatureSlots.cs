using Assets.Scripts.MVC.Battle;
using Assets.Scripts.MVC.CastleMVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanHireCreatureSlots : MonoBehaviour
{
    [SerializeField] private List<HireCreatureSlot> _hireCreatureSlots;
    [SerializeField] private ModelCreatures _modelCreatures;

    
    public void SetSpriteCreature(List<DicCreaturePack> dicCreaturePacks, CastleController castleController)
    {
        foreach(var item in _hireCreatureSlots)
        {
            item.ResetIcons();
        }

        int i = 0;
        foreach(var creature in dicCreaturePacks)
        {
            _hireCreatureSlots[i].Init(_modelCreatures.GetIconById((int)creature.DicCreatureDTO.id - 1), creature.Building, castleController);
            i++;
        }
    }
}

