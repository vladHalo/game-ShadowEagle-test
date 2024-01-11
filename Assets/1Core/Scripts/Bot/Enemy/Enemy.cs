using UnityEngine;
using UnityEngine.AI;

namespace _1Core.Scripts.Bot.Enemy
{
    public class Enemy : Bot
    {
        [SerializeField] private NavMeshAgent _agent;

        private Bot _player;

        private void Start()
        {
            _player = _sceneManager.player;
            _agent.SetDestination(_sceneManager.player.transform.position);
        }

        private void Update()
        {
            Attack();

            _animator.SetFloat(Str.Speed, _agent.speed);
        }

        public void Init(SceneManager sceneManager)
        {
            _sceneManager = sceneManager;
        }

        protected override void Attack()
        {
            if (IsAttackRange(_player.transform.position))
            {
                _agent.isStopped = true;
                if (CanAttack())
                    _player.GetDamage(_damage);
            }
            else
            {
                _agent.isStopped = false;
            }
        }
    }
}