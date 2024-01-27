using Assets.Scripts.MVC.CastleMVC.Buildinngs;
using Assets.Scripts.MVC.CastleMVC.View;
using Assets.Scripts.MVC.CastleSlots;
using Assets.Scripts.MVC.Game;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.MVC.CastleMVC
{
    public class DicCreaturePack
    {
        public Building Building { get; set; }
        public DicCreatureDTO DicCreatureDTO { get; set; }
    }

    public class CastleModel
    {
        private List<Building> _buildings;
        private GameModel _gameModel;
        private CastleView _castleView;
        private CommonData _commonData;
        private SlotsController _slotsController;
        private Heroes _heroes;

        public CastleObjectFullInfo CurrentCastleFullOjbectInfo { get; private set; }
        public string CurrentCastleID { get; private set; }
        public List<string> CanBuyBuildingCastles { get; private set; } = new List<string>();
        public string GarrisonID { get; private set; }
        public HeroObjectFullInfo HeroInGarrison { get; private set; }
        public HeroObjectFullInfo HeroInCastle { get; private set; }
        public DicCastleDTO CurrentDicCastleDTO { get; private set; }
        public IReadOnlyList<Building> Buildings => _buildings;
        private List<DicCreaturePack> _dicCreaturePackcs = new List<DicCreaturePack>();
        public int LevelLastHiredCreature { get; private set; }
        public int AmountHiredCreature { get; private set; }
        public bool TurnUpdated = true;
        


        public CastleModel(CastleView castleView,CommonData commonData, GameModel gameModel , SlotsController slotsController, Heroes heroes)
        {
            _castleView = castleView;
            _gameModel = gameModel;
            _commonData = commonData;
            _slotsController = slotsController;
            _heroes = heroes;
            _slotsController = slotsController;
        }

        public void SetBuyCreature(int amount , int level)
        {
            LevelLastHiredCreature = level;
            AmountHiredCreature = amount;
        }

        public void InitActiveCreatures()
        {
            _dicCreaturePackcs.Clear();
            foreach (var building in _buildings)
            {
                if (building.gameObject.activeSelf)
                {
                    if (building.BuildingType == BuildingType.Hire)
                    {
                        if (_commonData.TryGetDicBuildingDTOByID(building.Id, out DicBuildingDTO buildingDTO))
                        {
                            if (_commonData.TryGetDicCreatureDTOByID((int)buildingDTO.creatureId, out DicCreatureDTO creature))
                            {
                                DicCreatureDTO dicCreatureDTO = creature.Clone();
                                List<DicCreatureDTO> dicCreatureDTOs = new List<DicCreatureDTO>();
                                if (dicCreatureDTO.upgradeToId == 0)
                                {
                                    dicCreatureDTOs = CurrentDicCastleDTO.creatureSet
                                      .Select(cId => _commonData.CreaturesDictianory[cId])
                                      .Where(c => c.level == dicCreatureDTO.level).ToList();
                                }
                                else
                                {
                                    dicCreatureDTOs.Add(dicCreatureDTO);
                                }
                                foreach(var item in dicCreatureDTOs)
                                {
                                    DicCreaturePack dicCreaturePack = new DicCreaturePack();
                                    dicCreaturePack.DicCreatureDTO = item;
                                    dicCreaturePack.Building = building;
                                    _dicCreaturePackcs.Add(dicCreaturePack);
                                }
                            }
                        }
                    }
                }
            }
            _castleView.SetAvilableCreatures(_dicCreaturePackcs);
        }

        public void SetHeroInGarrison(HeroObjectFullInfo hero)
        {
            HeroInGarrison = hero;
        }

        public void SetHeroInCastle(HeroObjectFullInfo hero)
        {
            HeroInCastle = hero;
        }

        public void SetCasttleSettings(DicCastleDTO dicCastleDTO, HeroObjectFullInfo heroObjectFullInfo , HeroObjectFullInfo heroIncastle,  string garisonID,string castleID, CastleObjectFullInfo castleObjectFullInfo)
        {
            HeroInGarrison = heroObjectFullInfo;
            HeroInCastle = heroIncastle;
            CurrentDicCastleDTO = dicCastleDTO;
            GarrisonID = garisonID;
            CurrentCastleID = castleID;
            CurrentCastleFullOjbectInfo = castleObjectFullInfo;
            if (HeroInGarrison != null)
            {
                if (castleObjectFullInfo.heroInGarrison != null)
                {
                    if (_gameModel.TryGetHeroModelObject(castleObjectFullInfo.heroInGarrison.mapObjectId, out HeroModelObject heroModelObject))
                        _slotsController.DisplayGarnison(_heroes.GetHeroForCastleIconByID(HeroInGarrison.dicHeroId).Icon, heroModelObject);
                }
                else
                {
                    _slotsController.DisplayGarnison(_heroes.GetHeroForCastleIconByID(HeroInGarrison.dicHeroId).Icon, null);
                }
            }
            else
            if(HeroInCastle != null)
            {
                if (castleObjectFullInfo.heroInCastle != null)
                {
                    if (_gameModel.TryGetHeroModelObject(castleObjectFullInfo.heroInCastle.mapObjectId, out HeroModelObject heroModelObject))
                        _slotsController.DisplayCastle(_heroes.GetHeroForCastleIconByID(HeroInCastle.dicHeroId).Icon, heroModelObject);
                }
                else
                {
                    _slotsController.DisplayGarnison(_heroes.GetHeroForCastleIconByID(HeroInCastle.dicHeroId).Icon, null);
                }
            }
        }

        public void SetBuildings(List<Building> buildings)
        {
            _buildings = buildings;
            foreach (var item in _buildings)
                item.gameObject.SetActive(true);
            InitBuildingsIDS();
        }

        public void DisplayBuildgins(CastleObjectFullInfo fullInfo, DicCastleDTO castleInfo)
        {
            foreach (var item in _buildings)
                item.gameObject.SetActive(true);
            var activeBuildingsInCastle = _buildings.Select(b => b.Id).Intersect(fullInfo.buildings);
            
            activeBuildingsInCastle
                .ToList()
                .ForEach(buildingId =>
                {
                    _buildings[buildingId - 1].gameObject.SetActive(true);
                    
                });

            _buildings
                 .Select(b => b.Id)
                .Except(activeBuildingsInCastle)
                .Where(id => _buildings.Single(b => b.Id == id).gameObject.activeSelf)
                .ToList()
                .ForEach(buildingId =>
                {
                    _buildings.Single(b => b.Id == buildingId).gameObject.SetActive(false);
                });


            foreach (var buildingOnScene in _buildings.Where(b => castleInfo.buildingSet.Contains(b.Id)))
            {
                DicBuildingDTO buildingDTO = _commonData.BuildingDictianory[buildingOnScene.Id];


                if (buildingDTO.creatureId == 0)
                    continue;

                var sameCreatureLevelBuildings = fullInfo.buildings
                    .Select(bId => _commonData.BuildingDictianory[bId])
                    .Where(b => b.level == buildingDTO.level)
                    .Where(b => b.id != buildingDTO.id)
                    .Select(b => b.id);


                if (sameCreatureLevelBuildings.Count() == 1)
                {
                    if (fullInfo.buildings.Contains((int)buildingDTO.id))
                    {
                        _buildings
                            .Where(b => b.Id == Mathf.Min(sameCreatureLevelBuildings.Single(), buildingDTO.id))
                            .Single()
                            .gameObject.SetActive(false);
                    }
                }
            }
            InitActiveCreatures();
            //if (castleInfo.buildingSet[castleInfo.buildingSet.Count - 1] == 30)
            //{
            //    _buildings[31].gameObject.SetActive(true);
            //}
            //else
            //{
            //    _buildings[14].gameObject.SetActive(true);
            //}
        }

        public void DecreaseCreatureAvailable(int creatureLevel , int count)
        {
            if(CurrentCastleFullOjbectInfo.purchasableCreatureInfoMap.TryGetValue(creatureLevel, out PurchaseableCreatureInfo creaturesAmount))
            {
                creaturesAmount.amount -= count;
            }
        }
        
        public void InitBuildingsIDS()
        {
            int curBuidlingIndex = 0;
            foreach (DicCastleDTO castleDTO in _commonData.CastleDictianory.Values)
            {
                List<Building> castleBuildings = _buildings.GetRange(curBuidlingIndex, castleDTO.buildingSet.Count);
                var buildingSetEnumerator = castleDTO.buildingSet.GetEnumerator();

                for (int i = 0; i < castleBuildings.Count; i++)
                {
                    if (buildingSetEnumerator.MoveNext())
                    {
                        castleBuildings[i].SetID(buildingSetEnumerator.Current);
                    }
                }
                curBuidlingIndex += castleDTO.buildingSet.Count;
            }
        }

        public bool TryGetBuildingByID(int id, out Building building)
        {
            building = _buildings.FirstOrDefault(item => item.Id == id);

            if (building != null)
                return true;
            return false;
        }
        public void SetCantBuyedBuildingCastle(string CastleID)
        {
            CanBuyBuildingCastles.Add(CastleID);
        }
        public void UpdateCanBuyBuildingCastleList()
        {
            if (CanBuyBuildingCastles.Count > 0)
            {
                CanBuyBuildingCastles.Clear();
            }
            else
            {
                return;
            }
        }
    }
}