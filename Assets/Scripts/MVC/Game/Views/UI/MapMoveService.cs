using Assets.Scripts.MVC.Ground;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum MapCameraDirection
{
    Right,
    Left,
    Up,
    Down
}

public class MapMoveService : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Camera _mapCamera;
    [SerializeField] private MapCameraDirection _mapCameraDirection;
    private float _speed = 5f;
    private bool _buttonState;

   
    private void MoveCamera()
    {
        var _gameCompositeRoot = FindObjectOfType<GameController>();
        if (_mapCameraDirection == MapCameraDirection.Down && _mapCamera.transform.position.z >= MapBorders(_gameCompositeRoot.GameModel.MapSize.x))
        {
            _mapCamera.transform.Translate(Vector3.down * Time.deltaTime * _speed);
        }

        if (_mapCameraDirection == MapCameraDirection.Up  && _mapCamera.transform.position.z <= MapBorders(_gameCompositeRoot.GameModel.MapSize.x))
        {
            _mapCamera.transform.Translate(Vector3.up * Time.deltaTime * _speed);
        }

        if (_mapCameraDirection == MapCameraDirection.Right && _mapCamera.transform.position.x <= MapBorders(_gameCompositeRoot.GameModel.MapSize.x))
        {
            _mapCamera.transform.Translate(Vector3.right * Time.deltaTime * _speed);
        }

        if (_mapCameraDirection == MapCameraDirection.Left && _mapCamera.transform.position.x >= MapBorders(_gameCompositeRoot.GameModel.MapSize.x))
        {
            _mapCamera.transform.Translate(Vector3.left * Time.deltaTime * _speed);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        _buttonState = true;
        Debug.Log("_mapCameraDirection " +_mapCameraDirection);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _buttonState = false;
    }
    private void Update()
    {
        if (_buttonState == true)
        {
            MoveCamera();
            Debug.Log("_buttonState " + _buttonState);
        }
    }
    private float MapBorders(int mapSize)
    {
        if(mapSize == 32)
        {
            if(_mapCameraDirection == MapCameraDirection.Down)
            {
                return -26.5f;
            } else if(_mapCameraDirection == MapCameraDirection.Up)
            {
                return -4.6f;
            } else if(_mapCameraDirection == MapCameraDirection.Left)
            {
                return 4.45f;
            }
            else
            {
                return 26.5f;
            }
        }else if(mapSize == 10)
        {
            if (_mapCameraDirection == MapCameraDirection.Down)
            {
                return -4.6f;
            }
            else if (_mapCameraDirection == MapCameraDirection.Up)
            {
                return -4.6f;
            }
            else if (_mapCameraDirection == MapCameraDirection.Left)
            {
                return 4.45f;
            }
            else
            {
                return 4.45f;
            }
        } else if(mapSize == 64)
        {
            if (_mapCameraDirection == MapCameraDirection.Down)
            {
                return -49.5f;
            }
            else if (_mapCameraDirection == MapCameraDirection.Up)
            {
                return -4.6f;
            }
            else if (_mapCameraDirection == MapCameraDirection.Left)
            {
                return 4.45f; 
            }
            else
            {
                return 58.5f;
            }
        }
        else
        {
            if (_mapCameraDirection == MapCameraDirection.Down)
            {
                return -90.5f;
            }
            else if (_mapCameraDirection == MapCameraDirection.Up)
            {
                return -4.6f;
            }
            else if (_mapCameraDirection == MapCameraDirection.Left)
            {
                return 4.45f;
            }
            else
            {
                return 90.5f;
            }
        }
    }
}
