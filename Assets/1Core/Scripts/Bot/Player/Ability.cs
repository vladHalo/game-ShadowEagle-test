using _1Core.Scripts.Views;
using UnityEngine;

namespace _1Core.Scripts.Bot.Player
{
    public class Ability : MonoBehaviour
    {
        [SerializeField] private float _delay = 2;
        
        private float _timer;

        public float damage = 4;
        public ProgressBarView bar;

        private void Start()
        {
            _timer = _delay;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            bar.SetValue(_timer / _delay);
        }

        public void RefreshAbility() => _timer = 0;
        
        public bool CanAttack()
        {
            return _timer >= _delay;
        }
    }
}