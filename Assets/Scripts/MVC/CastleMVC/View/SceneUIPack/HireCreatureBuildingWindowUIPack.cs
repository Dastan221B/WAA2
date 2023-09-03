using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MVC.CastleMVC.View.SceneUIPack
{
    public class HireCreatureBuildingWindowUIPack : MonoBehaviour
    {

        [SerializeField] private GameObject _panel;

        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _hireCreaturesButton;
        [SerializeField] private Button _setMaxAmountButton;
        [SerializeField] private Button _increaseCreatureButton;
        [SerializeField] private Button _decreaseOneCreatureButton;

        [SerializeField] private TMP_Text _haveCreaturesInInvetory;
        [SerializeField] private TMP_Text _priceForCreatureText;
        [SerializeField] private TMP_Text _creaturesNameText;
        [SerializeField] private TMP_Text _totalPriceText;
        [SerializeField] private TMP_Text _availableCreaturesAmountText;
        [SerializeField] private TMP_Text _selectedCreaturesCountText;
        [SerializeField] private HireCreatureIcon[] _hireCreatureIcon;


        public GameObject Panel => _panel;

        public Button CloseButton => _closeButton;
        public Button HireCreaturesButton => _hireCreaturesButton;
        public Button SetMaxAmountButton => _setMaxAmountButton;
        public Button IncreaseCreatureButton => _increaseCreatureButton;
        public Button DecreaseOneCreatureButton => _decreaseOneCreatureButton;

        public TMP_Text HaveCreaturesInInvetory => _haveCreaturesInInvetory;
        public TMP_Text PriceForCreatureText => _priceForCreatureText;
        public TMP_Text CreaturesNameText => _creaturesNameText;
        public TMP_Text TotalPriceText => _totalPriceText;
        public TMP_Text AvailableCreaturesAmountText => _availableCreaturesAmountText;
        public TMP_Text SelectedCreaturesCountText => _selectedCreaturesCountText;
        public HireCreatureIcon[] HireCreatureIcon => _hireCreatureIcon;
    }
}