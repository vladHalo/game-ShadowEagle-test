using UnityEngine;

namespace _1Core.Scripts.Bot
{
    [RequireComponent(typeof(Animator))]
    public abstract class Bot : MonoBehaviour
    {
        [SerializeField] protected float _hp;
        [SerializeField] protected float _damage;
        [SerializeField] protected float _attackDelay = 1;
        [SerializeField] private float _attackRange = 2;

        [SerializeField] protected Animator _animator;

        protected float _attackTimer;

        private void Start()
        {
            _attackTimer = _attackDelay;
        }

        protected void Update()
        {
            _attackTimer += Time.deltaTime;
        }

        protected abstract void Attack();

        protected bool CanAttack()
        {
            return _attackTimer >= _attackDelay;
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

        public bool SetDamage(float damage)
        {
            _hp -= damage;
            return _hp <= 0;
        }

        public virtual void Die()
        {
            _animator.SetTrigger(Str.Die);
            enabled = false;
        }
    }
}