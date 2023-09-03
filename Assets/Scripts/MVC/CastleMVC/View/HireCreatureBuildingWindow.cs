using Assets.Scripts.MVC.CastleSlots;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Scripts.MVC.CastleMVC.View.SceneUIPack;

namespace Assets.Scripts.MVC.CastleMVC.View
{
    public class HireCreatureBuildingWindow : BuildingWindow
    {
        private GameObject _panel;

        private Button _closeButton;
        private Button _hireCreaturesButton;
        private Button _setMaxAmountButton;
        private Button _increaseCreatureButton;
        private Button _decreaseOneCreatureButton;

        private TMP_Text _haveCreaturesInInvetory;
        private TMP_Text _priceForCreatureText;
        private TMP_Text _creaturesNameText;
        private TMP_Text _totalPriceText;
        private TMP_Text _availableCreaturesAmountText;
        private TMP_Text _selectedCreaturesCountText;
        private HireCreatureIcon[] _hireCreatureIcons;
        //[SerializeField] private Slider _creaturesCountSlider;

        private ProgramState _programState;
        private CastleCommandsSender _castleCommandsSender;
        private CastleModel _castleModel;
        private DicCreatureDTO _currentCreatureDTO;
        private ModelCreatures _modelCreatures;
        private HireCreatureIcon _currentHireCreatureIcon;
        private SlotsModel _slotsModel;

        private int _priceForCreature;
        private int _selectedCreaturesAmount;
        private int _creaturesMaxCount;
        private List<DicCreatureDTO> _dicCreatures;

        public void Init(HireCreatureBuildingWindowUIPack hireCreatureBuildingWindowUIPack)
        {
            _panel = hireCreatureBuildingWindowUIPack.Panel;
            _closeButton = hireCreatureBuildingWindowUIPack.CloseButton;
            _hireCreaturesButton = hireCreatureBuildingWindowUIPack.HireCreaturesButton;
            _setMaxAmountButton = hireCreatureBuildingWindowUIPack.SetMaxAmountButton;
            _increaseCreatureButton = hireCreatureBuildingWindowUIPack.IncreaseCreatureButton;
            _decreaseOneCreatureButton = hireCreatureBuildingWindowUIPack.DecreaseOneCreatureButton;

            _haveCreaturesInInvetory = hireCreatureBuildingWindowUIPack.HaveCreaturesInInvetory;
            _priceForCreatureText = hireCreatureBuildingWindowUIPack.PriceForCreatureText;
            _creaturesNameText = hireCreatureBuildingWindowUIPack.CreaturesNameText;
            _totalPriceText = hireCreatureBuildingWindowUIPack.TotalPriceText;
            _availableCreaturesAmountText = hireCreatureBuildingWindowUIPack.AvailableCreaturesAmountText;
            _selectedCreaturesCountText = hireCreatureBuildingWindowUIPack.SelectedCreaturesCountText;
            _hireCreatureIcons = hireCreatureBuildingWindowUIPack.HireCreatureIcon;

            _closeButton.onClick.AddListener(() => Close());
            _hireCreaturesButton.onClick.AddListener(() => Hire());
            _setMaxAmountButton.onClick.AddListener(() => SetMaxCreatures());
            _increaseCreatureButton.onClick.AddListener(() => IncreaseCreatureHireCount());
            _decreaseOneCreatureButton.onClick.AddListener(() => DecreaseCreatureHireCount());

            foreach (var item in _hireCreatureIcons)
                item.OnSelected += SelectedCreature;
        }

        public void Init(ProgramState programState,CastleCommandsSender castleCommandsSender, CastleModel castleModel, ModelCreatures modelCreatures, SlotsModel slotsModel)
        {
            _programState = programState;
            _castleCommandsSender = castleCommandsSender;
            _castleModel = castleModel;
            _modelCreatures = modelCreatures;
            _slotsModel = slotsModel;
        }

        public void Init(List<DicCreatureDTO> dicCreatures)
        {
            if (dicCreatures.Count == 0)
                throw new System.Exception("creatures list is empty");

            _dicCreatures = dicCreatures;

            for (int i = 0; i < _hireCreatureIcons.Length; i++)
            {
                if (i < _dicCreatures.Count)
                {
                    _hireCreatureIcons[i].gameObject.SetActive(true);
 
                    _hireCreatureIcons[i].SetCreature(_dicCreatures[i], _modelCreatures.GetIconById((int)_dicCreatures[i].id - 1));
                }
                else
                {
                    _hireCreatureIcons[i].gameObject.SetActive(false);
                }
            }
            _currentCreatureDTO = _dicCreatures[0];
            _creaturesNameText.text = _currentCreatureDTO.name;
            _selectedCreaturesAmount = 1;
            _currentHireCreatureIcon = _hireCreatureIcons[0];
            SetupPriceForCurrentCreature();
            SetupCreaturesMaxCount();
        }


