using Assets.Scripts.GameResources.MapCreatures;
using Assets.Scripts.MVC.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.MVC.Battle.Views
{
    public class BattleView : MonoBehaviour
    {
        private CreatureBattleIcon _currentCreatureBattleIcon;
        private ModelCreatures _modelCreatures;
        private GameAndBattleCommandsSender _gameAndBattleCommandsSender;

        private List<CreatureBattleIcon> _creatureBattleIcons;


        public void Init(ModelCreatures modelCreatures, GameAndBattleCommandsSender gameAndBattleCommandsSender)
        {
            _modelCreatures = modelCreatures;
            _gameAndBattleCommandsSender = gameAndBattleCommandsSender;
        }

        public void Init(List<CreatureBattleIcon> creatureBattleIcons , FightOperationsBar fightOperationsBar, CreatureBattleIcon currentCreatureBattleIcon)
        {
            _creatureBattleIcons = creatureBattleIcons;
            _currentCreatureBattleIcon = currentCreatureBattleIcon;
            fightOperationsBar.SubscribeBlockAction(new UnityAction(_gameAndBattleCommandsSender.SendCreatureBlockActivationRequest));
            fightOperationsBar.SubscribeWaitAction(new UnityAction(_gameAndBattleCommandsSender.SendCreatureWaitActivationRequest));
        }

        public void SetCurrentAcriveCreature(CreatureModelObject creatureModelObject, Sprite sprite)
        {
            _currentCreatureBattleIcon.SetCreatureModelObject(sprite , creatureModelObject);
        }

        public void InitBattle(List<CreatureModelObject> creatureModelObjects)
        {
            for(int i = 0; i < _creatureBattleIcons.Count; i++)
            {
                if(i < creatureModelObjects.Count && _creatureBattleIcons[i] != null && _creatureBattleIcons[i].gameObject != null)
                {
                    _creatureBattleIcons[i].SetCreatureModelObject(_modelCreatures.GetIconById(creatureModelObjects[i].SpriteID - 1), creatureModelObjects[i]);
                }
                else if(_creatureBattleIcons[i] != null && _creatureBattleIcons[i].gameObject != null)
                {
                    _creatureBattleIcons[i].gameObject.SetActive(false);
                }
            }
        }

    }
}