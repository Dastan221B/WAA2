using Assets.Scripts.GameResources.MapCreatures;
using Assets.Scripts.MVC.Battle.BattleProcess;
using Assets.Scripts.MVC.Battle.Views;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MVC.Battle.BattleLoader
{
    public class BattleSceneLoadingTrigger : MonoBehaviour
    {

        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private CreatureStatsPanel _creatureStatsPanel;
        [SerializeField] private TMP_Text _resultText;
        [SerializeField] private Button _endGameButton;
        [SerializeField] private Transform _enemyCreatures;
        [SerializeField] private Transform _selfCreatures;
        [SerializeField] private GameObject _panel;

        [SerializeField] private CreatureBattleIcon _currentCreatureBattleIcon;
        [SerializeField] private FightOperationsBar _fightOperationsBar;
        [SerializeField] private List<CreatureBattleIcon> _creatureBattleIcons;
        [SerializeField] private Transform _battleSceneObjectsParent;
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _selfPlayerPosition;
        [SerializeField] private Transform _enemyPlayerPosition;

        private void Awake()
        {
            BattleController battleController = FindObjectOfType<BattleController>();
            BattleInitalProcess battleInitalProcess = FindObjectOfType<BattleInitalProcess>();
            BattleView battleView = FindObjectOfType<BattleView>();
            ResultPanel resultPanel = FindObjectOfType<ResultPanel>();
            BattleTimer battleTimer = FindObjectOfType<BattleTimer>();

            if(battleTimer == null)
                throw new System.Exception("BattleTimer not found");
            if (resultPanel == null)
                throw new System.Exception("ResultPanel not found");
            if (battleView == null)
                throw new System.Exception("BattleView not found");
            if (battleController == null)
                throw new System.Exception("BattleController not found");
            if (battleInitalProcess == null)
                throw new System.Exception("BattleInitalProcess not found");

            battleTimer.SetTimerText(_timerText);
            resultPanel.Init(_endGameButton, _selfCreatures, _enemyCreatures, _panel, _resultText);
            battleView.Init(_creatureBattleIcons, _fightOperationsBar, _currentCreatureBattleIcon);
            battleController.SetBattleSceneObjectsParent(_battleSceneObjectsParent);
            battleController.InitInBattle(_camera, _creatureStatsPanel);
            battleInitalProcess.SetPositions(_selfPlayerPosition, _enemyPlayerPosition);
            battleInitalProcess.LoadBattleObjects();
        }
    }
}