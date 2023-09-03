using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MinesCune", menuName = "ScriptableObjects/MinesCune")]
public class MinesCube : ScriptableObject
{
    [SerializeField] private List<Color> m_Color;

    public Color GetColorById(int id)
    {
        return m_Color[id];
    }
}
