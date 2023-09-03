using System;
using System.Collections.Generic;

[Serializable]
public class HeroObjectFullInfo
{
    public int level;
    public int attack;
    public int defence;
    public int power;
    public int knowledge;
    public int movePoints;
    public int dicHeroId;
    public string mapObjectId;
    public List<ArmySlotInfo> army { get; set; }

    public HeroObjectFullInfo Clone()
    {
        HeroObjectFullInfo newHeroObject = (HeroObjectFullInfo)MemberwiseClone();
        newHeroObject.army = army;
        newHeroObject.mapObjectId = mapObjectId;
        return newHeroObject;
    }
}
