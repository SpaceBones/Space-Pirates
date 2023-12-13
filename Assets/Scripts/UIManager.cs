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
	private GameManager _gameManager;
	// Start is called before the first frame update
	void Start()
	{
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (_gameManager == null)
			Debug.LogError("Game Manager is NULL!");
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

	public void GameOverSequence()
	{
		StartCoroutine(GameOverCoroutine());
		_restart.enabled = true;
		_gameManager.GameOver();
	}

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
}
