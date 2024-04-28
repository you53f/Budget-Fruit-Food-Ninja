using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public GameObject pauseScreen;
    public Button restartButton;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI overText;
    public GameObject titleScreen;
    public bool isGameActive;
    int score = 0;
    int lives = 3;
    private float spawnRate = 3;
    private bool paused;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0,targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives()
    {
        lives -=1;
        livesText.text = "Lives Remaining: " + lives;
        if(lives == 0)
        {GameOver();}
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        overText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        spawnRate /= difficulty;
        UpdateScore(0);
        StartCoroutine(SpawnTarget());
        titleScreen.gameObject.SetActive(false);
        livesText.text = "Lives Remaining: " + lives;
    }

    void PauseGame()
    {
        if(!paused)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            paused = true;
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
            paused = false;
        }
    }
}
