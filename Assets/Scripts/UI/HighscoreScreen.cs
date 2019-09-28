using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighscoreScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public void StartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    private void Start()
    {
        scoreText.text = GameManager.Score.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}