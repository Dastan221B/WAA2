using System;

[Serializable]
public class CreatureStackObjectFullInfo
{
    public int currentHealthPoint { get; set; }
    public bool isBlockOn { get; set; }
    public BattleFieldCoordinates battleFieldCoordinates { get; set; }
    public long dicCreatureId { get; set; }
    public int amount { get; set; }
    public int stackSlot { get; set; }
}
