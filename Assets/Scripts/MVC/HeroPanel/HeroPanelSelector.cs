using Assets.Scripts.MVC.Game.Views.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.MVC.HeroPanel
{
    public class HeroPanelSelector : MonoBehaviour
    {

        public bool TryPickHeroSlot(out HeroModelObjectIcon heroModelObjectIcon)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = Input.mousePosition;
                List<RaycastResult> raysastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, raysastResults);
                foreach (var item in raysastResults)
                {
                    if (item.gameObject.TryGetComponent(out heroModelObjectIcon))
                    {
                        return true;
                    }
                }
            }
            heroModelObjectIcon = null;
            return false;
        }

        public bool TryPickSlot(out CreatureSlotHeroPanel slot)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = Input.mousePosition;
                List<RaycastResult> raysastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, raysastResults);
                foreach (var item in raysastResults)
                {
                    if (item.gameObject.TryGetComponent(out slot))
                    {
                        return true;
                    }
                }
            }
            slot = null;
            return false;
        }
    }
}