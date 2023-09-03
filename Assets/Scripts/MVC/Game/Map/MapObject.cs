using System;
using UnityEngine;

[Serializable]
public class AbstractMapObject 
{
    public string id { get; set; }
    public int interactionPointXOffset { get; set; }
    public int interactionPointYOffset { get; set; }
    public int width { get; set; }
    public int height { get; set; }
    public string Id { get; private set; }

    public Coordinates coordinates{ get;set; }
}
