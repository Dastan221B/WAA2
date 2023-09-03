using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Scripts.Interfaces.Game;

namespace Assets.Scripts.MVC.Game.Views.UI
{
    public class CastleIcon : MonoBehaviour , ICastlePickable
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _frame;
        [SerializeField] private Image[] _frames;
        private GameAndBattleCommandsSender _gameAndBattleCommandsSender;
     
        [field: SerializeField] public Castle Castle { get; private set; }
        public string ID;

        public void SelectCastle()
        {
            foreach (var item in _frames)
            {
                item.color = Color.white;
            }
            _frame.color = Color.red;
        }

        public void SetCastle(Castle castle)
        {
            _icon.enabled = true;
            _icon.color = Color.white;
            _icon.sprite = castle.CastleIcon;
            Castle = castle;
            ID = castle.MapObjectID;
        }

        public void SetBaseIcon(Sprite sprite)
        {
            _icon.color = new Color(0, 0, 0, 0);
            _icon.sprite = sprite;
        }

        public void PickCastle(out CastleIcon castleIcon)
        {
            castleIcon = this;
        }

    }
}