using System;
using UnityEngine;

namespace Assets.Scripts.MVC.Game
{
    public class GameCastleObjectsChangeService : MonoBehaviour
    {
        [Serializable]
        public struct CameraSettings
        {
            [SerializeField] private Vector3 _position;
            [SerializeField] private Vector3 _rotation;

            public Vector3 Position => _position;
            public Vector3 Rotation => _rotation;

        }

        [SerializeField] private GameObject[] _gameObjects;
        [SerializeField] private GameObject[] _castleObjects;
        [SerializeField] private Camera _camera;
        [SerializeField] private CameraSettings _castleCameraSettings;
        [SerializeField] private CameraSettings _gameCameraSettings;

        public void EnterCastle()
        {
            foreach (var item in _gameObjects)
                item.SetActive(false);
            foreach (var item in _castleObjects)
                item.SetActive(true);
            _camera.transform.position = _castleCameraSettings.Position;
            _camera.transform.rotation = Quaternion.Euler(_castleCameraSettings.Rotation);
        }

        public void EnterGame()
        {
            foreach (var item in _gameObjects)
                item.SetActive(true);
            foreach (var item in _castleObjects)
                item.SetActive(false);
            _camera.transform.position = _gameCameraSettings.Position;
            _camera.transform.rotation = Quaternion.Euler(_gameCameraSettings.Rotation);
        }

    }
}