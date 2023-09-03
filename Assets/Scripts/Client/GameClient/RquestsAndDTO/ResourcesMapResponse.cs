using System;
using System.Collections.Generic;

[Serializable]
public class ResourcesMapResponse : SessionIdBasedResponse
{
    public Dictionary<int, Resource> resources { get; set; }
}
