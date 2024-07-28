using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText;
    public int score = 0;

    private void Awake()
    {
        // Ensure there is only one instance of ScoreManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize the score text
        UpdateScoreText();
    }

    private void Update()
    {
        // Get all GameObjects with the "Enemie" tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemie");

        // Assume that all enemies are inactive initially
        bool allEnemiesInactive = true;

        // Check if any enemy is still active
        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeSelf)
            {
                allEnemiesInactive = false;
                break;
            }
        }

        // If all enemies are inactive, the player wins
        if (allEnemiesInactive)
        {
            UiManager.instance.GameWon();
            AudioManager.Instance.StopMusic("Theme");
            AudioManager.Instance.PlaySFX("GameWon", 0.2f);
        }
    }




    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}
