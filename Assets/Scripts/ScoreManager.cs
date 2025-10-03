using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score = 0;
    private int totalEnemies = 0;

    [Header("UI Elements")]
    public TMP_Text scoreText; //  Score Text 
    public TMP_Text winText;   // "You Win!" 

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (winText != null)
            winText.gameObject.SetActive(false); // hide at start

        // Count total enemies in the scene
        GameObject enemyParent = GameObject.Find("ENEMY");
        if (enemyParent != null)
        {
            totalEnemies = 0;
            foreach (Transform child in enemyParent.transform)
            {
                if (child.gameObject.activeInHierarchy)
                    totalEnemies++;
            }
        }

        Debug.Log("ScoreManager Awake: Total Enemies = " + totalEnemies);
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
        Debug.Log("AddScore called: Current Score = " + score);
        CheckWinCondition();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    void CheckWinCondition()
    {
        Debug.Log("CheckWinCondition called: score = " + score + ", totalEnemies = " + totalEnemies);
        if (score >= totalEnemies && winText != null)
        {
            Debug.Log("You Win!");
            winText.gameObject.SetActive(true); // GUI win message
            Time.timeScale = 0f;
        }
    }
}
