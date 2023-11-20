using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
	[SerializeField] private TMP_Text _scoreText;
	[SerializeField] private Sprite[] _liveSprites;
	[SerializeField] private Image _livesImg;
	// Start is called before the first frame update
	void Start()
	{
		_scoreText.SetText("Score: 0");
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void UpdateScore(int score)
	{
		_scoreText.SetText("Score: " + score);
	}

	public void UpdateLives(int currentLives)
	{
		_livesImg.sprite = _liveSprites[currentLives];
	}

	//update score
}
