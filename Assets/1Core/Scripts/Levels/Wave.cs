using System;
using UnityEngine;

namespace _1Core.Scripts.Levels
{
    [CreateAssetMenu(fileName = "Wave", menuName = "Data/Waves")]
    [Serializable]
    public class Wave : ScriptableObject
    {
        public GameObject[] enemies;
    }
}