using System.Collections;
using UnityEngine;

[System.Serializable]
public class CastleObject : AbstractMapObject
{
    public int DicCastleId { get; set; }
    public CastleOwner castleOwner { get; set; }

}