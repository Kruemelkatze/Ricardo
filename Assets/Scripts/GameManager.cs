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

	public void TookPhoto(Texture2D photo, bool isOnPhoto)
	{
		if (isOnPhoto)
		{
			bombedPhotos.Add(photo);
			score += scorePerBombedPhoto;
		}
	}
}
