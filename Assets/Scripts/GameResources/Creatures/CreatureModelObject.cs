using System.Collections;
using UnityEngine;
using System;
using Assets.Scripts.MVC.Battle;

namespace Assets.Scripts.GameResources.MapCreatures
{
    public class CreatureModelObject : GameMapObject
    {
        [Header("Animations names")]
        [SerializeField] private string _idle;
        [SerializeField] private string _run;
        [SerializeField] private string _loose;
        [SerializeField] private string _atack;
        [SerializeField] private string _hit;
        [Header("Animations clips")]
        [SerializeField] private AnimationClip _attackAnimationClip;
        [SerializeField] private AnimationClip _deathAnimationClip;

        public event Action<CreatureModelObject> OnStartCreatureDeath;
        public event Action<CreatureModelObject> OnCreatureDeath;
        public event Action<CreatureModelObject> OnActionEnded;

        private Coroutine _animationCoroutine;
        [SerializeField] private Animator _animator;
        public Hexagon CurrentHexagon;
        [field: SerializeField] public int CreatureID { get; private set; }
        public int DicCreatureId { get; private set; }
        public int Amount { get; private set; }
        public int SpriteID { get; private set; }
        public float AttackDuration => _attackAnimationClip.length;
        public int Health { get; private set; }
        public DicCreatureDTO DicCreatureDTO { get; private set; }
        public CreatureStackObjectFullInfo CreatureInfo { get; private set; }
        public CreatureSide CreatureSide { get; private set; }
        private Quaternion _lookDirection;
        [field: SerializeField] public bool IsBlock { get; private set; }
        private BattleModel _battleModel;
        public bool IsInModeState { get; private set; }

        public void Init(BattleModel battleModel,CreatureStackObjectFullInfo creatureStackObjectFullInfo,DicCreatureDTO dicCreatureDTO,
            int creatureID, CreatureSide creatureSide, Quaternion lookDirection, int spriteID)
        {
            _battleModel = battleModel;
            Health = dicCreatureDTO.healthPoints;
            SpriteID = spriteID;
            _lookDirection = lookDirection;
            CreatureInfo = creatureStackObjectFullInfo;
            CreatureID = creatureID;
            DicCreatureDTO = dicCreatureDTO;
            CreatureSide = creatureSide;
        }

        public void SetDicCreatureID(int dicCreatureID, int amount)
        {
            if (amount < 0)
                amount = 0;

            if (dicCreatureID < 0)
                dicCreatureID = 0;

            DicCreatureId = dicCreatureID;
            Amount = amount;
        }

        public void SetCurrentHexagon(Hexagon hexagon)
        {
            CurrentHexagon = hexagon;
        }

        public void SetCreatureDTO(DicCreatureDTO dicCreatureDTO)
        {
            DicCreatureDTO = dicCreatureDTO;
            Health = dicCreatureDTO.healthPoints;
        }


        public virtual void EnterInIdleState()
        {
            IsBlock = false;
            transform.localRotation = _lookDirection;
            IsInModeState = false;
            _animator.CrossFade(_idle, 0.5f);
            OnActionEnded?.Invoke(this);
        }

        public virtual void EnterInMoveState()
        {
            IsInModeState = true;
            _animator.CrossFade(_run, 0);
        }

        public void Loose()
        {
            if (_animator == null)
            {
                Destroy(gameObject);
            }
            else
            {
                if(_animationCoroutine != null)
                    StopCoroutine(_animationCoroutine);
                _animationCoroutine = StartCoroutine(LooseAnimation());
            }
        }

        private IEnumerator HitCoroutineState()
        {
            IsInModeState = false;
            _animator.CrossFade(_hit, 0);
            yield return new WaitForSeconds(0.4f);
            EnterInIdleState();
        }

        public virtual void EnterInHitState()
        {
            if (!IsInModeState)
                StartCoroutine(HitCoroutineState());
        }

        private IEnumerator LooseAnimation()
        {
            OnStartCreatureDeath?.Invoke(this);
            IsInModeState = false;
            _animator.CrossFade(_loose, 0);
            yield return new WaitForSeconds(_deathAnimationClip.length);
            _animationCoroutine = StartCoroutine(DeathAnimation());
        }

        public void Attack(CreatureModelObject creatureToKill, bool isKilled, int attakDamage , int amount)
        {
            Amount = amount;
            if (creatureToKill != null && creatureToKill.IsBlock && !isKilled)
            {
                if (_animationCoroutine != null)
                    StopCoroutine(_animationCoroutine);
                _animationCoroutine = StartCoroutine(AttackCoroutrine(creatureToKill, false, attakDamage));
                creatureToKill.StartBlockAnimation();
                return;
            }
            if (_animator == null && creatureToKill.DicCreatureDTO.healthPoints <= 0)
            {
                if (creatureToKill != null && isKilled)
                    creatureToKill.Loose();
            }
            else
            {
                if (_animationCoroutine != null)
                    StopCoroutine(_animationCoroutine);
                if(creatureToKill != null)
                    _animationCoroutine = StartCoroutine(AttackCoroutrine(creatureToKill, isKilled, attakDamage));
            }
        }

        private IEnumerator DeathAnimation()
        {
            Vector3 target = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);
            while (true)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime);
                float distance = Vector3.Distance(transform.position, target);
                if (distance <= 0.1f) 
                    break;
                yield return null;
            }
            Destroy(gameObject);
            OnCreatureDeath?.Invoke(this);
        }

        public void EnterInBlockByTrigger()
        {
            IsInModeState = false;
            _animator.CrossFade(_hit, 0);
        }

        public void EnterInAttackByTrigger()
        {
            IsInModeState = false;
            _animator.CrossFade(_atack,0);
        }

        public void Block()
        {
            IsBlock = true;
        }

        public void StartBlockAnimation()
        {
            if (_animationCoroutine != null)
                StopCoroutine(_animationCoroutine);
            _animationCoroutine = StartCoroutine(BlockCoroutine());
        }

        private IEnumerator BlockCoroutine()
        {
            yield return new WaitForSeconds(0.4f);
            EnterInBlockByTrigger();
            yield return new WaitForSeconds(0.3f);
            OnActionEnded?.Invoke(this);
            EnterInIdleState();
        }

        public virtual void HandleAttack(CreatureModelObject creatureToKill)
        {
            EnterInAttackByTrigger();
            creatureToKill.EnterInHitState();
        }

        public void SetHealthPoints(int healthPoints)
        {
            DicCreatureDTO.healthPoints = healthPoints;
            if (DicCreatureDTO.healthPoints < 0)
                DicCreatureDTO.healthPoints = 0;
        }

        private IEnumerator AttackCoroutrine(CreatureModelObject creatureToKill, bool isKilled , int attakDamage)
        {
            transform.LookAt(creatureToKill.transform);
            HandleAttack(creatureToKill);
            yield return new WaitForSeconds(_attackAnimationClip.length);
            creatureToKill.SetHealthPoints(attakDamage);
            if (isKilled)
                creatureToKill.Loose();
            else
                EnterInIdleState();
        }

    }
}