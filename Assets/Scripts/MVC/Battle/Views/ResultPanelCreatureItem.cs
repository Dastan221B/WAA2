using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.MVC.Battle.Views
{
    public class ResultPanelCreatureItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _creaturesCount;
        [SerializeField] private Image _image;

        public void Init(Sprite icon, int count)
        {
            _creaturesCount.text = count.ToString();
            _image.sprite = icon;
        }
    }
}