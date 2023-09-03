using System.Collections.Generic;
using System;

[Serializable]
public class MapObjectListResponse : SessionIdBasedResponse
{
    public Dictionary<string, CastleObject> castleObjects { get; set; }
    public Dictionary<string, CreatureObject> creatureObjects { get; set; }
    public Dictionary<string, ResourceObject> resourceObjects { get; set; }
    public Dictionary<string, HeroObject> heroObjects { get; set; }
    public Dictionary<string, MineObject> mineObjects { get; set; }
}
