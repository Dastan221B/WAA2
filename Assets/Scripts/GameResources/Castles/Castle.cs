using Assets.Scripts.GameResources;
using System.Collections;
using UnityEngine;

public class Castle : GameMapObject
{
    [SerializeField] private Sprite _castleIcon;
    [SerializeField] private MeshRenderer _cube;
    public int DicCastleID { get; private set; }
    public string ObjectID => MapObjectID;
    public Sprite CastleIcon => _castleIcon;
    public Vector2Int GatePosition { get; private set; }
    public Cell GateCell { get; private set; }
    [field: SerializeField] public CastleObjectFullInfo CastleInfo { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public Color ColorCastle { get; private set; }
    public string HeroModelObjectMapID { get; private set; }
    public int Oridinal { get; private set; }


    public void SetSettings(string heroModelObjectMapID, int ordinal)
    {
        HeroModelObjectMapID = heroModelObjectMapID;
        Oridinal = ordinal;
    }

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
    }
    
    public void SetupColorCube(Color color)
    {
        ColorCastle = color;
        _cube.material.color = ColorCastle;
    }
}