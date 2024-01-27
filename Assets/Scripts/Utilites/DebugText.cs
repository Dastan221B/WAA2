using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Utilites
{
    public class DebugText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        public string TextValue { get; private set; }  

        public void SetText(string text)
        {
            _text.text = text;
            TextValue = text;
        }
    }
}