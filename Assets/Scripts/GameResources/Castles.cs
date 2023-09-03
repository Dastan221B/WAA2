using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Castles", menuName = "ScriptableObjects/Castles")]
public class Castles : ScriptableObject
{
    [SerializeField] private List<Castle> _castles = new List<Castle>();

    public Castle GetCastleByID(int id)
    {
        if (id > 0 && id < _castles.Count)
            return _castles[id];

        return _castles[0];
    }
}