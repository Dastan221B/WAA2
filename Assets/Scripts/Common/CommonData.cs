using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.Scripts.Client.DTO;

public class CommonData : MonoBehaviour
{
    public event System.Action OnEndedLoadingBuildings;

    [SerializeField] private ModelCreatures _modelCreatures;
    [SerializeField] private Heroes _heroes;
    private Dictionary<long, DicCastleDTO> _castleDictianory = new Dictionary<long, DicCastleDTO>();
    private Dictionary<long, DicBuildingDTO> _buildingsDictianory = new Dictionary<long, DicBuildingDTO>();
    private Dictionary<long, DicCreatureDTO> _creaturesDictianory = new Dictionary<long, DicCreatureDTO>();
    private Dictionary<long, DicHeroDTO> _heroesDictianory = new Dictionary<long, DicHeroDTO>();

    public IReadOnlyDictionary<long, DicBuildingDTO> BuildingDictianory => _buildingsDictianory;
    public IReadOnlyDictionary<long, DicCreatureDTO> CreaturesDictianory => _creaturesDictianory;
    public IReadOnlyDictionary<long, DicCastleDTO> CastleDictianory => _castleDictianory;
    public IReadOnlyDictionary<long, DicHeroDTO> HeroesDictianory => _heroesDictianory;

    public void SetDicCreatureDTOList(List<DicCreatureDTO> dicCreatureDTOs)
    {
        foreach(var item in dicCreatureDTOs)
        {
            _creaturesDictianory.Add(item.id, item);
            _modelCreatures.TrySetDicCreatureDTO((int) item.id, item);
        }
    }


    public void SetHeroesDTO(List<DicHeroDTO> heroes)
    {
        int id = 0;
        foreach (var item in heroes)
        {
            _heroesDictianory.Add(item.id, item);
            _heroes.HeroesList[id].Init(item);
            id++;
        }
    }

    public void SetBuildingsInfo(List<DicBuildingDTO> buildings)
    {
        foreach (var item in buildings)
        {
            _buildingsDictianory.Add(item.id, item);
        }
        OnEndedLoadingBuildings?.Invoke();
    }

    public void SetCastlesDTO(List<DicCastleDTO> castlesDTO)
    {
        foreach (var item in castlesDTO)
        {
            _castleDictianory.Add(item.id, item);
        }
    }


    public bool TryGetDicBuildingDTOByID(int id, out DicBuildingDTO building)
    {
        building = _buildingsDictianory[id];
       
        if (building != null)
            return true;
        return false;
    }

    public bool TryGetDicCastleDTOByID(int id, out DicCastleDTO castle)
    {
        castle = _castleDictianory[id];
        if (castle != null)
            return true;
        return false;
    }

    public bool TryGetDicCreatureDTOByID(int id, out DicCreatureDTO dicCreatureDTO)
    {
        dicCreatureDTO = _creaturesDictianory[id];
        if (dicCreatureDTO != null)
            return true;
        return false;
    }

    public bool TryGetDicCreatureDTOByDicID(int id, out DicCreatureDTO dicCreatureDTO)
    {
        dicCreatureDTO = _creaturesDictianory.Values.SingleOrDefault(item => id == item.id);
        if (dicCreatureDTO != null)
            return true;
        return false;
    }
}