using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MinesStructure", menuName = "ScriptableObjects/MinesStructure")]
public class MinesStructure : ScriptableObject
{
    [SerializeField] private MineStructure[] _mineStructures;

    public MineStructure GetMineSturctureByID(int id)
    {
        if (id > 0 && id < _mineStructures.Length)
            return _mineStructures[id - 1];
        return _mineStructures[0];
    }
}
