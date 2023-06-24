using UnityEngine;

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
        private static readonly int EnemyIdle = Animator.StringToHash("EnemyIdle");
        private static readonly int EnemyReactionHit = Animator.StringToHash("EnemyReactionHit");

        private void Awake()
        {
            _enemyController = enemy.GetComponent<IEnemyController>();
            _animator = GetComponent<Animator>();
        }

        private const float TRANSITION_DURATION = 0.3f;
        private void Update()
        {
            var state = GetState();
            if (_currentState == state)
            {
                return;
            }

            _animator.CrossFade(state, TRANSITION_DURATION, 0);
            _currentState = state;
        }

        private int GetState()
        {
            if (Time.time < _lockedTill) return _currentState;
            if (_enemyController.IsHit)
            {
                return LockState(EnemyReactionHit, 0.3f);
            }
            return EnemyIdle;

            int LockState(int s, float t)
            {
                _lockedTill = Time.time + t;
                return s;
            }
        }
    }
}