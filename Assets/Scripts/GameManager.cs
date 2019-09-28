using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public int score;

	public int scorePerBombedPhoto = 100;
	public List<Texture2D> bombedPhotos; 
	
	// Use this for initialization
	void Start () {
		Hub.Register(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TookPhoto(Phone phone, Texture2D photo, bool isOnPhoto, float relativeHeight)
	{
		if (isOnPhoto)
		{
			bombedPhotos.Add(photo);
			var addScore = scorePerBombedPhoto * (1 + relativeHeight / 2);

			var isDoedel = Hub.Get<PlayerMovement>().IsDoedel();

			addScore += isDoedel ? 2 : 1;
			
			score += (int) addScore;
		}
		
		Hub.Get<PhoneSpawner>().RemovePhone(phone);
	}
}
