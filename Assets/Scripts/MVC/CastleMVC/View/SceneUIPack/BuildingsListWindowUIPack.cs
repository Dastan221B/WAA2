using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MVC.CastleMVC.View.SceneUIPack
{
    public class BuildingsListWindowUIPack : MonoBehaviour
    {
        [SerializeField] private Button _close;
        [SerializeField] private GameObject _panel;
        [SerializeField] private BuildingSlot[] _buildingSlots;


        public Button Close => _close;
        public GameObject Panel => _panel;
        public BuildingSlot[] BuildingSlots => _buildingSlots;


    }
}