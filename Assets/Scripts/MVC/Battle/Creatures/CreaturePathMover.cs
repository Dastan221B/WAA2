using Assets.Scripts.GameResources.MapCreatures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MVC.Battle
{
    public class CreaturePathMover : MonoBehaviour
    {
        private BattleModel _battleModel;
        private float _moveSpeed;
        public event System.Action OnEndedMove;

        public void Init(BattleModel battleModel, float moveSpeed)
        {
            _battleModel = battleModel;
            _moveSpeed = moveSpeed;
        }

        public void StartMove(CreatureModelObject creature, List<BattleFieldCoordinates> path)
        {
            StartCoroutine(MoveCreature(creature, path));
        }

        private IEnumerator MoveCreature(CreatureModelObject creature, List<BattleFieldCoordinates> path)
        {
           
            int i = 0;
            Hexagon hexagon;
            Vector3 targetPosition = Vector3.zero;
            if (_battleModel.TryGetHexagonByCoordinates(path[path.Count - 1].x, path[path.Count - 1].y, out hexagon))
                hexagon.PaintToTargetMovePoint();

            if (_battleModel.TryGetHexagonByCoordinates(path[i].x, path[i].y, out hexagon))
                targetPosition = hexagon.transform.position + new Vector3(0, 0.152f, 0);
            creature.EnterInMoveState();
            while (true)
            {
                if (creature == null)
                    break;

                creature.transform.position = Vector3.MoveTowards(creature.transform.position, targetPosition, _moveSpeed * Time.deltaTime);
                creature.transform.LookAt(targetPosition);
                float distance = Vector3.Distance(creature.transform.position, targetPosition);
                if (distance < 0.1f)
                {
                    if (i + 1 >= path.Count)
                    {
                        break;
                    }
                    i++;
                    if (_battleModel.TryGetHexagonByCoordinates(path[i].x, path[i].y, out hexagon))
                        targetPosition = hexagon.transform.position + new Vector3(0, 0.152f, 0);
                }
                yield return true;
            }

            if(creature != null)
            {
                if (_battleModel.TryGetHexagonByCoordinates(path[path.Count - 1].x, path[path.Count - 1].y, out hexagon))
                {
                    hexagon.SetCreature(creature);
                    hexagon.PaintHexagonInCreatureSide();
                }
                hexagon.SetCreature(creature);
                if (_battleModel.TryGetHexagonByCoordinates(path[0].x, path[0].y, out hexagon))
                {
                    hexagon.CreatureExitFromHexagon();
                    hexagon.SetCreature(null);
                }
                creature.CreatureInfo.battleFieldCoordinates = path[path.Count - 1];
                creature.EnterInIdleState();
                OnEndedMove?.Invoke();
            }
            //_battleModel.ResetHexagons();
        }
    }
}