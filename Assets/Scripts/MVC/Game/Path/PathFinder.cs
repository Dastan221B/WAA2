using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.MVC.Game.Path
{
    public class PathFinder
    {

        private GameModel _gameModel;

        public PathFinder(GameModel gameModel)
        {
            _gameModel = gameModel;
        }

        public List<Cell> FindPath(Vector2Int currentPosition, Vector2Int targetPossition, out List<float> path_lengthes)
        {
            List<Vector2> path = CalculatePath(currentPosition, targetPossition, out path_lengthes);
            return ConvertPositionToCellPath(path);
        }


        public List<Cell> ConvertPositionToCellPath(List<Vector2> path)
        {
            List<Cell> cells = new List<Cell>();
            foreach (var position in path)
            {
                if (_gameModel.TryGetCellByPosition(new Vector2Int((int)position.x, -(int)position.y), out Cell cell))
                {
                    cells.Add(cell);
                }
            }
            return cells;
        }

        public List<Cell> ConvertPositionToCellPath(List<Coordinates> path)
        {
            List<Cell> cells = new List<Cell>();
            foreach (var position in path)
            {
                if (_gameModel.TryGetCellByPosition(new Vector2Int((int)position.x, -(int)position.y), out Cell cell))
                {
                    cells.Add(cell);
                }
            }
            return cells;
        }


        private List<Vector2> CalculatePath(Vector2 start, Vector2 end, out List<float> path_lengthes)
        {
            List<Vector2> vertices = new List<Vector2>();
            List<Castle> castles = new List<Castle>(_gameModel.Castles);
            List<Vector2> castleBlocks = new List<Vector2>();

            List<Vector2Int> blockPosition = new List<Vector2Int>()
            {
                new Vector2Int(3,2),
                new Vector2Int(4,1)
            };

            bool end_interactive = false;
            foreach (var castle in castles)
            {
                float[] x_coords = new float[] { castle.transform.position.x + 1, castle.transform.position.x, castle.transform.position.x - 1 };
                float[] y_coords = new float[] { castle.transform.position.z + .5f, castle.transform.position.z - .5f };
                for (int i = 0; i < x_coords.Length; i++)
                {
                    for (int j = 0; j < y_coords.Length; j++)
                    {
                        castleBlocks.Add(new Vector2(x_coords[i], y_coords[j]));
                    }
                }
            }

            foreach(var cell in _gameModel.Cells)
            {
                if (cell.GameMapObjectType == GameMapObjectType.CASTLE || cell.GameMapObjectType == GameMapObjectType.MINE || blockPosition.Contains(new Vector2Int(cell.X, cell.Y)))
                {
                    var vector = new Vector2(cell.transform.position.x, cell.transform.position.y);
                    castleBlocks.Add(vector);
                }
            }

            foreach (Cell terrainCell in _gameModel.Cells)
            {
                if (terrainCell.InteractiveMapObjectId == "" && terrainCell.ParentObjectId == "" 
                                && !castleBlocks.Contains(new Vector2(terrainCell.transform.position.x, terrainCell.transform.position.z)) &&
                                !blockPosition.Contains(new Vector2Int(terrainCell.X,terrainCell.Y)))
                {
                    vertices.Add(new Vector2(terrainCell.transform.position.x, terrainCell.transform.position.z));
                }
            }
            vertices.Add(start);
            if (!vertices.Contains(end))
            {
                List<Vector2> castleGates = castles.Select(c => new Vector2(c.transform.position.x, c.transform.position.z - 0.5f)).ToList();
                if (!castleBlocks.Contains(end) || castleGates.Contains(end))
                {
                    vertices.Add(end);
                    end_interactive = true;
                }
            }
            Dictionary<Vector2, List<Vector2>> neighbours = new Dictionary<Vector2, List<Vector2>>();
            for (int i = 0; i < vertices.Count; i++)
            {
                if (!neighbours.ContainsKey(vertices[i]))
                {
                    neighbours.Add(vertices[i], new List<Vector2>());
                    for (int j = 0; j < vertices.Count; j++)
                    {
                        float d = Vector2.Distance(vertices[i], vertices[j]);
                        if (d < 1.5f)
                        {
                            neighbours[vertices[i]].Add(vertices[j]);
                        }
                    }
                }
            }

            List<Vector2> openVertices = new List<Vector2>();
            openVertices.Add(start);
            List<Vector2> closedVertices = new List<Vector2>();
            Dictionary<Vector2, Vector2> path = new Dictionary<Vector2, Vector2>();
            Dictionary<Vector2, float> G = new Dictionary<Vector2, float>();
            Dictionary<Vector2, float> F = new Dictionary<Vector2, float>();
            float H(Vector2 a, Vector2 b) => Vector2.Distance(a, b);
            float Dist(Vector2 a, Vector2 b) => Vector2.Distance(a, b) * (_gameModel.Cells[(int)b.x, (int)-b.y].Type == 0 ? 0.75f : 1);
            List<Vector2> get_unclosed_neighbours(Vector2 node)
            {
                List<Vector2> list = neighbours[node];
                for (int i = 0; i < list.Count; i++)
                {
                    if (closedVertices.Contains(list[i]))
                    {
                        list.Remove(list[i]);
                    }
                }
                return list;
            }
            void CalculateNeighbour(Vector2 neighbour, Vector2 current)
            {
                path.Add(neighbour, current);
                G.Add(neighbour, G[current] + Dist(current, neighbour));
                F.Add(neighbour, G[neighbour] + H(neighbour, end));
                if (!openVertices.Contains(neighbour))
                {
                    openVertices.Add(neighbour);
                }
            }

            void ReCalculateNeighbour(Vector2 neighbour, Vector2 current)
            {
                path[neighbour] = current;
                G[neighbour] = G[current] + Dist(current, neighbour);
                F[neighbour] = G[neighbour] + H(neighbour, end);
                if (!openVertices.Contains(neighbour))
                {
                    openVertices.Add(neighbour);
                }
            }


            G.Add(start, 0);
            F.Add(start, G[start] + H(start, end));
            while (openVertices.Count != 0)
            {
                float min = int.MaxValue;
                foreach (Vector2 vector in openVertices)
                {
                    if (F.Keys.Contains(vector))
                    {
                        if (F[vector] < min)
                        {
                            min = F[vector];
                        }
                    }
                }
                Dictionary<Vector2, float> F_Filtered = new Dictionary<Vector2, float>();
                foreach (KeyValuePair<Vector2, float> kvp in F)
                {
                    if (openVertices.Contains(kvp.Key))
                    {
                        F_Filtered.Add(kvp.Key, kvp.Value);
                    }
                }
                Vector2 current = F_Filtered.FirstOrDefault(x => x.Value == min).Key;
                if (current == end)
                {
                    break;
                }
                openVertices.Remove(current);
                closedVertices.Add(current);
                foreach (Vector2 neighbour in get_unclosed_neighbours(current))
                {
                    float g = G[current] + Dist(current, neighbour);
                    if (G.Keys.Contains(neighbour))
                    {
                        if (g < G[neighbour])
                        {
                            ReCalculateNeighbour(neighbour, current);
                        }
                    }
                    else if (!openVertices.Contains(neighbour))
                    {
                        CalculateNeighbour(neighbour, current);
                    }
                }
            }
            Vector2 _vector = end;
            List<Vector2> result = new List<Vector2>();
            path_lengthes = new List<float>();
            result.Add(end);
            int count = 0;
            while (_vector != start)
            {
                count++;
                if (path.TryGetValue(_vector, out Vector2 value))
                {
                    result.Add(value);
                    path_lengthes.Add(Dist(value, _vector));
                    _vector = value;
                }
                if (count >= 1000)
                    break;
            }
            path_lengthes = path_lengthes.Select(x => x * 100).ToList();
            path_lengthes.Reverse();
            result.Reverse();
            if (end_interactive)
            {
                //result.Remove(result[result.Count - 1]);
            }
            return result;
        }
    }
}