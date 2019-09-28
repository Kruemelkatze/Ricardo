using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int Score;

    public int scorePerBombedPhoto = 100;
    public List<Texture2D> bombedPhotos;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    public int totalTime = 3 * 60;
    public float timeleft;

    private bool gameOver = false;

    // Use this for initialization
    void Start()
    {
        Hub.Register(this);

        Score = 0;
        timeleft = totalTime;
        gameOver = false;

        AudioControl.Instance.PlayDefaultMusic(0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = Score.ToString();
        timeleft -= Time.deltaTime;
        timeleft = Math.Max(0, timeleft);

        var timeStr = $"{(int) (timeleft / 60):00}:{(int) (timeleft % 60):00}";
        timeText.text = timeStr;

        if (!gameOver && Math.Abs(timeleft) < 0.05f)
        {
            gameOver = true;
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameOver = true;
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }

    public void TookPhoto(Phone phone, Texture2D photo, bool isOnPhoto, bool isDoedelOnPhoto, float relativeHeight)
    {
        if (isOnPhoto)
        {
            bombedPhotos.Add(photo);
            var addScore = scorePerBombedPhoto * (1 + relativeHeight / 2);

            var isDoedel = isDoedelOnPhoto && Hub.Get<PlayerMovement>().IsDoedel();

            addScore *= isDoedel ? 2 : 1;

            Score += (int) addScore;

            if (isDoedel)
            {
                AudioControl.Instance.PlayRandomSound("score", 1.2f);
            }
        }

        Hub.Get<PhoneSpawner>().RemovePhone(phone);
    }
}