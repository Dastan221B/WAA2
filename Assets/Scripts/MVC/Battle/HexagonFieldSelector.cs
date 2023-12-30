using Assets.Scripts.MVC.Battle.Views;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.MVC.Battle
{
    public class HexagonFieldSelector
    {
        private HexagonPicker _hexagonPicker;
        private BattleModel _battleModel;
        private Camera _camera;
        private LayerMask _hexagonLayerMask;
        private CreatureStatsPanel _creatureStatsPanel;
        private ModelCreatures _modelCreatures;

        public Hexagon SelectedHexagon { get; private set; }
        public Hexagon ClosestHexagonToAttackCreature { get; private set; }

        public HexagonFieldSelector(ModelCreatures modelCreatures ,HexagonPicker hexagonPicker , BattleModel battleModel, LayerMask layerMask)
        {
            _hexagonPicker = hexagonPicker;
            _battleModel = battleModel;
            _hexagonLayerMask = layerMask;
            _modelCreatures = modelCreatures;
        }

        public void SetCamera(Camera camera)
        {
            _camera = camera;
        }

        public void SetCreatureStatsPanel(CreatureStatsPanel creatureStatsPanel)
        {
            _creatureStatsPanel = creatureStatsPanel;
        }

        private List<Hexagon> GetNeighbours()
        {
            List<BattleFieldCoordinates> coords = new List<BattleFieldCoordinates>();

            coords.Add(new BattleFieldCoordinates(SelectedHexagon.BattleFieldCoordinates.x + 1, SelectedHexagon.BattleFieldCoordinates.y));
            coords.Add(new BattleFieldCoordinates(SelectedHexagon.BattleFieldCoordinates.x - 1, SelectedHexagon.BattleFieldCoordinates.y));
            coords.Add(new BattleFieldCoordinates(SelectedHexagon.BattleFieldCoordinates.x, SelectedHexagon.BattleFieldCoordinates.y + 1));
            coords.Add(new BattleFieldCoordinates(SelectedHexagon.BattleFieldCoordinates.x + 1, SelectedHexagon.BattleFieldCoordinates.y + 1));
            coords.Add(new BattleFieldCoordinates(SelectedHexagon.BattleFieldCoordinates.x + 1, SelectedHexagon.BattleFieldCoordinates.y - 1));
            coords.Add(new BattleFieldCoordinates(SelectedHexagon.BattleFieldCoordinates.x, SelectedHexagon.BattleFieldCoordinates.y - 1));
            coords = coords.Where(c => c.x < HexagonGenerator.HEXAGON_WIDTH && c.y < HexagonGenerator.HEXAGON_LENGTH).ToList();
            List<Hexagon> result = coords.Select(c => _battleModel.GetHexagonByCoordinates(c.x, c.y)).ToList();
            return result;
        }


        private Hexagon GetClosestHexagon()
        {
            Hexagon hexagon = new Hexagon();
            float _min = float.MaxValue;
            var hexagons = new List<Hexagon>();

            foreach (Hexagon hex in _battleModel._hexagons)
            {
                float distance = Vector3.Distance(hex.transform.position, SelectedHexagon.transform.position); // Расстояние между точкой и шестиугольником

                if (distance >= _battleModel.RadiusHexagonsSelecting)
                {
                    hexagons.Add(hex);
                }
            }

            foreach (Hexagon hex in hexagons)
            {
                if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _hexagonLayerMask))
                {
                    float distance = Vector3.Distance(hex.transform.position,
                         hit.point);
                    if (distance < _min)
                    {
                        _min = distance;
                        hexagon = hex;
                    }
                }
            }
            return hexagon;
        }

        private void TryOpenCreatureStatsInfoPanel(Hexagon hexagon)
        {
            if (hexagon != null && hexagon.BattleCreature != null)
            {
                int center = Screen.width / 2;
                if(Input.mousePosition.x > center)
                {
                    var creatureInfo = hexagon.BattleCreature.DicCreatureDTO;
                    _creatureStatsPanel.Open(creatureInfo, CreatureSide.Enemy, _modelCreatures.GetIconById((int)hexagon.BattleCreature.SpriteID - 1), creatureInfo.attack, creatureInfo.defense, creatureInfo.healthPoints, 0);
                }
                else
                {
                    var creatureInfo = hexagon.BattleCreature.DicCreatureDTO;
                    _creatureStatsPanel.Open(creatureInfo, CreatureSide.Self, _modelCreatures.GetIconById((int)hexagon.BattleCreature.SpriteID - 1), creatureInfo.attack, creatureInfo.defense, creatureInfo.healthPoints, 0);
                }
              
            }
            else
            {
                _creatureStatsPanel.Close();
            }
        }

        public void TrySelectCreature()
        {
            //if (_battleModel.ActiveCreatureStackBattleObjectFullInfo == null)
            //    return;
            if (_hexagonPicker.TryPickHexagon(out Hexagon hexagon))
            {
                TryOpenCreatureStatsInfoPanel(hexagon);
                if (_battleModel.ActiveCreatureStackBattleObjectFullInfo != null && _battleModel.TryGetHexagonByCoordinates(_battleModel.ActiveCreatureStackBattleObjectFullInfo.battleFieldCoordinates.x,
                    _battleModel.ActiveCreatureStackBattleObjectFullInfo.battleFieldCoordinates.y, out Hexagon hex))
                {
                    if (hexagon == hex)
                    {
                        TryOpenCreatureStatsInfoPanel(hexagon);
                        //if (_battleModel.TryGetHexagonByCoordinates(_battleModel.ActiveCreatureStackBattleObjectFullInfo.battleFieldCoordinates.x,
                        //    _battleModel.ActiveCreatureStackBattleObjectFullInfo.battleFieldCoordinates.y, out Hexagon hex))
                        //{
                        //    if (hexagon == hex)
                        //    {
                        //        TryOpenCreatureStatsInfoPanel(hexagon);
                        //    }
                        //}
                    }
                }

            }
        }

        public void SelectHexagon()
        {
            if (_battleModel.ActiveCreatureStackBattleObjectFullInfo == null)
                return;

            if (_hexagonPicker.TryPickHexagon(out Hexagon hexagon))
            {
                TryOpenCreatureStatsInfoPanel(SelectedHexagon);
                if (_battleModel.TryGetHexagonByCoordinates(_battleModel.ActiveCreatureStackBattleObjectFullInfo.battleFieldCoordinates.x,
                    _battleModel.ActiveCreatureStackBattleObjectFullInfo.battleFieldCoordinates.y, out Hexagon hex))
                {
                    if (hexagon == hex)
                    {
                        TryOpenCreatureStatsInfoPanel(hexagon);
                        return;
                    }
                }

                if (SelectedHexagon != null)
                    SelectedHexagon.Unselect();
                if (ClosestHexagonToAttackCreature != null)
                {
                    ClosestHexagonToAttackCreature.Unselect();
                    ClosestHexagonToAttackCreature = null;
                }
                SelectedHexagon = hexagon;
                SelectedHexagon.Select();
                if(SelectedHexagon.BattleCreature != null)
                {
                    if (ClosestHexagonToAttackCreature != null)
                    {
                        _creatureStatsPanel.Close();
                        ClosestHexagonToAttackCreature.Unselect();
                    }
                    ClosestHexagonToAttackCreature = GetClosestHexagon();
                    ClosestHexagonToAttackCreature.SelectToMoveAndAttack();

                }
            }
            else
            {
                if (SelectedHexagon != null)
                    SelectedHexagon.Unselect();
                SelectedHexagon = null;
            }
        }

    }
}