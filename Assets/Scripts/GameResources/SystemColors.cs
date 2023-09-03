using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameResources
{
    [CreateAssetMenu(fileName = "SystemColors" , menuName = "ScriptableObjects/SystemColors")]
    public class SystemColors : ScriptableObject
    {
        [SerializeField] private Color[] _colors;

        public Color GetColorByOrdinal(int ordinal)
        {
            if (ordinal >_colors.Length  && ordinal < 0)
                return Color.white;
            return _colors[ordinal];
        }
    }
}