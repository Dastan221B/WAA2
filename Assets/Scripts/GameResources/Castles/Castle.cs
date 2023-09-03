using Assets.Scripts.GameResources;
using System.Collections;
using UnityEngine;

public class Castle : GameMapObject
{
    [SerializeField] private Sprite _castleIcon;
    [SerializeField] private GameObject _cube;
    [SerializeField] private SystemColors _color;
    public int DicCastleID { get; private set; }
    public string ObjectID => MapObjectID;
    public Sprite CastleIcon => _castleIcon;
    public Vector2Int GatePosition { get; private set; }
    public Cell GateCell { get; private set; }
    [field: SerializeField] public CastleObjectFullInfo CastleInfo { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    public void SetCastleObjectFullInfo(CastleObjectFullInfo castleObjectFullInfo)
    {
        CastleInfo = castleObjectFullInfo;
    }

    public void SetSize(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public void SetGatePosition(Vector2Int vector2Int)
    {
        GatePosition = vector2Int;
    }

    public void SetGateCell(Cell cell)
    {
        GateCell = cell;
    }

    public void SetCastleID()
    {

    }

    public void SetDicCastleID(int castleID)
    {
        //if (castleID < 0)
        //    castleID = 0;
        DicCastleID = castleID;
    }public void SetColorCube(int ordinal)
    {
        _cube.GetComponent<MeshRenderer>().material.color = _color.GetColorByOrdinal(ordinal);
    }
}