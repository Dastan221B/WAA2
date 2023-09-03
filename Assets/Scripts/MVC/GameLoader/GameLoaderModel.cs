using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.GameResources;

public class GameLoaderModel
{
    public Action OnGameLoaded;

    private IGameDataHandler _gameDataHandler;
    private GameLoadedData _gameLoadedData;
    private TerrainLoader _terrainLoader;
    private ResourcesLoader _resourcesLoader;
    private CastlesLoader _castlesLoader;
    private MinesLoader _minesLoader;
    private HeroModelObjectLoader _heroModelObjectLoader;
    private CreaturesLoader _creaturesLoader;
    private ResourcesDataService _resourcesDataService;

    public GameLoaderModel(MinesLoader minesLoader, ResourcesDataService resourcesDataService,GameLoadedData gameLoadedData, TerrainLoader terrainLoader, ResourcesLoader resourcesLoader 
        , CastlesLoader castlesLoader, HeroModelObjectLoader heroModelObjectLoader, CreaturesLoader creaturesLoader, IGameDataHandler gameDataHandler)
    {
        _resourcesDataService = resourcesDataService;
        _gameLoadedData = gameLoadedData;
        _terrainLoader = terrainLoader;
        _resourcesLoader = resourcesLoader;
        _castlesLoader = castlesLoader;
        _heroModelObjectLoader = heroModelObjectLoader;
        _creaturesLoader = creaturesLoader;
        _gameDataHandler = gameDataHandler;
        _minesLoader = minesLoader;
    }



    private void LoadTerrain()
    {
        if(_gameLoadedData.TryGetLoadedDataByType(typeof(TerrainCellsData), out object data))
        {
            if (data is TerrainCell[][] terrainCells)
            {
                Cell[,] cells = _terrainLoader.CreateTerrain(terrainCells, new Vector2Int(terrainCells.Length, terrainCells[0].Length), out Cell[,] cellsInInvertPositions);
                _gameDataHandler.SetCells(cells, cellsInInvertPositions);
            }
        }
    }

    private void LoadResorcesMap()
    {
        if(_gameLoadedData.TryGetLoadedDataByType(typeof(ResourceMapData), out object data))
        {
            if (data is Dictionary<string, ResourceObject> resources)
                _gameDataHandler.SetResourcesStructures(_resourcesLoader.CreateStructures(resources));
        }
    }

    private void LoadCastles()
    {
        if (_gameLoadedData.TryGetLoadedDataByType(typeof(CastlesData), out object data))
        {
            if (data is Dictionary<string, CastleObject> castles)
                _gameDataHandler.SetCastless( _castlesLoader.CreateCastles(castles));
        }
    }
    private void LoadMines()
    {
        if (_gameLoadedData.TryGetLoadedDataByType(typeof(MinesData), out object data))
        {
            if (data is Dictionary<string, MineObject> mines)
                _gameDataHandler.SetMinesStructures(_minesLoader.CreateMines(mines));
        }
    }

    private void LoadHeroModels()
    {
        if (_gameLoadedData.TryGetLoadedDataByType(typeof(HeroesData), out object data))
        {
            if (data is Dictionary<string, HeroObject> heroes)
            {
                _gameDataHandler.SetHeroModelObjects(_heroModelObjectLoader.CreateHeroModelObjects(heroes));
            }
        }
    }

    private void LoadCreatures()
    {
        if (_gameLoadedData.TryGetLoadedDataByType(typeof(CreatureData), out object data))
        {
            if (data is Dictionary<string, CreatureObject> creatures)
                _gameDataHandler.SetMapCreatures( _creaturesLoader.CreateCreatures(creatures));
        }
    }

    private void LoadResources()
    {
        if (_gameLoadedData.TryGetLoadedDataByType(typeof(ResourcesData), out object data))
        {
            if (data is Dictionary<int, Resource> resources)
                _resourcesDataService.SetResources(resources);
        }
    }

    public void LoadObjects()
    {
        LoadTerrain();
        LoadCreatures();
        LoadResorcesMap();
        LoadHeroModels();
        LoadCastles();
        LoadResources();
        LoadMines();
        OnGameLoaded?.Invoke();
    }
}