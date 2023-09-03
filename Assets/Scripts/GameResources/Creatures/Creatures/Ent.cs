using Assets.Scripts.GameResources.MapCreatures;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameResources.Creatures.BattleCreatures
{
    public class Ent : CreatureModelObject
    {
        public override void HandleAttack(CreatureModelObject creatureToKill)
        {
            EnterInAttackByTrigger();
            StartCoroutine(CallTargetCreatureHit(creatureToKill));
        }

        private IEnumerator CallTargetCreatureHit(CreatureModelObject creatureToKill)
        {
            yield return new WaitForSeconds(AttackDuration / 1.55f);
            creatureToKill.EnterInHitState();

        }
    }
}