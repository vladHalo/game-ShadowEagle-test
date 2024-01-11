using System.Collections.Generic;
using _1Core.Scripts.Levels;
using UnityEngine;
using Zenject;

namespace _1Core.Scripts.Bot.Enemy
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private Factory _enemyFactory;
        [SerializeField] private LevelConfig _config;
        [SerializeField] private List<Bot> _enemies;
        private int _currWave = 0;

        [Inject] private SceneManager _sceneManager;

        private void Start()
        {
            Spawn();
        }

        public void Spawn()
        {
            var countEnemy = _config.waves[_currWave].enemies;

            for (int i = 0; i < countEnemy.Length; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
                Enemy enemy = _enemyFactory.Create<Enemy>(pos);
                _enemies.Add(enemy);
                enemy.Init(_sceneManager);
            }

            _currWave++;
        }

        public List<Bot> GetEnemies() => _enemies;

        public void RemoveEnemy(Enemy enemy)
        {
            _enemies.Remove(enemy);
            enemy.enabled = false;
            if (_enemies.Count == 0)
            {
                Spawn();
            }
        }
    }
}