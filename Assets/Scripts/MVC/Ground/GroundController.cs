using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.MVC.Ground
{
    public class GroundController : MonoBehaviour
    {
        public AnimationCurve HeightCurve;

        [SerializeField] private LayerMask _cellLayer;
        [SerializeField] private GameObject _pointPrefab;
        [SerializeField] private List<TerrainLayer> _terrainLayers = new List<TerrainLayer>();
        private Terrain _terrain;
        private TerrainGenerator _terrainGenerator;
        private GroundModel _groundModel;
        private GameModel _gameModel;
        private float _cellsPerTextureCoefficient;
        
        [Serializable]
        private struct TerrainLayer
        {
            public AnimationCurve HeightCurve;
            public int TextureID;
            public TerrainTypes TerrainTypes;
        }

        public void Init(GroundModel groundModel, GameModel gameModel)
        {
            _gameModel = gameModel;
            _groundModel = groundModel;
        }

        public void SetTerrainGenerator(TerrainGenerator terrainGenerator)
        {
            _terrainGenerator = terrainGenerator;
        }

        public void SetTerrain(Terrain terrain)
        {
            _terrain = terrain;
        }

        public void GenerateTrain()
        {
            _terrain.terrainData.size = new Vector3(_groundModel.Widht, 0, _groundModel.Height);
            _terrain.transform.position = new Vector3(-0.5f, 0.55f, _groundModel.Height * -1 + 0.5f);
            _cellsPerTextureCoefficient = (float)_terrain.terrainData.alphamapWidth / (float)_groundModel.Widht;
            var map =_terrainGenerator.GenerateNoiseMap();
            _terrain.terrainData.SetHeights(0, 0, map);
            _terrain.terrainData.SetAlphamaps(0, 0, GetCollorMap(_terrain.terrainData));
        }


        private int GetXPositionInCellsField(int x)
        {
            float num = x / _cellsPerTextureCoefficient;
            return (int)Math.Floor(num);
        }

        private int GetYPositionInCellsField(int y)
        {
            float num = y / _cellsPerTextureCoefficient;
            return (int)Math.Floor(num);
        }

        private float Mapping(float value , float sMin, float sMax, float mMin , float mMax)
        {
            return (value - sMin) * (mMax - mMin) / (sMax - sMin) + mMin;
        }

        private bool GetClosestRoadCellOnLine(int y, out Cell cell)
        {
            for(int x = 0; x < _groundModel.Widht; x++)
            {
                if(_groundModel.TryGetCellsByCoordinates(y,x, out cell))
                {
                    cell.name = x + " " + y;
                    if (cell.Type == TerrainTypes.DIRT_ROAD)
                        return true;
                }
            }
            cell = null;
            return false;

        }


        private float CellPositionToTexture(float position)
        {
            return position * _cellsPerTextureCoefficient;
        }

        public float[,,] GetCollorMap(TerrainData terrainData)
        {
            float[,,] splatmapData = new float[terrainData.alphamapWidth,
                                                   terrainData.alphamapHeight,
                                                   terrainData.alphamapLayers];

            Cell closestRoadCell = null;

            for (int y = 0; y < terrainData.alphamapHeight; y++)
            {
                GetClosestRoadCellOnLine(GetYPositionInCellsField(y), out Cell cell1);
                closestRoadCell = cell1;
                if(closestRoadCell != null)
                    closestRoadCell.name = "Selected";
                for (int x = 0; x < terrainData.alphamapWidth; x++)
                {
                    _groundModel.TryGetCellsByCoordinates(GetXPositionInCellsField(x), GetYPositionInCellsField(y), out Cell cell);;
                    float noise = Mathf.PerlinNoise(x * 0.02f, y * 0.02f);

                    for (int i = 0; i < _terrainLayers.Count; i++)
                    {

                        if (_terrainLayers[i].TerrainTypes == cell.Type)
                        {
                            splatmapData[x, y, i] = 1;

                            //if (i == 0)
                            //{
                            //    splatmapData[X, Y, i] = 1;

                            //}
                            //else
                            //{
                            //    //float xCoord = (float)X / terrainData.alphamapWidth;
                            //    //float yCoord = (float)Y / terrainData.alphamapHeight;
                            //    //float noiseGround = Mathf.PerlinNoise(xCoord * 5f, yCoord * 5f);
                            //    //float noiseClamped = Mathf.Clamp01(noiseGround * 3);
                            //    splatmapData[X, Y, 1] = 1;

                            //    //if (noiseGround < 0.6f && noiseGround > 0.4f)
                            //    //{
                            //    //    splatmapData[X, Y, 1] = noiseGround;
                            //    //}
                            //    //else if (noiseGround < 0.4f && noiseGround > 0.3f)
                            //    //{
                            //    //    splatmapData[X, Y, 2] = noiseGround;
                            //    //}
                            //    //else 
                            //    //{
                            //    //    splatmapData[X, Y, 3] = noiseGround;
                            //    //}

                            //}
                            //break;
                        }
                    }
                }
            }

            return splatmapData;
        }
    }
}