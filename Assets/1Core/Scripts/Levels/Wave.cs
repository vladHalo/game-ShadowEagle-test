using System;
using UnityEngine;

namespace _1Core.Scripts.Levels
{
    [CreateAssetMenu(fileName = "Wave", menuName = "Data/Waves")]
    [Serializable]
    public class Wave : ScriptableObject
    {
        public EnemyWave[] enemies;
    }

    [Serializable]
    public class EnemyWave
    {
        public GameObject enemies;
        public bool isApart;
    }
}