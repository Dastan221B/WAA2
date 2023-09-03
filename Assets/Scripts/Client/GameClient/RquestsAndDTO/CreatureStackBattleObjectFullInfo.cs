using System;

[Serializable]
public class CreatureStackBattleObjectFullInfo : ArmySlotInfo
{
    public string heroId { get; set; }
    public int battleFieldObjectId { get; set; }
    public int currentHealthPoint { get; set; }
    public bool isBlockOn { get; set; }
    public BattleFieldCoordinates battleFieldCoordinates { get; set; }
}
