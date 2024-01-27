using Assets.Scripts.GameResources.MapCreatures;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces.Battle;
using System.Drawing;
using Assets.Scripts.MVC.Battle.BattleProcess;

namespace Assets.Scripts.MVC.Battle
{
    public class BattleModel : MonoBehaviour
    {
        [SerializeField] private float _radiusHexagonsSelecting;

        public event Action<List<CreatureModelObject>> OnInitedCreatures;
        public event Action<CreatureModelObject, Sprite> OnSelectedCurrentActiveCreature;

        private BattleTurnNotificationProcess _battleTurnNotificationProcess;
        public Hexagon[,] _hexagons;
        private List<CreatureModelObject> _battleCreatures;
        private List<CreatureModelObject> _battleCreaturesFull = new List<CreatureModelObject>();
        private CreatureStackBattleObjectFullInfo _creatureStackBattleObjectFullInfo;
        private HexagonPicker _hexagonPicker;
        public Hexagon CurrentHexagon { get; private set; }
        public CreatureStackBattleObjectFullInfo ActiveCreatureStackBattleObjectFullInfo => _creatureStackBattleObjectFullInfo;
        private HeroModelObject _selfHero;
        private HeroModelObject _enemyHero;
        private Transform _battleSceneObjectsParent;
        private ModelCreatures _modelCreatures;
        private List<CreatureModelObject> _deathCreatures = new List<CreatureModelObject>();
        private CommonData _commonData;
        private IClearHexagonFrameList _clearHexagonFrameList;

        public Action ActionBeforeMove;

        private bool _isEndedGame;
        private Action _endGame;
        private bool _isEndGameComplete;
        public bool _endedGameInProcess;
        public bool IsCreatureInAction { get; private set; }
        public bool IsInited { get; private set; }
        public bool InitedHexagonsForCreature { get; private set; }
        public IEnumerable<CreatureModelObject> DeathCreatures => _deathCreatures;
        public IReadOnlyCollection<CreatureModelObject> CreatureModelObjectsSelf => _battleCreatures.Where(item => item.CreatureSide == CreatureSide.Self).ToList();
        public IReadOnlyCollection<CreatureModelObject> CreatureModelObjects => _battleCreatures;
        public IReadOnlyCollection<CreatureModelObject> CreatureModelObjectsFull => _battleCreaturesFull;

        public float RadiusHexagonsSelecting => _radiusHexagonsSelecting;

        public void Init(HexagonPicker hexagonPicker,ModelCreatures modelCreatures, CommonData commonData, IClearHexagonFrameList clearHexagonFrameList)
        {
            _hexagonPicker = hexagonPicker;
            _modelCreatures = modelCreatures;
            _commonData = commonData;
            _clearHexagonFrameList = clearHexagonFrameList;
        }

        public void InitBattleTurnNotificationProcess(BattleTurnNotificationProcess battleTurnNotificationProcess)
        {
            _battleTurnNotificationProcess = battleTurnNotificationProcess;
        }

        public bool TryGetHexagonByCoordinates(int x, int y, out Hexagon hexagon)
        {
            try
            {
                int rows = _hexagons.GetUpperBound(0) + 1;
                int columns = _hexagons.Length / rows;
                if (rows >= x && columns >= y)
                {
                    hexagon = _hexagons[x, y];
                    return true;
                }
                hexagon = null;
                return false;
            }
            catch
            {
                hexagon = null;
                return false;
            }

        }

        private Hexagon _currentHexagon;

        private void Update()
        {
            int isCreatureInIDLE = CreatureModelObjectsFull.Where(item => item.IsIdle).ToList().Count;

            if (isCreatureInIDLE != CreatureModelObjectsFull.Count)
            {
                if(ActionBeforeMove != null)
                {
                    ActionBeforeMove?.Invoke();
                    ActionBeforeMove -= _battleTurnNotificationProcess.InitMap;
                }
            }

            if(Input.GetMouseButtonDown(2) && _hexagonPicker.TryPickHexagon(out Hexagon hexagon))
            {
                if(_currentHexagon == null)
                {
                    _currentHexagon = hexagon;
                }
                else
                {
                    double distance = Distance(_currentHexagon, hexagon);
                          
                    _currentHexagon = null;
                    Debug.Log("Distance " + distance);
                }
            }
        }

        public void ResetInitedHexagonsForCreature()
        {
            InitedHexagonsForCreature = false;
        }

        public void ResetHexagons()
        {
            foreach (var item in _hexagons)
            {
                item.DisableToMove();
                item.Unselect();
            }
            if (TryGetHexagonByCoordinates(_creatureStackBattleObjectFullInfo.battleFieldCoordinates.x, _creatureStackBattleObjectFullInfo.battleFieldCoordinates.y, out Hexagon hexagon2))
                hexagon2.PaintToGreen();
        }

