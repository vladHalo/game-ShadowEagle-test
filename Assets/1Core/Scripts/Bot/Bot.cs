using UnityEngine;
using Zenject;

namespace _1Core.Scripts.Bot
{
    [RequireComponent(typeof(Animator))]
    public abstract class Bot : MonoBehaviour
    {
        [SerializeField] private float _hp;
        [SerializeField] protected float _damage;
        [SerializeField] private float _attackDelay = 2;
        [SerializeField] private float _attackRange = 2;

        [SerializeField] protected Animator _animator;
        [Inject] protected SceneManager _sceneManager;
        private float _attackTimer = 2;

        protected abstract void Attack();

        public void GetDamage(float damage)
        {
            _hp -= damage;
            Die();
        }

        protected bool IsAttackRange(Vector3 position)
        {
            var distance = Vector3.Distance(transform.position, position);
            if (distance <= _attackRange)
            {
                return true;
            }

            return false;
        }

        protected bool CanAttack()
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer >= _attackDelay)
            {
                _attackTimer = 0;
                return true;
            }

            return false;
        }

        private void Die()
        {
            if (_hp > 0) return;
            _animator.SetTrigger(Str.Die);
            enabled = false;
        }
    }
}