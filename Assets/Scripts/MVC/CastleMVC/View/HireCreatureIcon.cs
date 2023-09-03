using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MVC.CastleMVC.View
{
    public class HireCreatureIcon : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _icon;

        public DicCreatureDTO CreatureDTO { get; private set; }

        public event System.Action<HireCreatureIcon> OnSelected;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnSelected?.Invoke(this);
        }

        public void SetCreature(DicCreatureDTO dicCreatureDTO, Sprite sprite)
        {
            CreatureDTO = dicCreatureDTO;
            _icon.sprite = sprite;
        }

        public void Select()
        {
            _icon.color = Color.white;
        }

        public void Unselect()
        {
            _icon.color = Color.grey;
        }
    }
}