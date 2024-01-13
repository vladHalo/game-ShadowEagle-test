using _1Core.Scripts.Bot.Enemy;
using _1Core.Scripts.Bot.Player;
using _1Core.Scripts.Enums;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public Player player;
    public EnemyFactory enemyFactory;
    public GameObject[] panelsFinish;

    public void ResultGame(GameResult gameResult)
    {
        panelsFinish[(int)gameResult].SetActive(true);
    }

    public void Reset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}