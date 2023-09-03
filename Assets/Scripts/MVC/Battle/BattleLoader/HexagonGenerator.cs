using Assets.Scripts.Interfaces.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MVC.Battle
{
    public class HexagonGenerator : MonoBehaviour , IClearHexagonFrameList
    {
        public static readonly int HEXAGON_WIDTH = 15, HEXAGON_LENGTH = 11;
        private const float HEXAGON_DELTA_X = 0.9f,
            HEXAGON_DELTA_Y = 0.78f, HEXAGON_START_X = -6f, HEXAGON_START_Y = 3.5f;

        private Hexagon _hexagonPrefab;
        private GameObject _hexagonFrame;
        private List<GameObject> _listHexagonFrames = new List<GameObject>();

        public void ResetHexagonFrameList()
        {
            foreach (var item in _listHexagonFrames)
                Destroy(item);
            _listHexagonFrames.Clear();
        }

        public void Init(Hexagon hexagonPrefab , GameObject hexagonFraame)
        {
            _hexagonPrefab = hexagonPrefab;
            _hexagonFrame = hexagonFraame;
        }

        public Hexagon[,] GenerateHexagonField()
        {
            Hexagon[,] hexagonObjects = new Hexagon[HEXAGON_WIDTH, HEXAGON_LENGTH];

            for (int i = 0; i < HEXAGON_WIDTH; i++)
            {
                for (int j = 0; j < HEXAGON_LENGTH; j++)
                {
                    float x;
                    if (j % 2 == 0)
                    {
                        x = i * HEXAGON_DELTA_X;
                    }
                    else
                    {
                        x = i * HEXAGON_DELTA_X + HEXAGON_DELTA_X / 2;
                    }
                    Hexagon newHexagon = Instantiate(_hexagonPrefab, new Vector3(x + HEXAGON_START_X,0, -j * HEXAGON_DELTA_Y + HEXAGON_START_Y), _hexagonPrefab.transform.rotation);
                    
                    newHexagon.SetBattleFieldCoordinates(new BattleFieldCoordinates(i, j));
                    hexagonObjects[i, j] = newHexagon;
                    _listHexagonFrames.Add(Instantiate(_hexagonFrame, new Vector3(x + HEXAGON_START_X, 0, -j * HEXAGON_DELTA_Y + HEXAGON_START_Y), _hexagonPrefab.transform.rotation));
                }
            }
            return hexagonObjects;
        }
    }
}