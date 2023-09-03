using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.MVC.Game.Views.UI
{
    public class GameDateUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _dateText;


        public void SetDate(int days , int weeks, int months)
        {
            _dateText.text = $"M : {months}, W : {weeks}, D : {days}";
        }

    }
}