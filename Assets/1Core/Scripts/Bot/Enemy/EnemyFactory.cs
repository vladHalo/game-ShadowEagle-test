using System.Collections.Generic;
using _1Core.Scripts.Enums;
using _1Core.Scripts.Levels;
using UnityEngine;
using Zenject;

namespace _1Core.Scripts.Bot.Enemy
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private Factory _enemyFactory;
        [SerializeField] private LevelConfig _config;
        [SerializeField] private WavesViews _wavesViews;
        [SerializeField] private GameObject _smallGoblinPrefab;
        [SerializeField] private List<Enemy> _enemies;

        private int _currWave = 0;

        [Inject] private SceneManager _sceneManager;

        private void Start()
        {
            _wavesViews.RefreshWaves(_currWave, _config.waves.Length);
            Spawn();
        }

        public void Spawn()
        {
            var enemies = _config.waves[_currWave].enemies;

            for (int i = 0; i < enemies.Length; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
                Enemy enemy = _enemyFactory.Create<Enemy>(pos);
                _enemies.Add(enemy);
                enemy.Init(_sceneManager, enemies[i].isApart);
            }

            _currWave++;
        }

        public void SpawnSmallGoblins(Vector3 position)
        {
            for (int i = 0; i < 2; i++)
            {
                Vector3 pos = new Vector3(position.x + i * 2, 0, position.z + i * 2);
                Enemy enemy = _enemyFactory.Create<Enemy>(_smallGoblinPrefab, pos);
                _enemies.Add(enemy);
                enemy.Init(_sceneManager, false);
            }
        }

        public List<Enemy> GetEnemies() => _enemies;

        public void RemoveEnemy(Enemy enemy)
        {
            _enemies.Remove(enemy);
            enemy.enabled = false;
            if (_enemies.Count == 0)
            {
                if (_currWave < _config.waves.Length)
                {
                    _wavesViews.RefreshWaves(_currWave, _config.waves.Length);
                    Spawn();
                }
                else
                {
                    _sceneManager.ResultGame(GameResult.Win);
                }
            }
        }
    }
}