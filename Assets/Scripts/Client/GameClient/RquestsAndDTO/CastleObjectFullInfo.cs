using Assets.Scripts.MVC.CastleMVC.Buildinngs;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CastleObjectFullInfo
{
    public int dicCastleId { get; set; }
    public string mapObjectId { get; set; }
    public List<int> buildings { get; set; }
    public Dictionary<int, PurchaseableCreatureInfo> purchasableCreatureInfoMap { get; set; } = new Dictionary<int, PurchaseableCreatureInfo>();

    public List<ArmySlotInfo> creaturesInCastle { get; set; }
    [field: SerializeField] public HeroObjectFullInfo heroInCastle { get; set; }
    [field: SerializeField] public HeroObjectFullInfo heroInGarrison { get; set; }
    public Building BuildingCouncil { get; private set; }
    
    public List<ArmySlotInfo> CreaturesInCastle
    {
        get
        {
            List<ArmySlotInfo> creaturesInCastle = new List<ArmySlotInfo>();
            if (heroInCastle != null)
            {
                creaturesInCastle = heroInCastle.army;
            } 
            //else if(heroInGarrison != null)
            //{
            //    creaturesInCastle = heroInGarrison.army;
            //}
            else
            {
                creaturesInCastle = this.creaturesInCastle;
            }
            return creaturesInCastle;
        }
    }

    public void SetBuildingCouncil(Building building)
    {
        UnityEngine.Debug.Log(building);
        BuildingCouncil = building;
    }

    public void SetInfo(CastleObjectFullInfo info)
    {
        dicCastleId = info.dicCastleId;
        mapObjectId = info.mapObjectId;
        buildings = info.buildings ?? buildings;
        purchasableCreatureInfoMap = info.purchasableCreatureInfoMap ?? new Dictionary<int, PurchaseableCreatureInfo>();
        creaturesInCastle = info.creaturesInCastle ?? creaturesInCastle;
        heroInCastle = info.heroInCastle;
    }
}
