using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.MVC.Ground
{
    public class TerrainGenerator : MonoBehaviour
    {
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

        public float[,] GenerateNoiseMap()
        {
            // Массив данных о вершинах, одномерный вид поможет избавиться от лишних циклов впоследствии
            float[,] noiseMap = new float[_width, _height];

            // Порождающий элемент
            System.Random rand = new System.Random(_seed);

            // Сдвиг октав, чтобы при наложении друг на друга получить более интересную картинку
            Vector2[] octavesOffset = new Vector2[_octaves];
            for (int i = 0; i < _octaves; i++)
            {
                // Учитываем внешний сдвиг положения
                float xOffset = rand.Next(-100000, 100000) + _offset.x;
                float yOffset = rand.Next(-100000, 100000) + _offset.y;
                octavesOffset[i] = new Vector2(xOffset / _width, yOffset / _height);
            }

            if (scale < 0)
            {
                scale = 0.0001f;
            }

            // Учитываем половину ширины и высоты, для более визуально приятного изменения масштаба
            float halfWidth = _width / 2f;
            float halfHeight = _height / 2f;

            // Генерируем точки на карте высот
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    // Задаём значения для первой октавы
                    float amplitude = 1;
                    float frequency = 1;
                    float noiseHeight = 0;
                    float superpositionCompensation = 0;

                    // Обработка наложения октав
                    for (int i = 0; i < _octaves; i++)
                    {
                        // Рассчитываем координаты для получения значения из Шума Перлина
                        float xResult = (x - halfWidth) / scale * frequency + octavesOffset[i].x * frequency;
                        float yResult = (y - halfHeight) / scale * frequency + octavesOffset[i].y * frequency;

                        // Получение высоты из ГСПЧ
                        float generatedValue = Mathf.PerlinNoise(xResult, yResult);
                        // Наложение октав
                        noiseHeight += generatedValue * amplitude;
                        // Компенсируем наложение октав, чтобы остаться в границах диапазона [0,1]
                        noiseHeight -= superpositionCompensation;

                        // Расчёт амплитуды, частоты и компенсации для следующей октавы
                        amplitude *= _persistence;
                        frequency *= _lacunarity;
                        superpositionCompensation = amplitude / 2;
                    }

                    // Сохраняем точку для карты высот
                    // Из-за наложения октав есть вероятность выхода за границы диапазона [0,1]
                    noiseMap[y, x] = Mathf.Clamp01(noiseHeight);
                }
            }

            return noiseMap;


            //[SerializeField] private Terrain _terrain;
            //[SerializeField] private int _width = 256;
            //[SerializeField] private int _height = 256;
            //[SerializeField] private float _depth = 10;
            //[SerializeField] private int _octaves = 4;
            //[SerializeField] private float _scale = 50f;
            //[SerializeField] private float _lcunarity = 2f;
            //[Range(0, 1)] [SerializeField] private float _persistance = 0.5f;
            //[SerializeField] private AnimationCurve _heightCurve;
            //[SerializeField] private float _offset = 100f;
            //[SerializeField] private float _falloffDirection = 3f;
            //[SerializeField] private float _falloffRange = 3f;
            //[SerializeField] private bool _useFalloffMap;
            //[SerializeField] private bool _randomize;
            //[SerializeField] private bool _autoUpdate;

            //private void OnValidate()
            //{
            //    if (_width < 1)
            //    {
            //        _width = 1;
            //    }
            //    if (_height < 1)
            //    {
            //        _height = 1;
            //    }
            //    if (_lcunarity < 1)
            //    {
            //        _lcunarity = 1;
            //    }
            //    if (_octaves < 0)
            //    {
            //        _octaves = 0;
            //    }
            //    if (_scale <= 0)
            //    {
            //        _scale = 0.0001f;
            //    }
            //}

            //public float[,] Generate()
            //{
            //    if (_randomize)
            //    {
            //        _offset = UnityEngine.Random.Range(0f, 9999f);
            //    }

            //    TerrainData terrainData = _terrain.terrainData;

            //    terrainData.size = new Vector3(_width, _depth, _height);

            //    float[,] falloff = null;
            //    if (_useFalloffMap)
            //    {
            //        falloff = new FalloffMap
            //        {
            //            FalloffDirection = _falloffDirection,
            //            FalloffRange = _falloffRange,
            //            Size = _width
            //        }.Generate();
            //    }
            //    Debug.Log("Generate");
            //    return GenerateNoise(falloff);
            //}

            //private float[,] GenerateNoise(float[,] falloffMap = null)
            //{
            //    AnimationCurve heightCurve = new AnimationCurve(_heightCurve.keys);

            //    float maxLocalNoiseHeight;
            //    float minLocalNoiseHeight;

            //    float[,] noiseMap = new PerlinMap()
            //    {
            //        Size = _width,
            //        Octaves = _octaves,
            //        Scale = _scale,
            //        Offset = _offset,
            //        Persistance = _persistance,
            //        Lacunarity = _lcunarity
            //    }.Generate(out maxLocalNoiseHeight, out minLocalNoiseHeight);

            //    for (int Y = 0; Y < _height; Y++)
            //    {
            //        for (int X = 0; X < _width; X++)
            //        {
            //            var lerp = Mathf.InverseLerp(minLocalNoiseHeight, maxLocalNoiseHeight, noiseMap[X, Y]);

            //            if (falloffMap != null)
            //            {
            //                lerp -= falloffMap[X, Y];
            //            }

            //            if (lerp >= 0)
            //            {
            //                noiseMap[X, Y] = heightCurve.Evaluate(lerp);
            //            }
            //            else
            //            {
            //                noiseMap[X, Y] = 0;
            //            }
            //        }
            //    }

            //    return noiseMap;
            //}

            //public void Clear()
            //{
            //    TerrainData terrainData = _terrain.terrainData;
            //    terrainData.SetHeights(0, 0, new float[_width, _height]);
            //}
        }
    }
}