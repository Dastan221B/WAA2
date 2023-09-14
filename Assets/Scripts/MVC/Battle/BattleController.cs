using Assets.Scripts.GameResources.MapCreatures;
using Assets.Scripts.MVC.Battle;
using Assets.Scripts.MVC.Battle.BattleProcess;
using Assets.Scripts.MVC.Battle.Views;
using Assets.Scripts.MVC.CastleMVC;
using Assets.Scripts.MVC.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleController : MonoBehaviour
{
    private ProgramState _programState;
    private HexagonPicker _hexagonPicker;
    private GameAndBattleCommandsSender _gameAndBattleCommandsSender;
    private BattleModel _battleModel;
    private HexagonFieldSelector _hexagonFieldSelecter;
    private CreatureStatsPanel _creatureStatsPanel;
    private LoadScreen _loadScreen;

    public void Init(BattleModel battleModel,
        ProgramState programState , 
        HexagonPicker hexagonPicker , 
        GameAndBattleCommandsSender gameAndBattleCommandsSender,
        HexagonFieldSelector hexagonFieldSelecter,
        LoadScreen loadScreen)
    {
        _loadScreen = loadScreen;
        _battleModel = battleModel;
        _programState = programState;
        _hexagonPicker = hexagonPicker;
        _gameAndBattleCommandsSender = gameAndBattleCommandsSender;
        _hexagonFieldSelecter = hexagonFieldSelecter;
    }

    public void InitInBattle(Camera camera, CreatureStatsPanel creatureStatsPanel)
    {
        _creatureStatsPanel = creatureStatsPanel;
        _hexagonPicker.SetCamera(camera);
        _hexagonFieldSelecter.SetCamera(camera);
        _hexagonFieldSelecter.SetCreatureStatsPanel(creatureStatsPanel);
    }

    public void SetBattleSceneObjectsParent(Transform parent)
    {
        _battleModel.SetBattleSceneObjectsParent(parent);
    }

    private void Update()
    {
        if (!_loadScreen.IsLoaded)
            _hexagonFieldSelecter.TrySelectCreature();

        if (CheckToUI() || _programState.StatesOfProgram != StatesOfProgram.Battle || _battleModel.IsCreatureInAction || _loadScreen.IsLoaded)
            return;
        BattleGameProcess();
    }

    private bool CheckToUI()
    {
        try
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            if (results.Count > 0)
                return true;
            return false;
        }
        catch
        {
            return false;
        }
    }

    private void BattleGameProcess()
    {
        _hexagonFieldSelecter.SelectHexagon();

        if (Input.GetMouseButtonDown(0) && _hexagonFieldSelecter.SelectedHexagon != null)
        {
            if (_battleModel.TryGetCreatureByID(_battleModel.ActiveCreatureStackBattleObjectFullInfo.battleFieldObjectId, out CreatureModelObject battleCreature))
            {
                AttackType attackType;

                attackType = battleCreature.DicCreatureDTO.attackType;

                if (!_hexagonFieldSelecter.SelectedHexagon.IsAvalableToMove && _hexagonFieldSelecter.SelectedHexagon.BattleCreature == null)
                    return;

                if (!_hexagonFieldSelecter.SelectedHexagon.IsAvalableToMove)
                    return;

                if (_hexagonFieldSelecter.SelectedHexagon.BattleCreature != null)
                {

                    if (Mathf.Abs(_battleModel.ActiveCreatureStackBattleObjectFullInfo.battleFieldCoordinates.x - _hexagonFieldSelecter.SelectedHexagon.BattleFieldCoordinates.x) > 1 ||
                    Mathf.Abs(_battleModel.ActiveCreatureStackBattleObjectFullInfo.battleFieldCoordinates.y - _hexagonFieldSelecter.SelectedHexagon.BattleFieldCoordinates.y) > 1)
                    {
                        BattleFieldCoordinates coordinates = _hexagonFieldSelecter.ClosestHexagonToAttackCreature.BattleFieldCoordinates;
                        if (attackType == AttackType.MELEE)
                        {
                            _creatureStatsPanel.Close();
                            _gameAndBattleCommandsSender.SendMoveAndHitRequest(coordinates, _hexagonFieldSelecter.SelectedHexagon.CreatureID);
                            _hexagonFieldSelecter.SelectedHexagon.PaintToTargetMovePoint();
                            _battleModel.ResetHexagons();
                        }
                        else if (attackType == AttackType.RANGED || attackType == AttackType.RANGED_WITHOUT_PENALTY)
                        {
                            _gameAndBattleCommandsSender.SendHitRangedRequest(_hexagonFieldSelecter.SelectedHexagon.CreatureID);
                            _hexagonFieldSelecter.SelectedHexagon.PaintToTargetMovePoint();
                            _battleModel.ResetHexagons();
                        }
                    }
                    else
                    {

                        if (attackType == AttackType.MELEE)
                        {
                            if (_hexagonFieldSelecter.SelectedHexagon.BattleCreature != null &&
                                _hexagonFieldSelecter.ClosestHexagonToAttackCreature.BattleCreature != null)
                            {
                                BattleFieldCoordinates coordinates = _hexagonFieldSelecter.ClosestHexagonToAttackCreature.BattleFieldCoordinates;
                                _gameAndBattleCommandsSender.SendHitMeleeRequest(coordinates, _hexagonFieldSelecter.SelectedHexagon.CreatureID);
                                _battleModel.ResetHexagons();
                            }
                            else
                            {
                                BattleFieldCoordinates coordinates = _hexagonFieldSelecter.ClosestHexagonToAttackCreature.BattleFieldCoordinates;
                                _gameAndBattleCommandsSender.SendMoveAndHitRequest(coordinates, _hexagonFieldSelecter.SelectedHexagon.CreatureID);
                                _battleModel.ResetHexagons();
                            }

                        }
                        else if (attackType == AttackType.RANGED || attackType == AttackType.RANGED_WITHOUT_PENALTY)
                        {
                            _gameAndBattleCommandsSender.SendHitRangedRequest(_hexagonFieldSelecter.SelectedHexagon.CreatureID);
                            _hexagonFieldSelecter.SelectedHexagon.PaintToTargetMovePoint();
                            _battleModel.ResetHexagons();
                        }
                    }
                }
                else
                {
                    _creatureStatsPanel.Close();
                    _gameAndBattleCommandsSender.SendMoveCreatureRequest(_hexagonFieldSelecter.SelectedHexagon.BattleFieldCoordinates);
                    _hexagonFieldSelecter.SelectedHexagon.PaintToTargetMovePoint();
                    _battleModel.ResetHexagons();
                }
            }
        }
    }
}