        public void ResetHexagonsColors()
        {
            foreach (var item in _hexagons)
            {
                item.UnselectColor();
            }
            if (TryGetHexagonByCoordinates(_creatureStackBattleObjectFullInfo.battleFieldCoordinates.x, _creatureStackBattleObjectFullInfo.battleFieldCoordinates.y, out Hexagon hexagon2))
                hexagon2.PaintToGreen();
        }

        public double Distance(Hexagon hex1, Hexagon hex2)
        {
            double deltaX = hex2.transform.position.x - hex1.transform.position.x;
            double deltaY = hex2.transform.position.z - hex1.transform.position.z;

            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }

        public void InitHexagonsForCreature()
        {
            if (_hexagons == null)
                return;

            if (_creatureStackBattleObjectFullInfo == null)
                return;

            _commonData.TryGetDicCreatureDTOByID((int)_creatureStackBattleObjectFullInfo.dicCreatureId, out DicCreatureDTO dicCreatureDTO);
            ResetHexagons();
            DicCreatureDTO creatureDTO = dicCreatureDTO;
            BattleFieldCoordinates creatureCoordinates = _creatureStackBattleObjectFullInfo.battleFieldCoordinates;
 
            var rightBorder = new BattleFieldCoordinates(creatureCoordinates.x + creatureDTO.speed, creatureCoordinates.y);
            var leftBorder = new BattleFieldCoordinates(creatureCoordinates.x - creatureDTO.speed, creatureCoordinates.y);
            var topBorder = new BattleFieldCoordinates(creatureCoordinates.x, creatureCoordinates.y + creatureDTO.speed);
            var bottomBorder = new BattleFieldCoordinates(creatureCoordinates.x, creatureCoordinates.y - creatureDTO.speed);

            foreach (Hexagon hex in _hexagons)
            {
                if (TryGetHexagonByCoordinates(_creatureStackBattleObjectFullInfo.battleFieldCoordinates.x, _creatureStackBattleObjectFullInfo.battleFieldCoordinates.y, out Hexagon hexagon2))
                {
                    // Проверяем расстояние от точки до центра каждого шестиугольника
                    //float distance = Vector3.Distance(hex.transform.position
                    //, hexagon2.transform.position);

                    double distance = Distance(hex, hexagon2);

                    // Если расстояние меньше или равно половине длины стороны шестиугольника (радиусу), выделяем шестиугольник
                    if (distance <= creatureDTO.speed)
                    {
                        //Debug.Log("Distance From Move " + distance + "  Speed  " + creatureDTO.speed);
                        hex.AvalableToMove();
                    }
                    else
                    {
                        hex.DisableToMove();
                    }
                }
            }


            foreach (var item in _hexagons)
                item.PaintHexagonInCreatureSide();

            if (TryGetHexagonByCoordinates(_creatureStackBattleObjectFullInfo.battleFieldCoordinates.x, _creatureStackBattleObjectFullInfo.battleFieldCoordinates.y, out Hexagon hexagon3))
            {
                hexagon3.PaintToGreen();
                CurrentHexagon = hexagon3;
            }
            InitedHexagonsForCreature = true;
        }

        public void SetBattleSceneObjectsParent(Transform transform)
        {
            _battleSceneObjectsParent = transform;
        }

        public void EnterCreatureInAction()
        {
            IsCreatureInAction = true;
        }

        private void CreatureExitFromAction(CreatureModelObject creatureModelObject)
        {
            IsCreatureInAction = false;
        }

        public Hexagon GetHexagonByCoordinates(int x, int y)
        {
            return _hexagons[x, y];
        }

        public bool TryGetCreatureByID(string id, out CreatureModelObject creature)
        {
            if (_battleCreatures == null)
            {
                creature = null;
                return false;
            }
            foreach (var item in _battleCreatures)
            {
                if (id == item.MapObjectID)
                {
                    creature = item;
                    return true;
                }
            }
            creature = null;
            return false;
        }

        public bool TryGetCreatureByID(int id, out CreatureModelObject creature)
        {
            if (_battleCreatures == null)
            {
                creature = null;
                return false;
            }
            foreach (var item in _battleCreatures)
            {
                if (id == item.CreatureID)
                {
                    creature = item;
                    return true;
                }
            }
            creature = null;
            return false;
        }

        public void SetCreatureStackBattleObjectFullInfo(CreatureStackBattleObjectFullInfo creatureStackBattleObjectFullInfo)
        {
            _creatureStackBattleObjectFullInfo = creatureStackBattleObjectFullInfo;

            if (_creatureStackBattleObjectFullInfo != null && TryGetCreatureByID(_creatureStackBattleObjectFullInfo.battleFieldObjectId, out CreatureModelObject creatureModelObject))
            {
                //CurrentHexagon = creatureModelObject;
                OnSelectedCurrentActiveCreature?.Invoke(creatureModelObject, _modelCreatures.GetIconById((int)creatureModelObject.SpriteID - 1));
            }
        }
        
