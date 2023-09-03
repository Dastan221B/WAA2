using System.Collections;
using UnityEngine;

public class TerrainMapProcess : MonoBehaviour
{

    public TerrainMapInfo ResolveTerrainMap(MessageInput messageInput)
    {
        TerrainMapInfo terrianMapInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<TerrainMapInfo>(messageInput.body);
        return terrianMapInfo;
    }
}