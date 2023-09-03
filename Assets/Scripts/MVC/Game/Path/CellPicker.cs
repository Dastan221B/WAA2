using System.Collections;
using UnityEngine;

public class CellPicker : MonoBehaviour
{
    [SerializeField] private LayerMask _cellLayerMask;


    public bool TryPickCell(out Cell cell)
    {
        //var groundPlane = new Plane(Vector3.up, Vector3.zero);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if (groundPlane.Raycast(ray, out float position))
        //{
        //    Vector3 worldPosition = ray.GetPoint(position);

        //}
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _cellLayerMask))
        {
            if (hit.transform != null && hit.transform.TryGetComponent(out cell))
            {
                return true;
            }
        }
        cell = null;
        return false;
    }

}