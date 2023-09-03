using System;

[Serializable]
public class TerrainMapInfo : SessionIdBasedResponse
{
    public TerrainCell[][] terrainMap { get; set; }
}
