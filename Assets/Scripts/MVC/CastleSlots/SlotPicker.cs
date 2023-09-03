using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.MVC.CastleSlots
{
    public class SlotPicker : MonoBehaviour
    {
        [SerializeField] private LayerMask _slotLayerMask;


        public bool TryPickSlot<T>(out T slot)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("111");
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = Input.mousePosition;
                List<RaycastResult> raysastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, raysastResults);
                foreach(var item in raysastResults)
                {
                    if (item.gameObject.TryGetComponent(out slot))
                    {
                        Debug.Log("121");
                        return true;
                    }
                }
            }
            slot = default;
            return false;
        }

    }
}