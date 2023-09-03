using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MVC.Game.Views.UI
{
    public class HeroModelObjectIcon : MonoBehaviour, IHeroPickable
    {
        [SerializeField] private GameObject _frame;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _movePointsBar;

        public HeroModelObject HeroModelObject { get; private set; }
        public Sprite Icon => _icon.sprite;
        private float _maxMovePoints = 1600;

        public void OnFrame()
        {
            _frame.SetActive(true);
        }

        public void OffFrame()
        {
            _frame.SetActive(false);
        }

        private void DisplayMovePoints(int movePoints)
        {
            _movePointsBar.fillAmount = (float)movePoints / _maxMovePoints;
        }

        private void SetMaxMovePoints(int movePoints)
        {
            _maxMovePoints = movePoints;
        }

        public void SetHeroModelObject(HeroModelObject heroModelObject)
        {
            _movePointsBar.gameObject.SetActive(true);
            if (HeroModelObject != null)
            {
                HeroModelObject.OnChangeMovePoints -= DisplayMovePoints;
                HeroModelObject.OnInitedMovePoints -= SetMaxMovePoints;
            }

            _icon.enabled = true;
            _icon.color = Color.white;
            _icon.sprite = heroModelObject.Hero.Icon;
            HeroModelObject = heroModelObject;
            DisplayMovePoints(heroModelObject.MovePointsLeft);
            HeroModelObject.OnInitedMovePoints += SetMaxMovePoints;
            HeroModelObject.OnChangeMovePoints += DisplayMovePoints;
        }
        
        public void SetBaseIcon(Sprite sprite)
        {
            _movePointsBar.gameObject.SetActive(false);
            _icon.sprite = sprite;
            _movePointsBar.fillAmount = 0;
            _icon.color = new Color(0, 0, 0, 0);
        }

        public void PickHero(out HeroModelObject heroModelObject)
        {
            heroModelObject = HeroModelObject;
        }
    }
}