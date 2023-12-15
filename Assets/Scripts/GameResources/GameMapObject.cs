using System.Collections;
using UnityEngine;

public class GameMapObject : MonoBehaviour
{
    [field: SerializeField] public string MapObjectID { get; private set; }
    public Cell CellPlace { get; private set; }
    
    public void SetCellPlace(Cell cell)
    {
        CellPlace = cell;
    }

    public void SetMapObjectID(string mapObjectID)
    {
        MapObjectID = mapObjectID;
    }

}