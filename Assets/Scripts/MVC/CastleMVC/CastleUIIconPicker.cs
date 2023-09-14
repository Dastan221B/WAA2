using Assets.Scripts.Interfaces.Game;
using Assets.Scripts.MVC.CastleMVC.View;
using Assets.Scripts.MVC.Game.Views.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.MVC.CastleMVC
{
    public class CastleUIIconPicker : MonoBehaviour
    {
        [SerializeField] private LayerMask _iconLayer;
        private ProgramState _programState;
        private CastleCommandsSender _castleCommandsSender;
        private GameModel _gameModel;
        private CastleView _castleView;

        private float _clicked = 0;
        private float _clicktime = 0;
        private float _clickdelay = 1f;
        private string _castleIDSelected;

        public void Init(CastleView castleView,GameModel gameModel,ProgramState programState , CastleCommandsSender castleCommandsSender)
        {
            _castleView = castleView;
            _gameModel = gameModel;
            _programState = programState;
            _castleCommandsSender = castleCommandsSender;
        }

        private void Update()
        {
            if (!_gameModel.IsCurrentTurn) return;

            if (Input.GetMouseButtonDown(0) && TryPickCaslteIcon(out CastleIcon castleIcon))
            {
                castleIcon.SelectCastle();
                _clicked++;
                if (_clicked == 1) _clicktime = Time.time;

                if (_clicked > 1 && Time.time - _clicktime < _clickdelay)
                {
                    _castleIDSelected = castleIcon.Castle.MapObjectID;
                    _castleCommandsSender.SendCastleFullInfoRequest(castleIcon.Castle.MapObjectID);
                }
                else if (_clicked > 2 || Time.time - _clicktime > 1) _clicked = 0;
            }
        }

        public void CallOpenCasle()
        {
            _castleCommandsSender.SendCastleFullInfoRequest(_castleIDSelected);
        }

        public bool TryPickCaslteIcon(out CastleIcon castleIcon)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = Input.mousePosition;
                List<RaycastResult> raysastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, raysastResults);
                foreach (var item in raysastResults)
                {
                    if (item.gameObject.TryGetComponent(out castleIcon))
                    {
                        return true;
                    }
                }
            }
            castleIcon = null;
            return false;

        }
    }
}