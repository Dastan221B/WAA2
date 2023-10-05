using Assets.Scripts.MVC.Battle.BattleLoader;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.Battle.BattleProcess
{
    public class BattleInitalProcess : MonoBehaviour
    {
        private Transform _selfPlayerPosition;
        private Transform _enemyPlayerPosition;
        public event System.Action OnBattleInited;
        private HeroModelObjects _heroModelObjects;
        private BattleModel _battleModel;
        private HexagonGenerator _hexagonGenerator;
        private CreatureSpawner _creatureSpawner;
        private BattleInitialInfo _battleInitialInfo;
        private GameModel _gameModel;

        public void Init (GameModel gameModel,BattleModel battleModel , HexagonGenerator hexagonGenerator,
            CreatureSpawner creatureSpawner, HeroModelObjects heroModelObjects)
        {
            _gameModel = gameModel;
            _battleModel = battleModel;
            _hexagonGenerator = hexagonGenerator;
            _creatureSpawner = creatureSpawner;
            _heroModelObjects = heroModelObjects;
        }

        public void SetPositions(Transform selfPlayerPosition, Transform enemyPlayerPosition)
        {
            _selfPlayerPosition = selfPlayerPosition;
            _enemyPlayerPosition = enemyPlayerPosition;
        }

        public void SetBattleInfo(MessageInput messageInput, out BattleInitialInfo battleInitialInfo)
        {
            _battleInitialInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<BattleInitialInfo>(messageInput.body);
            battleInitialInfo = _battleInitialInfo;
        }

        public void LoadBattleObjects()
        {
            _battleModel.SetHexagons(_hexagonGenerator.GenerateHexagonField());
            _battleModel.InitBattleCreatures(_creatureSpawner.CreateBattleCreatures(_battleInitialInfo.mapObjects));
            var selfHero = _heroModelObjects.GetHeroModelObjectByID(_battleInitialInfo.assaulter.dicHeroId);
            _battleModel.SetSelfHeroObject(Instantiate(selfHero, _selfPlayerPosition.position, selfHero.transform.rotation));
            if (_battleInitialInfo.defender.dicHeroId > 0)
            {
                var enemyHero = _heroModelObjects.GetHeroModelObjectByID(_battleInitialInfo.defender.dicHeroId);
                _battleModel.SetEnemyHeroObject(Instantiate(enemyHero, _enemyPlayerPosition.position, enemyHero.transform.rotation));
            }
            //OnBattleInited?.Invoke();
        }
    }
}