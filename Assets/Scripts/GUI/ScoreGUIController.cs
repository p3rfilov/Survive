using UnityEngine;
using UnityEngine.UI;

public class ScoreGUIController : MonoBehaviour
{
    public Text score;
    public Text zombies;

    GameRules gameController;

    void Start()
    {
        gameController = transform.GetComponent<GameRules>();
        EventManager.OnGameStatsChanged += DisplayStats;
    }

    void DisplayStats ()
    {
        if (gameController != null)
        {
            score.text = gameController.Score.ToString();
            zombies.text = gameController.EnemyCount.ToString();
        }
    }
}
