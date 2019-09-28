using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	public static int Score;

	public int scorePerBombedPhoto = 100;
	public List<Texture2D> bombedPhotos;

	public TextMeshProUGUI scoreText;
	
	// Use this for initialization
	void Start () {
		Hub.Register(this);
		Score = 0;
		AudioControl.Instance.PlayDefaultMusic(0.3f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		scoreText.text = Score.ToString();
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
		}
		
		Hub.Get<PhoneSpawner>().RemovePhone(phone);
	}
}
