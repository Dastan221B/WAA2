using Assets.Scripts.GameResources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastlesLoader 
{
    private Castles _castles;
    private GameModel _gameModel;
    private SystemColors _systemColors;

    public CastlesLoader(Castles castles, GameModel gameModel, SystemColors systemColors)
    {
        _systemColors = systemColors;
        _castles = castles;
        _gameModel = gameModel;
    }

    public List<Castle> CreateCastles(Dictionary<string, CastleObject> castles)
    {
        List<Castle> castlesObjects = new List<Castle>();
        int CASTLE_COORDINATES_OFFSET = 0;

        foreach (var castle in castles)
        {
            int castleGatePositionX = castle.Value.coordinates.x + CASTLE_COORDINATES_OFFSET;
            int castleGatePositionY = castle.Value.coordinates.y - CASTLE_COORDINATES_OFFSET;
            var castlePrefab = _castles.GetCastleByID(castle.Value.DicCastleId);
            Castle castleObject = MonoBehaviour.Instantiate(castlePrefab,
                new Vector3(castleGatePositionX + (castle.Value.width/2), 0.55f, castleGatePositionY - (castle.Value.height / 2) + 0.5f), castlePrefab.transform.rotation, _gameModel.TerrainObjectsParent);

            castleObject.SetGatePosition(new Vector2Int(castleGatePositionX, castleGatePositionY));
            castleObject.SetSize(castle.Value.width, castle.Value.height);
            castleObject.SetDicCastleID(castle.Value.DicCastleId);
            castleObject.SetMapObjectID(castle.Key);
            castleObject.SetupColorCube(_systemColors.GetColorByOrdinal(castle.Value.castleOwner.ordinal));
            castleObject.SetSettings(castle.Value.castleOwner.id, castle.Value.castleOwner.ordinal);
            //Debug.Log("castle.Value.castleOwner.id " + castle.Value.castleOwner.id);
            //if(_gameModel.TryGetHeroModelObject(castle.Value.castleOwner.id , out HeroModelObject heroModelObject))
            //{
            //    Debug.Log("castle.Value.castleOwner.ordinal " + castle.Value.castleOwner.ordinal);
            //    heroModelObject.SetOrdinal(castle.Value.castleOwner.ordinal);
            //}
            castlesObjects.Add(castleObject);
            
        }
        return castlesObjects;
    }

}