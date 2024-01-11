using UnityEngine;

namespace _1Core.Scripts.Levels
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Data/BattleCampInfo")]
    public class LevelConfig : ScriptableObject
    {
        public Wave[] waves;
    }
}