using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Utilites
{
    public class DebugPanel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private DebugText _textPrefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private GameObject _panel;

        private List<DebugText> _textList = new List<DebugText>();  

        private void Awake()
        {
            _inputField.onValueChanged.AddListener(OnInputValueChanged);
            DontDestroyOnLoad(this);
        }

        public void OpenPanel()
        {
            _panel.SetActive(!_panel.activeSelf);    
        }

        private void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        void OnInputValueChanged(string inputText)
        {
            // Проходим по списку объектов и проверяем, соответствует ли каждый объект введенному тексту
            foreach (var obj in _textList)
            {
                if (obj.TextValue.Contains(inputText))
                {
                    obj.gameObject.SetActive(true); // Включаем объект, если он соответствует
                }
                else
                {
                    obj.gameObject.SetActive(false); // Отключаем объект, если он не соответствует
                }
            }
        }

        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            if(type == LogType.Error ||
                type == LogType.Log)
            {
                DebugText text = Instantiate(_textPrefab, _parent);
                _textList.Add(text);
                text.SetText($"Log: {logString}");
            }
            OnInputValueChanged(_inputField.text);
        }
    }
}