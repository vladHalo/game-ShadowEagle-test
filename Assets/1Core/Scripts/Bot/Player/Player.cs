using _1Core.Scripts.Enums;
using _1Core.Scripts.Views;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _1Core.Scripts.Bot.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : Bot
    {
        [SerializeField] private int _indexAttackClick;
        [SerializeField] private float _healing = 1f;
        [SerializeField] private float _speedMove = 5f;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ProgressBarView _attackBar;
        [SerializeField] private Ability _ability;
        [SerializeField] private Text _textIndexAttack;

        [Inject] private SceneManager _sceneManager;

        private AttackStatus _attackStatus;

        private void Update()
        {
            base.Update();
            _attackBar.SetValue(_attackTimer / _attackDelay);
            Attack();
            Movement();
        }

        private void FindTarget(float damage)
        {
            foreach (var bot in _sceneManager.enemyFactory.GetEnemies())
            {
                if (IsAttackRange(bot.transform.position))
                {
                    bool isDead = bot.SetDamage(damage);
                    if (isDead)
                    {
                        _hp += _healing;
                        bot.Die();
                    }

                    return;
                }
            }
        }

        private void RefreshAttackStatus() => _attackStatus = AttackStatus.Idle;

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

        protected override void Attack()
        {
            if (_attackStatus == AttackStatus.Attack) return;

            if (Input.GetKeyDown(KeyCode.Space) && _indexAttackClick < 100)
            {
                _indexAttackClick++;
                _textIndexAttack.text = $"{_indexAttackClick}";
            }

            if (CanAttack() && _indexAttackClick > 0)
            {
                _animator.SetTrigger(Str.Attack);
                _attackTimer = 0;
                _attackBar.RefreshValue();
                FindTarget(_damage);
                _indexAttackClick--;
                _textIndexAttack.text = $"{_indexAttackClick}";
            }

            if (Input.GetKeyDown(KeyCode.E) && _indexAttackClick == 0)
            {
                if (_ability.CanAttack())
                {
                    _animator.SetTrigger(Str.DoubleAttack);
                    _ability.RefreshAbility();
                    _attackBar.RefreshValue();
                    FindTarget(_ability.damage);
                    _attackStatus = AttackStatus.Attack;
                    Invoke(nameof(RefreshAttackStatus), 1);
                }
            }
        }

        public override void Die()
        {
            base.Die();
            _rigidbody.velocity = Vector3.zero;
            _sceneManager.ResultGame(GameResult.Lose);
            _sceneManager.player = null;
        }
    }
}