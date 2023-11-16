using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
	[SerializeField] private TMP_Text _scoreText;
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

	//update score
}
