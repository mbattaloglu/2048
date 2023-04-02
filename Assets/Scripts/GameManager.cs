using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TileBoard board;
    public CanvasGroup gameOverUI;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private int score;
    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        SetScore(0);
        highScoreText.text = LoadHighScore().ToString();
        gameOverUI.alpha = 0;
        gameOverUI.interactable = false;

        board.ClearTile();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }

    public void GameOver()
    {
        board.enabled = false;
        gameOverUI.interactable = true;
        StartCoroutine(Fade(gameOverUI, 1f, 1f));
    }

    public IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay)
    {
        yield return new WaitForSeconds(delay);
        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;
        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int points)
    {
        SetScore(score + points);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
        SaveHighScore();
    }

    private void SaveHighScore()
    {
        int highscore = LoadHighScore();

        if (score > highscore)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }

    private int LoadHighScore()
    {
        return PlayerPrefs.GetInt("highscore", 0);
    }
}
