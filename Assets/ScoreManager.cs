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
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemie").Length;

        if (score == enemyCount)
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
