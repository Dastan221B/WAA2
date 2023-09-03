using System.Collections;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.MVC.CastleMVC.View
{
    public class BuildingBuyWindowPriceItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _priceText;
        public int ResourceId { get; private set; }        


        public void SetResouceID(int id)
        {
            ResourceId = id;
        }

        public void SetPrice(int price)
        {
            _priceText.text = price.ToString();
        }

    }

}