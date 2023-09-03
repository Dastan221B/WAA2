using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiamondGeneration : MonoBehaviour
{
    [SerializeField] private Terrain _terrain;

    [SerializeField] private int _terrainPower;
    [SerializeField] private int _size;
    [SerializeField] private int _max;
    [SerializeField] private float[,] _map;

    [SerializeField] private float _outsideHeight;
    [SerializeField] private float _roughness;
    [SerializeField] private bool _raiseToSecondPower = true;
    [SerializeField] private bool _normalize = true;
    [SerializeField] private float _baseHeight = 100f;
    [SerializeField] private int _smoothCycles = 1;

    private bool _inited;

    public void GenerateTerrain()
    {
        _map = new float[_size, _size];

        InitHeights();
        AddRandomToAngles();
        _terrain.terrainData.SetHeights(0, 0, Generate());
    }


    public float[,] Generate()
    {
        GenerateDiamondSquareTerrain();
        return _map;
    }

    private void GenerateDiamondSquareTerrain()
    {
        for (var i = 0; i < _size; i++)
        {
            for (var j = 0; j < _size; j++)
            {
                _map[i, j] = GetHeight(i, j);
            }
        }

        if (_raiseToSecondPower)
        {
            RaiseToSecondPower(0, 0, _size);
        }

        for (var i = 0; i < _smoothCycles; i++)
        {
            PostProcess();
        }
        Normalize(0, 0, _size);
    }

    private void InitHeights()
    {
        for (var x = 0; x < _size; x++)
        {
            for (var z = 0; z < _size; z++)
            {
                _map[x, z] = float.MinValue;
            }
        }
    }

    private void AddRandomToAngles()
    {
        var initVal = _max;
        _map[0, 0] = UnityEngine.Random.Range(-initVal, initVal);
        _map[0, _max] = UnityEngine.Random.Range(-initVal, initVal);
        _map[_max, 0] = UnityEngine.Random.Range(-initVal, initVal);
        _map[_max, _max] = UnityEngine.Random.Range(-initVal, initVal);
    }

    private float GetHeight(int x, int y)
    {
        if (x < 0 || x > _max || y < 0 || y > _max)
        {
            return _outsideHeight;
        }
        if (_map[x, y] != float.MinValue)
        {
            return _map[x, y];
        }

        var baseSize = 1;
        while (((x & baseSize) == 0) && ((y & baseSize) == 0))
        {
            baseSize <<= 1;
        }
        if (((x & baseSize) != 0) && ((y & baseSize) != 0))
        {
            return CountOneSquare(x, y, baseSize * 2);
        }
        else
        {
            return CountOneDiamond(x, y, baseSize * 2);
        }
    }

    private float CountOneSquare(int x, int y, int currMax)
    {
        _map[x, y] = Displace((GetHeight(x - currMax / 2, y - currMax / 2) +
                     GetHeight(x + currMax / 2, y - currMax / 2) +
                     GetHeight(x + currMax / 2, y + currMax / 2) +
                     GetHeight(x - currMax / 2, y + currMax / 2)) / 4,
                            _roughness, currMax);

        return _map[x, y];
    }

    private float CountOneDiamond(int x, int y, int currMax)
    {
        var halfSize = currMax / 2;

        _map[x, y] = Displace((GetHeight(x, y - halfSize) +
                     GetHeight(x + halfSize, y) +
                     GetHeight(x, y + halfSize) +
                     GetHeight(x - halfSize, y)) / 4,
                            _roughness, currMax);

        return _map[x, y];
    }

    private float Displace(float val, float roughness, int currMax)
    {
        var diff = Math.Max(0.5f, Math.Min(val / _baseHeight, 1f));
        var rnd = (UnityEngine.Random.Range(0f, 1f) * 2f - 1f);
        return val + rnd * roughness * (float)currMax;
    }

    private void RaiseToSecondPower(int x, int z, int size)
    {
        for (var i = x; i < x + size; i++)
        {
            for (var j = z; j < z + size; j++)
            {
                var val = Mathf.Pow(_map[i, j], 2);
                _map[i, j] = val;
            }
        }
    }

    private void Normalize(int x, int z, int size)
    {
        var max = _map[x, z];

        for (var i = x; i < x + size; i++)
        {
            for (var j = z; j < z + size; j++)
            {
                if (_map[i, j] > max)
                {
                    max = _map[i, j];
                }
            }
        }
        if (max == float.MinValue) return;

        for (var i = x; i < x + size; i++)
        {
            for (var j = z; j < z + size; j++)
            {
                _map[i, j] /= max;
            }
        }
    }

    private void PostProcess()
    {
        var newMap = new float[_size, _size];
        Array.Copy(_map, 0, newMap, 0, _map.Length);
        for (var x = 0; x < _size; x++)
        {
            for (var z = 0; z < _size; z++)
            {
                var h1 = GetHeight(x - 1, z - 1);
                var h2 = GetHeight(x - 1, z);
                var h3 = GetHeight(x - 1, z + 1);
                var h4 = GetHeight(x, z - 1);
                var h5 = GetHeight(x + 1, z - 1);
                var h6 = GetHeight(x + 1, z);
                var h7 = GetHeight(x + 1, z + 1);
                var h8 = GetHeight(x, z + 1);
                var heights = new List<float>() { h1, h2, h3, h4, h5, h6, h7, h8 };
                var valuablePoints = heights.Where(x => x != _outsideHeight);
                newMap[x, z] = valuablePoints.Sum() / valuablePoints.Count();
            }
        }
        _map = newMap;
    }
}
