using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseGeneration : MonoBehaviour
{
    [SerializeField] private int _widht;
    [SerializeField] private int _height;
    [SerializeField] private Terrain _terrain;
    public float power = 3.0f;
    public float scale = 1.0f;
    private Vector2 startPoint = new Vector2(0f, 0f);


    void MakeNoise()
    {
        var map = new float[_widht, _height];

        for (int x = 0; x < _widht; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                float noise = (Mathf.PerlinNoise(x * scale, y * scale) - 0.5f) * power;
                map[x,y] = noise;  // Задаём высоту для точки с вышеуказанными координатами
            }

        }
        _terrain.terrainData.SetHeights(0, 0, map);
    }


    public void Start()
    {
        MakeNoise();
    }
}
