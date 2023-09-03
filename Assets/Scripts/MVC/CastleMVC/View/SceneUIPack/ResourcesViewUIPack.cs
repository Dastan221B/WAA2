using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.MVC.CastleMVC.View.SceneUIPack
{
    [ExecuteInEditMode]
    public class ResourcesViewUIPack : MonoBehaviour
    {
        [SerializeField] private TMP_Text _wood;
        [SerializeField] private TMP_Text _mercury;
        [SerializeField] private TMP_Text _ore;
        [SerializeField] private TMP_Text _sulfur;
        [SerializeField] private TMP_Text _crystal;
        [SerializeField] private TMP_Text _gem;
        [SerializeField] private TMP_Text _gold;

        public TMP_Text Wood => _wood;
        public TMP_Text Mercury => _mercury;
        public TMP_Text Ore => _ore;
        public TMP_Text Sulfur => _sulfur;
        public TMP_Text Crystal => _crystal;
        public TMP_Text Gem => _gem;
        public TMP_Text Gold => _gold;


        private void OnEnable()
        {
            _wood.gameObject.name = "WoodResource";
            _mercury.gameObject.name = "MercuryResource";
            _ore.gameObject.name = "OreResource";
            _sulfur.gameObject.name = "SulfurResource";
            _crystal.gameObject.name = "CrystalResource";
            _gem.gameObject.name = "GemResource";
            _gold.gameObject.name = "GoldResource";
        }

    }
}