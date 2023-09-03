using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.CastleMVC.Buildinngs
{
    public class Building : MonoBehaviour
    {
        [SerializeField] private BuildingType _buildingType;
        [field: SerializeField] public int Id { get; private set; }
        public BuildingType BuildingType => _buildingType;

        public void SetID(int id)
        {
            Id = id;
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

    }
}