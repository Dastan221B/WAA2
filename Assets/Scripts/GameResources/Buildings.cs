using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buildings", menuName = "ScriptableObjects/Buildings")]
public class Buildings : ScriptableObject
{
    [SerializeField] private List<Sprite> _sprites;


    public Sprite GetSpriteByID(int id)
    {
        return _sprites[id];
    }

}