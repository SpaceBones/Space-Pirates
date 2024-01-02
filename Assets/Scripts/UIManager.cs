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
	[SerializeField] private TMP_Text _gameOver;
	[SerializeField] private TMP_Text _restart;
	[SerializeField] private TMP_Text _ammoText;
	[SerializeField] private Slider _thrustSlider;
	private GameManager _gameManager;
	// Start is called before the first frame update
	void Start()
	{
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (_gameManager == null)
			Debug.LogError("Game Manager is NULL!");
		_scoreText.SetText("Score: 0");
		_ammoText.SetText("Ammo: 15");
	}

	public void UpdateScore(int score)
	{
		_scoreText.SetText("Score: " + score);
	}

	public void UpdateLives(int currentLives)
	{
		_livesImg.sprite = _liveSprites[currentLives];
	}

	public void GameOverSequence()
	{
		StartCoroutine(GameOverCoroutine());
		_restart.enabled = true;
		_gameManager.GameOver();
	}

	public void UpdateThrust(float value)
	{
		if (value < 0)
			_thrustSlider.value = 0;
		else if (value > 1)
			_thrustSlider.value = 1;
		else
			_thrustSlider.value = value;
	}

	//A classic retro GAME OVER Blink
	IEnumerator GameOverCoroutine()
	{
		while (true)
		{
			_gameOver.enabled = true;
			yield return new WaitForSeconds(.75f);
			_gameOver.enabled = false;
			yield return new WaitForSeconds(.75f);
		}
	}

	public void UpdateAmmo(int ammo)
	{
		_ammoText.SetText("Ammo: " + ammo);
	}
}