        public void AddCreature(CreatureModelObject creatureModelObject)
        {
            _battleCreaturesFull.Add(creatureModelObject);
        }

        public void RemoveCreature(CreatureModelObject creatureModelObject)
        {
            _battleCreaturesFull.Remove(creatureModelObject);
            creatureModelObject.OnCreatureDeath -= RemoveCreature;
        }

        public void InitBattleCreatures(List<CreatureModelObject> battleCreatures)
        {
            if (battleCreatures.Count <= 0)
                throw new Exception("Battles creatures list is empty");
            _battleCreatures = battleCreatures;
            List<CreatureModelObject> creatures = new List<CreatureModelObject>();
            foreach (var item in _battleCreatures)
            {
                item.OnActionEnded += CreatureExitFromAction;
                item.OnCreatureDeath += HandleCreatureDeath;
                item.OnStartCreatureDeath += AddCreatureToDeath;
                item.OnCreatureDeath += RemoveCreature;

                if (item.CreatureSide == CreatureSide.Self)
                    creatures.Add(item);
            }
            OnInitedCreatures?.Invoke(creatures);
            if (_creatureStackBattleObjectFullInfo != null && TryGetCreatureByID(_creatureStackBattleObjectFullInfo.battleFieldObjectId, out CreatureModelObject creatureModelObject))
            {
                OnSelectedCurrentActiveCreature?.Invoke(creatureModelObject, _modelCreatures.GetIconById((int)creatureModelObject.SpriteID - 1));
            }
            IsCreatureInAction = false;
            IsInited = true;
        }

        public void SetHexagons(Hexagon[,] hexagons)
        {
            if (hexagons.Length <= 0)
                throw new Exception("Hexagons list is empty");
            _hexagons = hexagons;
            InitHexagonsForCreature();
        }

        public void ClearBattleScene()
        {
            _deathCreatures.Clear();
            _battleSceneObjectsParent.gameObject.SetActive(false);
            foreach (var item in _hexagons)
                MonoBehaviour.Destroy(item.gameObject);
            foreach (var item in _battleCreatures)
            {
                if (item != null && item.gameObject != null)
                    MonoBehaviour.Destroy(item.gameObject);
            }

            _clearHexagonFrameList.ResetHexagonFrameList();
            _battleCreatures.Clear();
            if (_selfHero != null && _selfHero.gameObject != null)
            {
                MonoBehaviour.Destroy(_selfHero.gameObject);
            }
            //MonoBehaviour.Destroy(_enemyHero.gameObject);

        }

        public void EnterInBattle()
        {
            _endedGameInProcess = false;
            _isEndGameComplete = false;
            if (_battleSceneObjectsParent != null)
                _battleSceneObjectsParent.gameObject.SetActive(true);
        }

        public void EndGame(Action endGameAction)
        {
            IsInited = false;
            _isEndedGame = true;
            _endGame = endGameAction;
            Invoke(nameof(CallEndGame), 7f);
        }

        public void CallEndGame()
        {
            if (!_endedGameInProcess)
            {
                _isEndGameComplete = true;
                _endGame?.Invoke();
            }
        }

        private void CheckToEndGame(CreatureModelObject creatureModelObject)
        {
            if (_isEndedGame && !_isEndGameComplete)
            {
                _isEndGameComplete = true;
                _endedGameInProcess = true;
                _isEndedGame = false;
                _endGame?.Invoke();
            }
        }

        public void AddCreatureToDeath(CreatureModelObject battleCreature)
        {
            battleCreature.OnStartCreatureDeath -= AddCreatureToDeath;
            _deathCreatures.Add(battleCreature);
        }

        private void HandleCreatureDeath(CreatureModelObject battleCreature)
        {
            battleCreature.OnCreatureDeath -= HandleCreatureDeath;
            if (TryGetHexagonByCoordinates((int)battleCreature.transform.position.x,
                (int)battleCreature.transform.position.x, out Hexagon hexagon))
            {
                hexagon.SetCreature(null);
                _battleCreatures.Remove(battleCreature);
                //battleCreature.OnCreatureDeath += HandleCreatureDeath;
            }
            CheckToEndGame(battleCreature);
        }

        public void SetSelfHeroObject(HeroModelObject heroModelObject)
        {
            heroModelObject.transform.localScale *= 2;

            _selfHero = heroModelObject;
        }

        public void SetEnemyHeroObject(HeroModelObject heroModelObject)
        {
            heroModelObject.transform.localScale *= 2;
            heroModelObject.transform.rotation = Quaternion.Euler(0, -90, 0);
            _enemyHero = heroModelObject;
        }
    }
}