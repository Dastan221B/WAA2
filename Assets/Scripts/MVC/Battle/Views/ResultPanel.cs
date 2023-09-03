using Assets.Scripts.GameResources.MapCreatures;
using Assets.Scripts.MVC.Battle.BattleProcess;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

namespace Assets.Scripts.MVC.Battle.Views
{
    public class ResultPanel : MonoBehaviour
    {
        private TMP_Text _resultText;
        private Button _endGameButton;
        private Transform _playerCreatures;
        private Transform _enemyCreatures;
        private GameObject _panel;

        private ResultPanelCreatureItem _resultPanelCreatureItemPrefab;
        private ModelCreatures _modelCreatures;
        private List<GameObject> _creatuersIcons = new List<GameObject>();
        private EndGameProcess _endGameProcess;
        private BattleModel _battleModel;

        public void Init(BattleModel battleModel,EndGameProcess endGameProcess , ResultPanelCreatureItem resultPanelCreatureItemPrefab , ModelCreatures modelCreatures)
        {
            _battleModel = battleModel;
            _modelCreatures = modelCreatures;
            _resultPanelCreatureItemPrefab = resultPanelCreatureItemPrefab;
            _endGameProcess = endGameProcess;
        }

        public void Init(Button endGameButton, Transform playerCreatures, Transform enemyCreatures, GameObject panel, TMP_Text resultText)
        {
            _resultText = resultText;
            _endGameButton = endGameButton;
            _playerCreatures = playerCreatures;
            _enemyCreatures = enemyCreatures;
            _panel = panel;
            _endGameButton.onClick.AddListener(EndGame);
        }

        public void EndGame()
        {
            foreach (var item in _creatuersIcons)
                Destroy(item);
            _creatuersIcons.Clear();
            _endGameProcess.EndGame();
            ClosePanel();
        }

        public void SetPlayerCreatures(CreatureModelObject creatureModelObjects)
        {
            SetCreatureOnPanel(creatureModelObjects, _playerCreatures);

        }

        public void SetEnemyCreatures(CreatureModelObject creatureModelObjects)
        {
            SetCreatureOnPanel(creatureModelObjects, _enemyCreatures);
        }

        public void OpenPanel(bool isWin)
        {
            if (isWin)
            {
                _resultText.text = "You win!";
            }
            else
            {
                _resultText.text = "You loose!";
            }

            foreach (var creature in _battleModel.DeathCreatures)
            {
                if (creature.CreatureSide == CreatureSide.Self)
                    SetPlayerCreatures(creature);
                else
                    SetEnemyCreatures(creature);
            }

            _panel.gameObject.SetActive(true);
        }

        public void ClosePanel()
        {
            _panel.gameObject.SetActive(false);
        }

        private void SetCreatureOnPanel(CreatureModelObject creatureModelObject, Transform parent)
        {
            var creatureItem = Instantiate(_resultPanelCreatureItemPrefab, parent);
            creatureItem.Init(_modelCreatures.GetIconById((int)creatureModelObject.SpriteID - 1), creatureModelObject.CreatureInfo.stackSlot - 1);
            _creatuersIcons.Add(creatureItem.gameObject);
        }

    }
}