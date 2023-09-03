using Assets.Scripts.GameResources.MapCreatures;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.Battle.Views
{
    public class BattleIconCreaturePicker : MonoBehaviour
    {
        private Camera _camera;
        private CreatureModelObject _creatureModelObject;
        [SerializeField] private LayerMask _hexagonLayerMask;

        public void SetCamera(Camera camera)
        {
            _camera = camera;
        }

        //public bool TryPickCreatureFromIcon(out CreatureModelObject creatureModelObject)
        //{
        //    if (_camera == null)
        //    {
        //        creatureModelObject = null;
        //        return false;
        //    }

        //    if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _hexagonLayerMask))
        //    {
        //        if (hit.transform != null && hit.transform.TryGetComponent(out CreatureBattleIcon creatureBattleIcon))
        //        {
        //            creatureModelObject = creatureBattleIcon.CreatureModelObject;
        //            return true;
        //        }
        //    }
        //    creatureModelObject = null;
        //    return false;
        //}
    }
}