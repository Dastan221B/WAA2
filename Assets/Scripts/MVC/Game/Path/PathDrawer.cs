using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.MVC.Game.Path
{
    public class PathDrawer
    {
        private List<Cell> _drawedPathOnCells = new List<Cell>();

        public void DrawPath(List<Cell> path, List<float> path_lengthes, int movePointsLeft)
        {
            ResetDrawedCells();
            DistrebuteActiveCells(path, path_lengthes, movePointsLeft);

        }

        public void DrawPath(List<Cell> path)
        {
            ResetDrawedCells();
            DrawArrows(path);
        }

        private void DistrebuteActiveCells(List<Cell> path ,List<float> path_lengthes, int movePointsLeft)
        {
            int i;
            float length = 0;
            for (i = 0; i < path_lengthes.Count; i++)
            {
                length += path_lengthes[i];
                if (length > movePointsLeft)
                {
                    break;
                }
            }
            for (int k = 0; k < path.Count - 1; k++)
            {
                if (i - 1 >= k)
                {
                    path[k].SetColorArrow(Color.green);
                    path[k].DrawArrow(path[k + 1].Arrow);
                }
                else
                {
                    path[k].SetColorArrow(Color.red);
                    path[k].DrawArrow(path[k + 1].Arrow);
                }
                _drawedPathOnCells.Add(path[k]);

            }
            path[path.Count - 1].OnTargetMovePoint();
            _drawedPathOnCells.Add(path[path.Count - 1]);
        }

        private void DrawArrows(List<Cell> path)
        {
            for (int i = 0; i < path.Count; i++)
            {
                if (i + 1 < path.Count)
                {
                    path[i].SetColorArrow(Color.green);
                    path[i].DrawArrow(path[i + 1].Arrow);
                    _drawedPathOnCells.Add(path[i]);
                }
            }
            path[path.Count - 1].OnTargetMovePoint();
            _drawedPathOnCells.Add(path[path.Count - 1]);
        }


        public void ResetDrawedCells()
        {
            if(_drawedPathOnCells.Count > 0)
            {
                foreach(var item in _drawedPathOnCells)
                {
                    item.ResetCell();
                }
            }
        }
    }
}