        public void SelectedCreature(HireCreatureIcon hireCreatureIcon)
        {
            if (_currentHireCreatureIcon != null)
                _currentHireCreatureIcon.Unselect();
            _currentCreatureDTO = hireCreatureIcon.CreatureDTO;
            _currentHireCreatureIcon = hireCreatureIcon;
            _currentHireCreatureIcon.Select();
            SetupPriceForCurrentCreature();
            SetupCreaturesMaxCount();
            _creaturesNameText.text = _currentCreatureDTO.name;
        }

        //private void OnSliderValueChanged(float value)
        //{
        //    if (Mathf.RoundToInt(value * (float)_creaturesMaxCount) == _selectedCreaturesAmount)
        //        return;

        //    _selectedCreaturesAmount = Mathf.RoundToInt(value * (float)_creaturesMaxCount);

        //    _creaturesCountSlider.value = (float)_selectedCreaturesAmount / (float)_creaturesMaxCount;
        //    _totalPriceText.text = (_selectedCreaturesAmount * _priceForCreature).ToString();
        //    _availableCreaturesAmountText.text = (_creaturesMaxCount - _selectedCreaturesAmount).ToString();
        //    _selectedCreaturesCountText.text = _selectedCreaturesAmount.ToString();
        //}

        public void Hire()
        {
            _castleCommandsSender.SendHireCastleCreatureRequest(_castleModel.CurrentCastleID, (int)_currentCreatureDTO.id, _selectedCreaturesAmount,
                new List<ArmySlotInfo>(_slotsModel.CastleArmy).ExcludeNull(), new List<ArmySlotInfo>(_slotsModel.GarrisonArmy).ExcludeNull());
            Close();
        }

        public void SetMaxCreatures()
        {
            _selectedCreaturesAmount = _creaturesMaxCount;
            UpdateUI();
            //_creaturesCountSlider.value = _creaturesCountSlider.maxValue;
        }

        public void IncreaseCreatureHireCount()
        {
            if (_selectedCreaturesAmount >= _creaturesMaxCount)
                return;

            _selectedCreaturesAmount++;
            UpdateUI();
            //_creaturesCountSlider.value = _creaturesCountSlider.maxValue * koef;
        }

        public void DecreaseCreatureHireCount()
        {
            if (_selectedCreaturesAmount <= 0)
                return;
            _selectedCreaturesAmount--;
            UpdateUI();
            //_creaturesCountSlider.value = _creaturesCountSlider.maxValue * koef;
        }

        private void UpdateUI()
        {
            _totalPriceText.text = (_selectedCreaturesAmount * _priceForCreature).ToString();
            _availableCreaturesAmountText.text = (_creaturesMaxCount - _selectedCreaturesAmount).ToString();
            _selectedCreaturesCountText.text = _selectedCreaturesAmount.ToString();
            if (_slotsModel.TryGetArmyInSlots((int)_currentCreatureDTO.id, out ArmySlotInfo armySlotInfo))
                _haveCreaturesInInvetory.text = armySlotInfo.amount.ToString();
        }


        private void SetupCreaturesMaxCount()
        {
            if (_castleModel.CurrentCastleFullOjbectInfo.purchasableCreatureInfoMap.TryGetValue(_currentCreatureDTO.level, out PurchaseableCreatureInfo creaturesAmount))
            {
                _creaturesMaxCount = creaturesAmount;
            }
        }

        private void SetupPriceForCurrentCreature()
        {
            _priceForCreature = _currentCreatureDTO.price.FirstOrDefault(p => p.id == (int)ResourceTypes.GOLD)?.amount ?? default;
            _priceForCreatureText.text = _priceForCreature.ToString();
        }

        public override void Open()
        {
            _programState.CastleUIWindowsStartHandler();
            UpdateUI();
            _panel.SetActive(true);
        }

        public override void Close()
        {
            _programState.CastleStartHandler();
            _panel.SetActive(false);
        }
    }
}