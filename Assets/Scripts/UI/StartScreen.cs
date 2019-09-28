using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public void StartGame()
    {
        StartCoroutine(LoadDeferred(1));
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
    
    IEnumerator LoadDeferred(int scene)
    {
        AudioControl.Instance.PlayRandomSound("towel");
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}