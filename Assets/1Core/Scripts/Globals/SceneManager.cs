using _1Core.Scripts.Bot.Enemy;
using _1Core.Scripts.Bot.Player;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public Player player;
    public EnemyFactory enemyFactory;
    public GameObject lose;
    public GameObject win;

    public void GameOver()
    {
        lose.SetActive(true);
    }

    public void Reset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
