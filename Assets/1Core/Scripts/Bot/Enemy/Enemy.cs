using UnityEngine;
using UnityEngine.AI;

namespace _1Core.Scripts.Bot.Enemy
{
    public class Enemy : Bot
    {
        [SerializeField] private NavMeshAgent _agent;

        private SceneManager _sceneManager;
        private bool _isApart;

        private void Update()
        {
            base.Update();
            if (_sceneManager.player != null)
            {
                _agent.SetDestination(_sceneManager.player.transform.position);
                Attack();
            }
        }

        protected override void Attack()
        {
            if (IsAttackRange(_sceneManager.player.transform.position))
            {
                _agent.isStopped = true;
                _animator.SetFloat(Str.Speed, 0);
                if (CanAttack())
                {
                    _animator.SetTrigger(Str.Attack);
                    _attackTimer = 0;
                    bool isDead = _sceneManager.player.SetDamage(_damage);
                    if (isDead) _sceneManager.player.Die();
                }
            }
            else
            {
                _agent.isStopped = false;
                _animator.SetFloat(Str.Speed, 1);
            }
        }

        public void Init(SceneManager sceneManager, bool isApart)
        {
            _sceneManager = sceneManager;
            _animator.SetFloat(Str.Speed, 1);
            transform.transform.rotation =
                Quaternion.LookRotation(_sceneManager.player.transform.position - transform.position);
            _isApart = isApart;
        }

        public override void Die()
        {
            if (_isApart) _sceneManager.enemyFactory.SpawnSmallGoblins(transform.position);
            _sceneManager.enemyFactory.RemoveEnemy(this);
            base.Die();
        }
    }
}