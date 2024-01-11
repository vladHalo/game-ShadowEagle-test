using UnityEngine;

namespace _1Core.Scripts.Bot.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : Bot
    {
        [SerializeField] private float _speedMove = 5f;
        [SerializeField] private Rigidbody _rigidbody;

        private void Update()
        {
            Movement();
            Attack();
        }

        protected override void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (CanAttack())
                {
                    _animator.SetTrigger(Str.Attack);
                }

                FindTarget();
            }
        }

        private void FindTarget()
        {
            foreach (var bot in _sceneManager.enemyFactory.GetEnemies())
            {
                if (IsAttackRange(bot.transform.position))
                {
                    transform.transform.rotation =
                        Quaternion.LookRotation(bot.transform.position - transform.position);
                    bot.GetDamage(_damage);
                    return;
                }
            }
        }

        private void Movement()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 moveVector = new Vector3(horizontalInput, 0, verticalInput);
            _rigidbody.velocity =
                new Vector3(moveVector.x * _speedMove, _rigidbody.velocity.y, moveVector.z * _speedMove);

            if (moveVector != Vector3.zero)
            {
                Vector3 direct =
                    Vector3.RotateTowards(transform.forward, moveVector, _speedMove * 4f * Time.fixedDeltaTime, 0.0f);
                _rigidbody.rotation = Quaternion.LookRotation(direct);
            }

            var floatSpeed = _rigidbody.velocity.magnitude / _speedMove;
            _animator.SetFloat(Str.Speed, floatSpeed);
        }
    }
}