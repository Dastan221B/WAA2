using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "TerrainCells", menuName = "ScriptableObjects/TerrainCells")]
public class TerrainCells : ScriptableObject
{
    [SerializeField] private RiverCell _riverCell;
    [SerializeField] private GrassCell _grassCell;
    [SerializeField] private DirtyCell _dirtyCell;
    [SerializeField] private Cell _greenTreeCell;
    [SerializeField] private Cell _snowCell;
    [SerializeField] private Cell _mountains;

    public RiverCell RiverCell => _riverCell;
    public GrassCell GrassCell => _grassCell;
    public DirtyCell DirtyCell => _dirtyCell;

    public Cell GetCellByType(TerrainTypes terrainType)
    {
        switch (terrainType)
        {
            case TerrainTypes.DIRT_ROAD:
                return _dirtyCell;
            case TerrainTypes.GRASS:
                return _grassCell;
            case TerrainTypes.RIVER:
                return _riverCell;
            case TerrainTypes.SNOW:
                return _snowCell;
            case TerrainTypes.GREEN_TREE:
                return _greenTreeCell;
            case TerrainTypes.GREY_MOUNTAIN:
                return _mountains;
        }
        return _grassCell;
    }

}