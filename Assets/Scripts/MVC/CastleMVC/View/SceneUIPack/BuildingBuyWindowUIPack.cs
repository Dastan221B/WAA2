using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MVC.CastleMVC.View.SceneUIPack
{
    public class BuildingBuyWindowUIPack : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _confirm;
        [SerializeField] private Button _cancel;
        [SerializeField] private TMP_Text _buildingDependencies;
        [SerializeField] private TMP_Text _buildingName;
        [SerializeField] private Image _buildingImg;
        [SerializeField] private BuildingBuyWindowPriceItem[] _buildingPriceResources;

        public GameObject Panel => _panel;
        public Button Confirm => _confirm;
        public Button Cancel => _cancel;
        public TMP_Text BuildingDependencies => _buildingDependencies;
        public TMP_Text BuildingName => _buildingName;
        public Image BuildingImg => _buildingImg;
        public BuildingBuyWindowPriceItem[] BuildingPriceResources => _buildingPriceResources;
    }
}