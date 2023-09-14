using Assets.Scripts.GameResources;
using Assets.Scripts.GameResources.MapCreatures;
using Assets.Scripts.MVC.Game;
using Assets.Scripts.MVC.Game.Views;
using Assets.Scripts.MVC.Ground;
using Assets.Scripts.MVC.TradeMVC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class GameModel : IGameDataHandler
{
    public event Action<Player> OnEnteredInTurn;
    public event Action OnUpdatedTurn;
    public event Action OnExitFromTurn;
    public event Action<int> OnTickTimer;
    public event Action OnStatedTurn;
    public event Action<Player> OnPlayerEnterInTurn;
    public event Action OnEnterInBattleScene;
    public event Action OnEnterInGameFromBattleScene;
    public event Action OnUpdateGameDate;

    public string GameSessionID { get; private set; }

    private List<Castle> _castlesTurn = new List<Castle>();
    private List<HeroModelObject> _heroModelObjectsTurn = new List<HeroModelObject>();
    private List<Player> _players = new List<Player>();

    private GameAndBattleCommandsSender _gameAndBattleCommandsSender;
    public HeroModelObject SelectedHero { get; private set; }
    public HeroModelObject HeroSelf { get; private set; }
    public HeroModelObject HeroOpponent { get; private set; }

    private Cell[,] _cells;
    private Cell[,] _cellsIninversionSpace;
    private List<ResourceSturcture> _resourcesSturctures;
    private List<MineStructure> _mineSturctures;
    private List<Castle> _castles;
    private List<HeroModelObject> _heroModelObjects;
    private List<CreatureModelObject> _mapCreatures;
    private GroundModel _groundModel;
    private GameMapObject _creatureModelObject;
    private Cell _cellHasFightedCreature;
    private GameTurnView _gameTurnView;
    private TradeController _tradeController;
    private SystemColors _systemColors;
    public GameMapObject CreatureModelObject => _creatureModelObject;
    public Castle AttackedCastle { get; private set; }
    public bool IsLasFightWin { get; private set; }
    public bool IsAttacked { get; private set; }
    public bool IsAttackedOnHero { get; private set; }

    public int DaysCounter { get; private set; }
    public int WeeksCounter { get; private set; }
    public int MonthsCounter { get; private set; }

    public string CastleToAttack;
    public List<List<string>> InteractiveObjectIdToGameObjectType => new List<List<string>>
                                {
                                    _castles.Select(item => item.MapObjectID).ToList(),
                                    _mapCreatures.Select(item => item.MapObjectID).ToList(),
                                    _heroModelObjects.Select(item => item.MapObjectID).ToList(),
                                    _resourcesSturctures.Select(item => item.MapObjectID).ToList(),
                                    _mineSturctures.Select(item => item.MapObjectID).ToList()
                                };

    public bool IsCurrentTurn { get; private set; }
    public bool IsHeroMove { get; private set; }
    public Transform SceneObjectsParent { get; private set; }
    public Transform TerrainObjectsParent { get; private set; }
    public Vector2Int MapSize { get; private set; }

    public Vector2Int PositionSelectedHeroOnGrid
    {
        get
        {
            if (SelectedHero != null)
            {
                return SelectedHero.transform.position.ToVector2IntInHeroPosition();
            }
            return Vector2Int.zero;
        }
    }
    public IReadOnlyList<Castle> CastlesTurn => _castlesTurn;
    public IReadOnlyList<HeroModelObject> HeroModelObjectsTurn => _heroModelObjectsTurn;
    public IReadOnlyCollection<Player> Players => _players;
    public IReadOnlyList<Castle> Castles => _castles;
    public Cell[,] Cells => _cells;

    public GameModel(GroundModel groundModel)
    {
        _groundModel = groundModel;
    }

    public void SetLastFightResult(bool result)
    {
        IsLasFightWin = result;
    }

    public void SetMapSize(Vector2Int mapSize)
    {
        MapSize = mapSize;
    }

    public void Init(SystemColors systemColors,TradeController tradeController,GameTurnView gameTurnView, GameAndBattleCommandsSender gameAndBattleCommandsSender)
    {
        _systemColors = systemColors;
        _tradeController = tradeController;
        _gameTurnView = gameTurnView;
        _gameAndBattleCommandsSender = gameAndBattleCommandsSender;
    }

    public void SetHeroInFight(HeroModelObject self)
    {
        HeroSelf = self;
    }

    public void SetHeroOpponent(HeroModelObject opponent)
    {
        HeroOpponent = opponent;
    }

    public void ResetHeroInFight()
    {
        HeroSelf = null;
    }

    public void StartAttackOnHero()
    {
        IsAttackedOnHero = true;
    }

    public void ExitFromFightHero()
    {
        IsAttackedOnHero = false;
    }

    public void StartAttack()
    {
        IsAttacked = true;
    }

    public void ExitFromFight()
    {
        IsAttacked = false;
    }

    public void Surrender()
    {
        _gameAndBattleCommandsSender.SendEndTurnRequest();
    }

    public void ReplaceToLastPlaceInTurn(HeroModelObject heroModelObject)
    {
        _heroModelObjectsTurn.Remove(heroModelObject);
        _heroModelObjectsTurn.Add(heroModelObject);
        //_heroModelObjectsTurn.Reverse();
    }

    public void SetDate(int daysCounter, int weeksCounter, int monthsCounter)
    {
        DaysCounter = daysCounter;
        WeeksCounter = weeksCounter;
        MonthsCounter = monthsCounter;
        OnUpdateGameDate?.Invoke();
    }

    public void InitedData()
    {
        foreach (var castle in _castles)
        {
            TryGetCellByPosition(new Vector2Int(castle.GatePosition.x, castle.GatePosition.y * -1), out Cell cell);
            cell.SetInteractiveMapObjectId(castle.MapObjectID);
            cell.SetCasle(castle);
            cell.GetComponent<MeshRenderer>().material.color = Color.black;
            cell.SetBaseInteractiveMapObjectId(castle.MapObjectID);
            for (int x = castle.GatePosition.x; x < castle.Width + castle.GatePosition.x; x++)
            {
                for (int y = castle.GatePosition.y; y > castle.GatePosition.y - castle.Height; y--)
                {
                    int rows = _cells.GetUpperBound(0) + 1;    // количество строк
                    int columns = _cells.Length / rows;        // количество столбцов
                    _cells[x, y * -1].SetParentObjectId(castle.MapObjectID);
                    _cells[x, y * -1].SetCasle(castle);

                    if (x < rows && y < columns)
                    {
                        _cells[x, y * -1].SetParentObjectId(castle.MapObjectID);
                        _cells[x, y * -1].SetCasle(castle);
                    }
                }
            }
            //_cells[castle.GatePosition.X + 1, castle.GatePosition.Y * -1].SetParentObjectId(castle.MapObjectID);
            //_cells[castle.GatePosition.X + 1, (castle.GatePosition.Y + 1) * -1].SetParentObjectId(castle.MapObjectID);
            _cells[castle.GatePosition.x + 1, (castle.GatePosition.y - 1) * -1].SetInteractiveMapObjectId(castle.MapObjectID);
            Cell cellGate = _cells[castle.GatePosition.x + 1, (castle.GatePosition.y - 1) * -1];
            Vector2Int position = new Vector2Int((int)cellGate.transform.position.x, (int)cellGate.transform.position.y);
            castle.SetGatePosition(position);
            castle.SetGateCell(cellGate);
            //_cells[castle.GatePosition.X - 1, (castle.GatePosition.Y + 1) * -1].SetParentObjectId(castle.MapObjectID);
            //_cells[castle.GatePosition.X - 1, castle.GatePosition.Y * -1].SetParentObjectId(castle.MapObjectID);
            castle.SetCellPlace(cell);

        }
        foreach (var hero in _heroModelObjects)
        {
            var position = hero.transform.position.ToVector2IntInHeroPosition();
            TryGetCellByPosition(new Vector2Int(position.x, position.y * -1), out Cell cell);
            hero.SetCellStayed(cell);
            cell.SetInteractiveMapObjectId(hero.MapObjectID);
            cell.SetBaseInteractiveMapObjectId(hero.MapObjectID);
            hero.SetCellPlace(cell);
            if(TryGetCastleByHeroID(hero.MapObjectID, out Castle castle))
            {
                hero.SetOrdinal(castle.Oridinal);
            }
        }
        foreach (var resource in _resourcesSturctures)
        {
            var position = resource.transform.position.ToVector2IntInHeroPosition();
            TryGetCellByPosition(new Vector2Int(position.x, position.y * -1), out Cell cell);
            cell.SetGameMapObject(resource);
            cell.SetInteractiveMapObjectId(resource.MapObjectID);
            resource.SetCellPlace(cell);
        }
        foreach (var mine in _mineSturctures)
        {
            TryGetCellByPosition(new Vector2Int(mine.GatePosition.x, mine.GatePosition.y * -1), out Cell cell);
            cell.SetInteractiveMapObjectId(mine.MapObjectID);
            cell.GetComponent<MeshRenderer>().material.color = Color.black;
            cell.SetBaseInteractiveMapObjectId(mine.MapObjectID);
            cell.SetMineStructure(mine);
            for (int x = mine.GatePosition.x; x <= mine.Width + mine.GatePosition.x; x++)
            {
                for (int y = mine.GatePosition.y; y > mine.GatePosition.y - mine.Height; y--)
                {
                    int rows = _cells.GetUpperBound(0) + 1;    // количество строк
                    int columns = _cells.Length / rows;        // количество столбцов
                    if (x < rows && y < columns)
                    {
                        _cells[x, y * -1].SetParentObjectId(mine.MapObjectID);
                        _cells[x, y * -1].SetGameMapObjectType(GameMapObjectType.MINE);
                        _cells[x, y * -1].SetMineStructure(mine);
                    }
                }
            }
            _cells[mine.GatePosition.x + 1, (mine.GatePosition.y - 1) * -1].SetInteractiveMapObjectId(mine.MapObjectID);
            Cell cellGate = _cells[mine.GatePosition.x + 1, (mine.GatePosition.y - 1) * -1];
            mine.SetGateCell(cellGate);
            mine.SetGatePosition(new Vector2Int(mine.GatePosition.x + 1, (mine.GatePosition.y - 1)));
            mine.SetCellPlace(cell);
        }
        foreach (var creature in _mapCreatures)
        {
            var position = creature.transform.position.ToVector2IntInHeroPosition();
            TryGetCellByPosition(new Vector2Int(position.x, position.y * -1), out Cell cell);
            cell.SetInteractiveMapObjectId(creature.MapObjectID);
            cell.SetModelObject(creature);
            cell.SetBaseInteractiveMapObjectId(creature.MapObjectID);
            creature.SetCellPlace(cell);
        }
    }

    public void SetSceneObjectsParent(Transform sceneObjectsParent, Transform terrainParent)
    {
        SceneObjectsParent = sceneObjectsParent;
        TerrainObjectsParent = terrainParent;
    }

    public void InitGameSession(IEnumerable<SessionParticipant> sessionParticipants)
    {
        foreach (var particicpant in sessionParticipants)
        {
            Player player = new Player(particicpant.UserInfo.UserName, particicpant.UserInfo.Email, particicpant.Ordinal,
                    particicpant.DicCastleId, particicpant.DicHeroId, particicpant.UserId);

            _players.Add(player);
        }
    }

    public bool TryGetResourceByResourceObjectID(string resourcesID, out ResourceSturcture resourceSturcture)
    {
        resourceSturcture = _resourcesSturctures.FirstOrDefault(item => item.MapObjectID == resourcesID);
        if (resourceSturcture != null)
            return true;
        return false;
    }

    public void EnterInBattleScene()
    {
        SceneObjectsParent.gameObject.SetActive(false);
        OnEnterInBattleScene?.Invoke();
    }

    public void SetAttakedCastle(Castle castle)
    {
        AttackedCastle = castle;
    }

    public void EnterInGameFromBattleScene()
    {
        SceneObjectsParent.gameObject.SetActive(true);
        if (_creatureModelObject is CreatureModelObject creature)
            _mapCreatures.Remove(creature);
        if (_creatureModelObject != null && _creatureModelObject.gameObject != null)
        {
            MonoBehaviour.Destroy(_creatureModelObject.gameObject);
            _creatureModelObject = null;
        }
        if (_cellHasFightedCreature != null)
            _cellHasFightedCreature.ResetInteractiveMapObject();

        if(HeroSelf != null && HeroSelf.InCastle)
        {
            HeroSelf.EnterInCastle();
            HeroSelf.gameObject.SetActive(false);
        }

        if (HeroOpponent != null && HeroOpponent.InCastle)
        {
            HeroOpponent.EnterInCastle();
            HeroOpponent.gameObject.SetActive(false);
        }
        if (HeroSelf != null && !IsLasFightWin)
        {
            HeroSelf.CellPlace.ResetInteractiveMapObject();
            _heroModelObjects.Remove(HeroSelf);
            _heroModelObjectsTurn.Remove(HeroSelf);

            if (HeroSelf.gameObject != null)
            {
                MonoBehaviour.Destroy(HeroSelf.gameObject);
            }
            OnUpdatedTurn?.Invoke();
            _gameTurnView.ResetDisplayHeroes();
        }else if(HeroOpponent != null && IsLasFightWin)
        {
            if(AttackedCastle != null)
            {
                _castlesTurn.Add(AttackedCastle);
                AttackedCastle.SetupColorCube(_systemColors.GetColorByOrdinal(HeroSelf.Ordinal));
                AttackedCastle = null;
            }
            HeroOpponent.CellPlace.ResetInteractiveMapObject();
            _heroModelObjects.Remove(HeroOpponent);
            _heroModelObjectsTurn.Remove(HeroOpponent);
            if (HeroOpponent.gameObject != null)
                MonoBehaviour.Destroy(HeroOpponent.gameObject);
            OnUpdatedTurn?.Invoke();
            _gameTurnView.ResetDisplayHeroes();
        }
        
        ResetHeroInFight();
        ExitFromFight();
        ExitFromFightHero();
        OnEnterInGameFromBattleScene?.Invoke();
    }

    public void RemoveCasle(Castle castle)
    {
        _castlesTurn.Remove(castle);
    }

    public void TryCaptureCastle()
    {
        if (AttackedCastle != null && !_castlesTurn.Contains(AttackedCastle))
        {
            _castlesTurn.Add(AttackedCastle);
            AttackedCastle = null;
        }
    }

    public void SetGameSessionID(string sessionID)
    {
        GameSessionID = sessionID;
    }

    public void StopHeroMove()
    {
        IsHeroMove = false;
    }

    public void HeroEndedMove(HeroModelObject heroModelObject)
    {
        IsHeroMove = false;
        //var position = self.transform.position.ToVector2IntInHeroPosition();
        //Debug.Log(position);
        //TryGetCellByPosition(new Vector2Int(position.X, position.Y * -1), out Cell cell);
        //cell.SetInteractiveMapObjectId(self.MapObjectID);
        //self.SetCellStayed(cell);
        heroModelObject.Idle();
    }

    public void HeroStartMove()
    {
        IsHeroMove = true;
    }

    public void EnterInTurn(TurnNotificationInfo turnNotificationInfo)
    {
        if(_tradeController.InTrade)
            _tradeController.ExitFromTradePanel();

        foreach (var item in _heroModelObjects)
            item.HeroInTurn = false;
        _heroModelObjectsTurn.Clear();
        _castlesTurn.Clear();
        if (TryGetPlayerByOrdinal(turnNotificationInfo.ordinal, out Player player))
        {
            player.heroesInfo = turnNotificationInfo.heroesInfo;
            player.castlesInfo = turnNotificationInfo.castlesInfo;
            foreach (var hero in turnNotificationInfo.heroesInfo)
            {
                if (TryGetHeroModelObject(hero.Key, out HeroModelObject heroModelObject))
                {
                    heroModelObject.SetMovePointsLeft(hero.Value.movePoints);
                    heroModelObject.Init(hero.Value);
                    heroModelObject.HeroInTurn = true;
                    _heroModelObjectsTurn.Add(heroModelObject);
                }
            }

            foreach (var castle in turnNotificationInfo.castlesInfo.Values)
            {
                if (TryGetCastleByID(castle.mapObjectId, out Castle castleObject))
                {
                    _castlesTurn.Add(castleObject);
                }
            }
            IsCurrentTurn = true;
            OnUpdatedTurn?.Invoke();
            OnEnteredInTurn?.Invoke(player);
            
        }
    }

    public void SetFightedCreatureSettings(GameMapObject creatureModelObject, Cell cell)
    {
        _creatureModelObject = creatureModelObject;
        _cellHasFightedCreature = cell;
    }

    public void ExitFromTurn()
    {
        IsCurrentTurn = false;
        OnExitFromTurn?.Invoke();
    }

    public void PlayerEnterInTurn(int ordinalPlayer)
    {
        if (TryGetPlayerByOrdinal(ordinalPlayer, out Player player))
        {
            OnPlayerEnterInTurn?.Invoke(player);
        }
    }

    public void PlayerStartTurn()
    {
        OnStatedTurn?.Invoke();     
    }

    public void SetCells(Cell[,] cells, Cell[,] cellsInInversionSpace)
    {
        _cells = cells;
        _cellsIninversionSpace = cellsInInversionSpace;
        _groundModel.SetCells(_cellsIninversionSpace);
    }

    public void SetCastless(List<Castle> castles)
    {
        _castles = castles;
    }

    public void SetResourcesStructures(List<ResourceSturcture> resourcesSturctures)
    {
        _resourcesSturctures = resourcesSturctures;
    }
    public void SetMinesStructures(List<MineStructure> mineStructures)
    {
        _mineSturctures = mineStructures;
    }

    public void SetHeroModelObjects(List<HeroModelObject> heroModelObjects)
    {
        _heroModelObjects = heroModelObjects;
    }

    public void SetMapCreatures(List<CreatureModelObject> mapCreatures)
    {
        _mapCreatures = mapCreatures;
    }


    public void SetSelectedHero(HeroModelObject heroModelObject)
    {
        if (SelectedHero != null)
            SelectedHero.UnselectHero();
        SelectedHero = heroModelObject;   
        SelectedHero.SelectHero();
        
    }

    public bool TryGetPlayerByCastleID(int id, out Castle castle)
    {
        castle = _castles.FirstOrDefault(item => item.DicCastleID == id);
        if (castle != null)
            return true;
        return false;
    }

    public bool TryGetCastleByID(string id, out Castle castle)
    {
        castle = _castles.FirstOrDefault(item => item.MapObjectID == id);
        if (castle != null)
            return true;
        return false;
    }

    public bool TryGetCastleByHeroID(string id, out Castle castle)
    {
        castle = _castles.FirstOrDefault(item => item.HeroModelObjectMapID == id);
        if (castle != null)
            return true;
        return false;
    }

    public bool TryGetPlayerByOrdinal(int ordinal, out Player player)
    {
        player = _players.FirstOrDefault(item => item.Ordinal == ordinal);
        if (player != null)
            return true;
        return false;
    }

    public bool TryGetCellByPosition(Vector2Int position, out Cell cell)
    {
        int rows = _cells.GetUpperBound(0) + 1;
        int columns = _cells.Length / rows;
        if (rows >= position.x && columns >= position.y)
        {
            cell = _cells[position.x, position.y];
            return true;
        }
        cell = null;
        return false;
    }

    public bool TryGetHeroModelObject(string id, out HeroModelObject heroModelObject)
    {

        heroModelObject = _heroModelObjects.FirstOrDefault(item => item.MapObjectID == id);
        if (heroModelObject != null)
            return true;
        return false;

    }

    public bool TryGetMineStructure(string id, out MineStructure mine)
    {
        mine = _mineSturctures.FirstOrDefault(item => item.MapObjectID == id);
        if (mine != null)
            return true;
        return false;

    }

    public bool TryGetHeroModelObjectForCoursor(string id, out HeroModelObject heroModelObject)
    {

        heroModelObject = _heroModelObjectsTurn.FirstOrDefault(item => item.MapObjectID == id);
        if (heroModelObject != null)
            return true;
        return false;
    }
    public bool TryGetCastle(string id, out Castle castle)
    {

        castle = _castles.FirstOrDefault(item => item.MapObjectID == id);
        if (castle != null)
            return true;
        return false;

    }
    public bool TryGetMine(string id, out MineStructure mine)
    {

        mine = _mineSturctures.FirstOrDefault(item => item.MapObjectID == id);
        if (mine != null)
            return true;
        return false;

    }
    public bool TryGetPlayerByHeroID(string id, out Player player)
    {
        player = _players.FirstOrDefault(item => item.heroesInfo.Values.Select(x => x.mapObjectId).Contains(id));
        if(player != null) return true;
        return false;
    }
    public bool TryGetPlayerByCastleID(string id, out Player player)
    {
        player = _players.FirstOrDefault(item => item.castlesInfo.Values.Select(x => x.mapObjectId).Contains(id));
        if (player != null) return true;
        return false;
    }
}