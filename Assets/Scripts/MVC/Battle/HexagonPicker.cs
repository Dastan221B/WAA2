using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.Battle
{
    public class HexagonPicker : MonoBehaviour
    {
        private Camera _camera;
        [SerializeField] private LayerMask _hexagonLayerMask;

        public void SetCamera(Camera camera)
        {
            _camera = camera;
        }

        public bool TryPickHexagon(out Hexagon hexagon)
        {
            if (_camera == null)
            {
                hexagon = null;
                return false;
            }
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _hexagonLayerMask))
            {
                if (hit.transform != null && hit.transform.TryGetComponent(out hexagon))
                {
                    return true;
                }
            }
            hexagon = null;
            return false;
        }
    }
}