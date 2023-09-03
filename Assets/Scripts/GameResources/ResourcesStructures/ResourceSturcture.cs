using System.Collections;
using UnityEngine;

public class ResourceSturcture : GameMapObject
{
    [field: SerializeField] public int DicResourceId { get; private set; }

    public void SetDicResourceID(int dicResourceID)
    {
        if (dicResourceID < 0)
            dicResourceID = 0;
        DicResourceId = dicResourceID;
    }
}