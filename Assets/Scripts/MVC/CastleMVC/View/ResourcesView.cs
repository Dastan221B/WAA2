using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Assets.Scripts.GameResources;
using Assets.Scripts.MVC.CastleMVC.View.SceneUIPack;

namespace Assets.Scripts.MVC.CastleMVC.View
{
    public class ResourcesView : MonoBehaviour
    {
        private TMP_Text _wood; 
        private TMP_Text _mercury; // берюзовый
        private TMP_Text _ore; // уголь
        private TMP_Text _sulfur; // зеленый
        private TMP_Text _crystal; // красный 
        private TMP_Text _gem; // Синий
        private TMP_Text _gold; // золото

        private ResourcesDataService _resourcesDataService;

        public void Init(ResourcesDataService resourcesDataService)
        {
            _resourcesDataService = resourcesDataService;
        }

        public void Init(ResourcesViewUIPack resourcesViewUIPack)
        {
            _wood = resourcesViewUIPack.Wood;
            _mercury = resourcesViewUIPack.Mercury;
            _ore = resourcesViewUIPack.Ore;
            _sulfur = resourcesViewUIPack.Sulfur;
            _crystal = resourcesViewUIPack.Crystal;
            _gem = resourcesViewUIPack.Gem;
            _gold = resourcesViewUIPack.Gold;
            UpdateResources();
        }

        public void UpdateResources()
        {
            if (_wood == null)
                return;

            _wood.text = _resourcesDataService.GetResoucesValueByType(ResourceTypes.WOOD).ToString();
            _mercury.text = _resourcesDataService.GetResoucesValueByType(ResourceTypes.MERCURY).ToString();
            _ore.text = _resourcesDataService.GetResoucesValueByType(ResourceTypes.ORE).ToString();
            _sulfur.text = _resourcesDataService.GetResoucesValueByType(ResourceTypes.SULFUR).ToString(); 
            _crystal.text = _resourcesDataService.GetResoucesValueByType(ResourceTypes.CRYSTAL).ToString();
            _gem.text = _resourcesDataService.GetResoucesValueByType(ResourceTypes.GEM).ToString();
            _gold.text = _resourcesDataService.GetResoucesValueByType(ResourceTypes.GOLD).ToString();
        }
    }
}