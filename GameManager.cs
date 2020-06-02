using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public int stage = 0;

    EnemiesInstantiate enemiesInstantiate;
    public GameObject gameOver;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI stageText;
    [SerializeField]
    public GameObject[] lifeDisplay;
    int points = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            if(SceneManager.GetActiveScene().name != "Menu")
            {
                Setup();
            }
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Setup()
    {
        enemiesInstantiate = GameObject.FindObjectOfType<EnemiesInstantiate>();
        gameOver = GameObject.Find("GameOver");
        gameOver.SetActive(false);
        scoreText = GameObject.Find("ScoreNb").GetComponent<TextMeshProUGUI>();
        scoreText.text = points.ToString();
        stageText = GameObject.Find("StageNb").GetComponent<TextMeshProUGUI>();
        stageText.text = stage.ToString();
        lifeDisplay = GameObject.FindGameObjectsWithTag("LifeTag");
        for (int i = 0; i < lifeDisplay.Length; i++)
        {
            lifeDisplay[i].GetComponent<Image>().enabled = true;
        }
        Time.timeScale = 1;
    }

    private void Update()
    {
        if(gameOver != null && gameOver.active)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                NextStage();
            }
        }
    }

    public void AddPoints(int newPoints)
    {
        points += newPoints;
        scoreText.text = points.ToString();
    }

    public void UpdateLife(int currentLife)
    {
        for (int i = 0; i < lifeDisplay.Length; i++)
        {
            if(i == currentLife)
            {
                lifeDisplay[i].GetComponent<Image>().enabled = false;
            }
        }
    }

    public void GameOver(int qntEnemies)
    {
        gameOver.SetActive(true);
        TextMeshProUGUI text = GameObject.Find("Message").GetComponent<TextMeshProUGUI>();

        //se ainda existem inimigos, quer dizer que o player perdeu. Ou foi atingido, ou os inimigos chegaram perto demais
        if (qntEnemies > 0)
        {
            text.SetText("Game Over");
            ResetStatusGame();
        }
        else
        {
            text.SetText("You Win");
        }

        Time.timeScale = 0.0f;
    }

    public void NextStage()
    {
        stage++;
        if (enemiesInstantiate.TemMaisFases(stage))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Setup();
        }
        else
        {
            GoToMenu();
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Cena1");
        Setup();

    }

    public void ResetStatusGame()
    {
        stage = 0;
        points = 0;
    }
}
