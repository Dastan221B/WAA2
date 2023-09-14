using Assets.Scripts.GameResources;
using System.Collections;
using UnityEngine;

public class MineStructure : GameMapObject
{
    //[field: SerializeField] public int DicMineId { get; private set; }
    [SerializeField] private MinesCube _minesCube;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private SystemColors _color;
    public int DicMineID { get; private set; }
    public string ObjectID => MapObjectID;
    public Vector2Int GatePosition { get; private set; }
    public Cell GateCell { get; private set; }
    //public CastleObjectFullInfo CastleInfo { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    //public void SetCastleObjectFullInfo(CastleObjectFullInfo castleObjectFullInfo)
    //{
    //    CastleInfo = castleObjectFullInfo;
    //}

    private void Start()
    {
        _meshRenderer.material.color = _minesCube.GetColorById(DicMineID - 1);
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

    public void SetColor(Color color)
    {
        _meshRenderer.material.color = color;
    }

    public void SetDicMineID(int MineID)
    {
        //if (castleID < 0)
        //    castleID = 0;
        DicMineID = MineID;
    }
}