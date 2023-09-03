using Assets.Scripts.GameResources.MapCreatures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MVC.Battle
{
    public class Hexagon : MonoBehaviour
    {
        [SerializeField] private Color _redColor;
        [SerializeField] private Color _whiteColor;
        [SerializeField] private Color _greyColor;
        [SerializeField] private Color _blackColor;
        [SerializeField] private Color _greenColor;
        [SerializeField] private Color _blueColor;
        [SerializeField] private MeshRenderer _meshRenderer;
        [field: SerializeField] public BattleFieldCoordinates BattleFieldCoordinates { get; private set; }
        private int _stayedCreatureID;
        private CreatureModelObject _battleCreature;

        public int CreatureID => _stayedCreatureID;

        public bool IsAvalableToMove { get; private set; }
        public CreatureModelObject BattleCreature => _battleCreature;
        private bool _isSelectedToTargetPoint;

        private void Awake()
        {
            Unselect();
        }

        public void SetCreature(CreatureModelObject battleCreature)
        {
            if(battleCreature == null)
            {
                Debug.Log("Battle creature null " + BattleFieldCoordinates.x + "  " + BattleFieldCoordinates.y);
                _stayedCreatureID = 0;
                _battleCreature = null;
                return;
            }

            _stayedCreatureID = battleCreature.CreatureID;
            _battleCreature = battleCreature;
            PaintHexagonInCreatureSide();
        }

        public void SetBattleFieldCoordinates(BattleFieldCoordinates battleFieldCoordinates)
        {
            BattleFieldCoordinates = battleFieldCoordinates;
        }

        public void Select()
        {
            if (_isSelectedToTargetPoint || !IsAvalableToMove)
                return;
            _meshRenderer.material.color = _whiteColor;
            _meshRenderer.enabled = true;
        }

        public void Unselect()
        {
            if (_isSelectedToTargetPoint)
                return;
            if(_battleCreature != null)
            {
                PaintHexagonInCreatureSide();
            }
            else
            {
                if (IsAvalableToMove)
                {
                    _meshRenderer.enabled = true;
                    _meshRenderer.material.color = _greyColor;
                }
                else
                {
                    _meshRenderer.enabled = false;
                }
            }
        }

        public void UnselectColor()
        {
            if (_isSelectedToTargetPoint)
                return;
            if (_battleCreature != null)
            {
                PaintHexagonInCreatureSide();
            }
            else
            {
                _meshRenderer.enabled = false;
            }
        }

        public void PaintToTargetMovePoint()
        {
            _isSelectedToTargetPoint = true;
            _meshRenderer.enabled = true;
            _meshRenderer.material.color = _redColor;
        }


        public void AvalableToMove()
        {
            if (_meshRenderer == null)
                return;
            IsAvalableToMove = true;
            _meshRenderer.material.color = _greyColor;
            _meshRenderer.enabled = true;
        }

        public void DisableToMove()
        {
            IsAvalableToMove = false;
            if(_meshRenderer != null )
             _meshRenderer.enabled = false;
        }


        public void CreatureExitFromHexagon()
        {
            if (IsAvalableToMove)
            {
                _meshRenderer.material.color = _greyColor;
            }
            else
                _meshRenderer.enabled = false;
        }

        public void PaintToGreen()
        {
            _meshRenderer.enabled = true;
            _meshRenderer.material.color = _greenColor;
        }

        public void PaintHexagonInCreatureSide()
        {
            if (_battleCreature == null)
                return;
            _isSelectedToTargetPoint = false;
            _meshRenderer.enabled = true;
            if (_battleCreature.CreatureSide == CreatureSide.Self)
                _meshRenderer.material.color = _whiteColor;
            else
                _meshRenderer.material.color = _blackColor;
        }

        public void SelectToMoveAndAttack()
        {
            if (_isSelectedToTargetPoint)
                return;
            _meshRenderer.material.color = _blueColor;
            _meshRenderer.enabled = true;
        }
    }
}