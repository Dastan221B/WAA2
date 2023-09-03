using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.CastleMVC.View
{
    public abstract class BuildingWindow : MonoBehaviour
    {
        public abstract void Open();
        public abstract void Close();
    }
}