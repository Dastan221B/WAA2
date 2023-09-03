using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets.Scripts.MVC.Game.Path
{
    public class HeroPathMover : MonoBehaviour
    {
        private float _heroMoveSpeed;
        public event Action<HeroModelObject> OnHeroEndedMove;
        private Camera _camera;
        private Camera _mapCamera;
        private Action _eventBeforeMove;

        public void AddEventBeforeMove(Action action)
        {
            _eventBeforeMove = action;
        }

        public void Init(float speed)
        {
            _heroMoveSpeed = speed;
        }
        
        public void Init(Camera camera, Camera mapCamera)
        {
            _camera = camera;
            _mapCamera = mapCamera;
        }

        public void MoveHeroOnPath(HeroModelObject heroModelObject , List<Cell> path)
        {
            StartCoroutine(MoveHero(heroModelObject, path));
        }

        public void MoveHeroOnPath(HeroModelObject heroModelObject, List<Cell> path, Action action)
        {
            StartCoroutine(MoveHero(heroModelObject, path, action));
        }

        private IEnumerator MoveHero(HeroModelObject heroModelObject, List<Cell> path, Action action = null)
        {

            _camera.GetComponent<StrategyCamera>().enabled = false;
            int i = 0;
            Vector3 targetPosition = new Vector3(path[i].transform.position.x, heroModelObject.transform.position.y, path[i].transform.position.z);
            Vector3 lastPointPosition = new Vector3(path[path.Count - 1].transform.position.x, heroModelObject.transform.position.y, path[path.Count - 1].transform.position.z);
            if (Vector3.Distance(heroModelObject.transform.position, lastPointPosition) > 0.1f)
                heroModelObject.Move();

            while (true)
            {

                heroModelObject.transform.position = Vector3.MoveTowards(heroModelObject.transform.position, targetPosition, _heroMoveSpeed * Time.deltaTime);
                heroModelObject.transform.LookAt(targetPosition);
                float distance = Vector3.Distance(heroModelObject.transform.position, targetPosition);
                if(heroModelObject.HeroInTurn)
                {
                    _camera.transform.position = Vector3.Lerp(_camera.transform.position,
                        new Vector3(heroModelObject.transform.position.x, _camera.transform.position.y, path[i].transform.position.z - 5), Time.deltaTime * 10);
                }
                var mapsize = FindObjectOfType<GameController>().GameModel.MapSize.x;
                var mapPosX = heroModelObject.transform.position.x;
                var mapPosZ = path[i].transform.position.z;
                if (mapsize != 10)
                {
                    //if (mapPosX <= 4.45f)
                    //{
                    //    mapPosX = 4.45f;
                    //}
                    //else
                    //if (mapPosX >= 4.5 + mapsize - 10)
                    //{
                    //    mapPosX = 4.5f + mapsize - 10;
                    //}
                    //if (mapPosZ <= -4.5f - mapsize + 10)
                    //{
                    //    mapPosZ = -4.5f - mapsize + 10;
                    //}
                    //else
                    //if (mapPosZ >= -4.6f)
                    //{
                    //    mapPosZ = -4.6f;
                    //}
                    _mapCamera.transform.position = Vector3.Lerp(_mapCamera.transform.position,
                  new Vector3(mapPosX, 8.9f, mapPosZ), Time.deltaTime * 10);
                }
                    if (distance <= 0.1f)
                {
                    if(i + 1 >= path.Count)
                    {
                        break;
                    }
                    path[i].ResetCell();
                    i++;
                    targetPosition = new Vector3(path[i].transform.position.x, heroModelObject.transform.position.y, path[i].transform.position.z);
                }

                yield return null;
            }
            _camera.GetComponent<StrategyCamera>().enabled = true;
            OnHeroEndedMove?.Invoke(heroModelObject);
            action?.Invoke();
            _eventBeforeMove?.Invoke();
            _eventBeforeMove = null;
            if(heroModelObject.LastCellStayed.GameMapObjectType == GameMapObjectType.CASTLE)
            {
                heroModelObject.LastCellStayed.BackToBaseInteractiveMapObjectID();
            }
            else
            {
                heroModelObject.LastCellStayed.ResetInteractiveMapObject();
            }
            heroModelObject.SetCellStayed(path[path.Count - 1]);
            path[path.Count - 1].SetInteractiveMapObjectId(heroModelObject.MapObjectID);
            heroModelObject.transform.position = lastPointPosition;
        }
    }
}