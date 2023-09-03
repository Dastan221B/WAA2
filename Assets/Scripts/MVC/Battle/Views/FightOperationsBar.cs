using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.Events;

namespace Assets.Scripts.MVC.Battle.Views
{
    public class FightOperationsBar : MonoBehaviour
    {
        [SerializeField] private Button _waitButton;
        [SerializeField] private Button _blockButton;


        public void SubscribeWaitAction(UnityAction action)
        {
            _waitButton.onClick.AddListener(action);
        }

        public void SubscribeBlockAction(UnityAction action)
        {
            _blockButton.onClick.AddListener(action);
        }
    }
}