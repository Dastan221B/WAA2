using UnityEngine;

public enum MapType
{
    Noise,
    Color
}

public class NoiseMap : MonoBehaviour
{
    [SerializeField] private Terrain _terrain;
    [SerializeField] private NoiseMapRenderer _noiseMapRenderer;
    [SerializeField] private NoiseMapGenerator _noiseMapGenerator;
    // Исходные данные для нашего генератора шума
    [SerializeField] private float _heightFactor;
    [SerializeField] public int _width;
    [SerializeField] public int _height;
    [SerializeField] public float scale;

    [SerializeField] public int _octaves;
    [SerializeField] public float _persistence;
    [SerializeField] public float _lacunarity;

    [SerializeField] public int _seed;
    [SerializeField] public Vector2 _offset;

    [SerializeField] public MapType type = MapType.Noise;
    private int _previousSeed;
    private void Start()
    {
        _previousSeed = _seed;
        GenerateMap();
    }

    public void GenerateMap()
    {
        // Генерируем карту
        float[,] noiseMap = _noiseMapGenerator.GenerateNoiseMap(_width, _height, _seed, scale, _octaves, _persistence, _lacunarity, _offset, _heightFactor);

        // Передаём карту в рендерер
        //_noiseMapRenderer.RenderMap(_width, _height, noiseMap, type);
        _terrain.terrainData.SetHeights(0,0,noiseMap);
        //_terrain.terrainData.SetAlphamaps(0, 0, _noiseMapRenderer.GetCollorMap(noiseMap, _terrain.terrainData));
        _previousSeed = _seed;
    }
}