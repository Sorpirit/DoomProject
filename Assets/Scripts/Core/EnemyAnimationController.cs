using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimationController : MonoBehaviour
    {
        [SerializeField] private Enemy enemy;
        private IEnemyController _enemyController;
        private Animator _animator;
        private int _currentState;
        private float _lockedTill;
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int ReactionHit = Animator.StringToHash("ReactionHit");
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Death2 = Animator.StringToHash("Death2");
        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int Scream = Animator.StringToHash("Scream");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Walk = Animator.StringToHash("Walk");

        private float _reactionAnimationDuration;

        private void UpdateAnimClipTimes()
        {
            AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in clips)
            {
                switch (clip.name)
                {
                    case "Zombie Reaction Hit":
                        _reactionAnimationDuration = clip.length;
                        break;
                }
            }
        }

        private void Awake()
        {
            _enemyController = enemy.GetComponent<IEnemyController>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            UpdateAnimClipTimes();
        }

        private const float TRANSITION_DURATION = 0.1f;

        private void Update()
        {
            var state = GetState();

            if (_currentState == state)
            {
                if (_currentState == ReactionHit && _enemyController.IsHit)
                {
                    _animator.CrossFade(state, TRANSITION_DURATION, 0, 0.1f);
                }
            }
            else
            {
                _animator.CrossFade(state, TRANSITION_DURATION, 0);
                _currentState = state;
            }
        }

        private int GetState()
        {
            if (_enemyController.IsDead)
            {
                if (Random.value > 0.5f || _currentState == Death)
                {
                    return Death;
                }

                return Death;
            }

            if (_enemyController.IsHit)
            {
                return LockState(ReactionHit, _reactionAnimationDuration);
            }

            if (Time.time < _lockedTill) return _currentState;


            if (_enemyController.IsChasing)
            {
                return Run;
            }

            return Walk;

            int LockState(int s, float t)
            {
                _lockedTill = Time.time + t;
                return s;
            }
        }
    }
}