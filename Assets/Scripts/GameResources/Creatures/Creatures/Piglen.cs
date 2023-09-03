using Assets.Scripts.GameResources.MapCreatures;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameResources.Creatures.Creatures
{
    public class Piglen : CreatureModelObject
    {
        public override void HandleAttack(CreatureModelObject creatureToKill)
        {
            EnterInAttackByTrigger();
            StartCoroutine(CallTargetCreatureHit(creatureToKill));
        }

        private IEnumerator CallTargetCreatureHit(CreatureModelObject creatureToKill)
        {
            yield return new WaitForSeconds(1.2f);
            creatureToKill.EnterInHitState();
            //yield return new WaitForSeconds(0.3f);
            //creatureToKill.EnterInHitState();
            //yield return new WaitForSeconds(0.3f);
            //creatureToKill.EnterInHitState();
        }
    }
}