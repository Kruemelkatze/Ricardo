using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighscoreScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public void StartGame()
    {
        StartCoroutine(LoadDeferred(1));
    }

    public void ToMainMenu()
    {
        StartCoroutine(LoadDeferred(0));
    }

    IEnumerator LoadDeferred(int scene)
    {
        AudioControl.Instance.PlayRandomSound("towel");
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    private void Start()
    {
        scoreText.text = GameManager.Score.ToString();
        AudioControl.Instance.PlayRandomSound("gameover", 1.2f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(LoadDeferred(0));
        }
    }
}