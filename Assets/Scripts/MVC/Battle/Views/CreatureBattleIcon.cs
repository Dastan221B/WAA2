using Assets.Scripts.GameResources.MapCreatures;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MVC.Battle.Views
{
    public class CreatureBattleIcon : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _colorImage;
        public CreatureModelObject CreatureModelObject { get; private set; }


        public void SetCreatureModelObject(Sprite icon , CreatureModelObject creatureModelObject)
        {
            CreatureModelObject = creatureModelObject;

            if(_colorImage != null)
            {
                if (CreatureModelObject.CreatureSide == CreatureSide.Self)
                {
                    _colorImage.color = Color.red;
                }
                else
                {
                    _colorImage.color = Color.blue;
                }
            }

            _icon.sprite = icon;
            _icon.color = Color.white;
        }

    }
}