using Assets.Scripts.MVC.CastleMVC.Buildinngs;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.CastleMVC
{
    public class BuildingsPicker : MonoBehaviour
    {
        [SerializeField] private LayerMask _buildingLayerMask;


        public bool TryPickBuilding(out Building building)
        {

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _buildingLayerMask))
            {
                if (hit.transform != null && hit.transform.TryGetComponent(out building))
                {
                    return true;
                }
            }
            building = null;
            return false;
        }
    }
}