using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        AudioControl.Instance.PlayRandomSound("towel");
    }
    
    public void ExitGame()
    {
        Application.Quit(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }
}