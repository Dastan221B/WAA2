using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinesLoader
{
    private MinesStructure _minesStructure;
    private GameModel _gameModel;

    public MinesLoader(MinesStructure minesStructure, GameModel gameModel)
    {
        _minesStructure = minesStructure;
        _gameModel = gameModel;
    }

    public List<MineStructure> CreateMines(Dictionary<string, MineObject> mines)
    {
        List<MineStructure> mineObjects = new List<MineStructure>();
        int MINE_COORDINATES_OFFSET = 0;

        foreach (var mine in mines)
        {
            int mineGatePositionX = mine.Value.coordinates.x + MINE_COORDINATES_OFFSET;
            int mineGatePositionY = mine.Value.coordinates.y - MINE_COORDINATES_OFFSET;
            var minePrefab = _minesStructure.GetMineSturctureByID(mine.Value.DicResourceId);
            MineStructure mineObject = MonoBehaviour.Instantiate(minePrefab,
                new Vector3(mineGatePositionX + (mine.Value.width / 2), 0.55f, mineGatePositionY - (mine.Value.height / 2) + 0.5f), minePrefab.transform.rotation, _gameModel.TerrainObjectsParent);
            if(mine.Value.DicResourceId == 3)
            {
                mineObject.transform.position = new Vector3(mineObject.transform.position.x, 1.55f, mineObject.transform.position.z);
            }
            mineObject.SetGatePosition(new Vector2Int(mineGatePositionX, mineGatePositionY));
            mineObject.SetSize(mine.Value.width, mine.Value.height);
            mineObject.SetDicMineID(mine.Value.DicResourceId);
            mineObject.SetMapObjectID(mine.Key);
            mineObjects.Add(mineObject);
        }
        return mineObjects;
    }
}
