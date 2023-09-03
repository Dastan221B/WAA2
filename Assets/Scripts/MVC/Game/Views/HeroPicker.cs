using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.MVC.Game.Views
{
    public class HeroPicker : MonoBehaviour
    {
        [SerializeField] private LayerMask _iconLayer;
        private ProgramState _programState;

        public void Init(ProgramState programState)
        {
            _programState = programState;
        }

        public bool TryPickHero(out HeroModelObject heroModelObject)
        {
            if (_programState.StatesOfProgram != StatesOfProgram.Game)
            {
                heroModelObject = null;
                return false;
            }

            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            foreach(var item in results)
            {
                if (item.gameObject.TryGetComponent(out IHeroPickable heroPickable))
                {
                    heroPickable.PickHero(out heroModelObject);
                    return true;
                }
            }
            heroModelObject = null;
            return false;
        
        }
       
    }
}