using System.Collections;
using UnityEngine;

public class MapObjectListResponseProcess : MonoBehaviour
{
    private GameLoadedData _gameLoadedData;

    public MapObjectListResponseProcess(GameLoadedData gameLoadedData)
    {
        _gameLoadedData = gameLoadedData;
    }

    public void InitData(MessageInput message)
    {
        MapObjectListResponse gameObjectInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<MapObjectListResponse>(message.body);
        _gameLoadedData.AddToData(gameObjectInfo.castleObjects, typeof(CastlesData));
        _gameLoadedData.AddToData(gameObjectInfo.creatureObjects, typeof(CreatureData));
        _gameLoadedData.AddToData(gameObjectInfo.heroObjects, typeof(HeroesData));
        _gameLoadedData.AddToData(gameObjectInfo.mineObjects, typeof(MinesData));
        _gameLoadedData.AddToData(gameObjectInfo.resourceObjects, typeof(ResourceMapData));
    }
}