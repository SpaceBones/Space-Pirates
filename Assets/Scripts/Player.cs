using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	[SerializeField] private float _speed = 10.0f;
	[SerializeField] private float _boost = 12.0f;
	//It's high because it's basically "how many degrees per second am I rotating?"
	[SerializeField] private float _rotationSpeed = 250.0f;
	private float _yBarrier = 6.55f;
	private float _xBarrier = 11.28f;
	[SerializeField] private GameObject _laser;
	[SerializeField] private float _cooldown = 0.05f;
	private float _canFire = 0.1f;
	[SerializeField] private int _lives = 3;
	[SerializeField] private SpawnManager _spawnManager;
	private bool _activeTriple = false;
	[SerializeField] private GameObject _laserTriple;
	private bool _activeShield = false;
	[SerializeField] private GameObject _shieldVisual;
	private int _score;
	private UIManager _uiManager;


	// Start is called before the first frame update
	void Start()
	{
		//take the current position = new position (x,y,z)
		transform.position = new Vector3(0, 0, 0);
		_spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
		_uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
		if (_spawnManager == null)
		{
			Debug.LogError("The Spawn Manager is Null!");
		}
		else if (_uiManager == null)
		{
			Debug.LogError("The UI Manager is Null!");
		}
	}

	// Update is called once per frame
	void Update()
	{
		CalculateMovement();
		//AlternateMovement();
		if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > _canFire)
			FireLaser();
	}

	void CalculateMovement()
	{
		//Movement
		float hInput = Input.GetAxis("Horizontal");
		float vInput = Input.GetAxis("Vertical");

		//The Top is forward and backward movement in addition to boost, while the Bottom is left and right rotation
		if (Input.GetKey(KeyCode.Mouse1) == true || Input.GetKey(KeyCode.LeftShift) == true)
			transform.Translate(new Vector3(0, vInput, 0) * Time.deltaTime * _boost);
		else
			transform.Translate(new Vector3(0, vInput, 0) * Time.deltaTime * _speed);
		transform.Rotate(new Vector3(0, 0, hInput * -1) * Time.deltaTime * _rotationSpeed, Space.Self);

		//If the current transform meets or exceeds the barrier, spawn them on the opposite side
		if (transform.position.y >= _yBarrier || transform.position.y <= (_yBarrier * -1.0f))
			transform.position = new Vector3(transform.position.x, transform.position.y * -1.0f, transform.position.z);
		if (transform.position.x >= _xBarrier || transform.position.x <= (_xBarrier * -1.0f))
			transform.position = new Vector3(transform.position.x * -1.0f, transform.position.y, transform.position.z);

	}

	// A scrapped idea that I really liked finding the answer to, so I left it in.
	//Basically; How to make the player follow the mouse. Could be used for an alternate control scheme
	// void AlternateMovement()
	// {
	//     var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
	//     var angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg * -1.0f;
	//     transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0,0,1));
	// }

	void FireLaser()
	{
		//If you press a button, spawn laser
		if (_activeTriple == true)
		{
			Instantiate(_laserTriple, transform.position, transform.rotation);
			_canFire = Time.time + (_cooldown * 1.5f);
		}
		else
		{
			Instantiate(_laser, transform.position, transform.rotation);
			_canFire = Time.time + _cooldown;
		}
	}

	public void Damage()
	{
		if (_activeShield == true)
		{
			_activeShield = false;
			_shieldVisual.SetActive(false);
		}
		else
		{
			_lives--;
			_uiManager.UpdateLives(_lives);
		}
		if (_lives < 1)
		{
			Destroy(gameObject);
			//tell Spawn Manager to stop spawning
			_spawnManager.OnPlayerDeath();
		}
	}

	public void Powerup(int power)
	{
		if (power == 3)
		{
			_activeTriple = true;
			StartCoroutine(TripleshotPowerdownCoroutine(8.0f));
		}
		else if (power == 2)
		{
			_activeShield = true;
			_shieldVisual.SetActive(true);
			//when you are next hit, shield is removed instead of health
		}
		else if (power == 1)
		{
			_speed += 3.0f;
			_rotationSpeed += 30.0f;
			_boost += 5.0f;
			StartCoroutine(SpeedPowerdownCoroutine(8.0f));
		}
		AddScore(300);
	}

	IEnumerator TripleshotPowerdownCoroutine(float wait)
	{
		yield return new WaitForSeconds(wait);
		_activeTriple = false;
	}

	IEnumerator SpeedPowerdownCoroutine(float wait)
	{
		yield return new WaitForSeconds(wait);
		_speed -= 3.0f;
		_rotationSpeed -= 30.0f;
		_boost -= 5.0f;
	}

	public void AddScore(int points)
	{
		_score += points;
		_uiManager.UpdateScore(_score);
	}
}
