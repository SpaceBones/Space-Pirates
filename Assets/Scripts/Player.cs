using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
	[SerializeField] private float _speed = 10.0f;
	[SerializeField] private float _boostMultiplier = 1.7f;
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
	private bool _activeBomb = false;
	[SerializeField] private GameObject _laserTriple;
	[SerializeField] private GameObject _laserBomb;
	private bool _activeShield = false;
	[SerializeField] private GameObject _shieldVisual;
	[SerializeField] private SpriteRenderer _shieldSpriteRenderer;
	private int _score;
	private UIManager _uiManager;
	[SerializeField] private GameObject _damage_l;
	[SerializeField] private GameObject _damage_r;
	[SerializeField] private AudioSource _laserSound;
	private int _shieldHP = 0;
	private int _ammo = 15;
	private float _thrusters = 1.0f;
	private float _thrustCooldown = 2.0f;
	private float _canThrust = 0.1f;
	[SerializeField] private Animator _animator;

	// Start is called before the first frame update
	void Start()
	{
		transform.position = new Vector3(0, -1.0f, 0);
		_spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
		_uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
		_laserSound = GetComponent<AudioSource>();
		if (_spawnManager == null)
		{
			Debug.LogError("The Spawn Manager is Null!");
		}
		if (_uiManager == null)
		{
			Debug.LogError("The UI Manager is Null!");
		}
		if (_laserSound == null)
		{
			Debug.LogError("The Laser Sound is Null!");
		}
	}

	// Update is called once per frame
	void Update()
	{
		CalculateMovement();
		//AlternateMovement();
		if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > _canFire && _ammo > 0)
			FireLaser();
	}

	void CalculateMovement()
	{
		//Movement
		float hInput = Input.GetAxis("Horizontal");
		float vInput = Input.GetAxis("Vertical");

		//The Top is forward and backward movement in addition to boost, while the Bottom is left and right rotation
		if ((Input.GetKey(KeyCode.Mouse1) == true || Input.GetKey(KeyCode.LeftShift) == true) && Time.time > _canThrust && _thrusters > 0)
		{
			transform.Translate(new Vector3(0, vInput, 0) * Time.deltaTime * _speed * _boostMultiplier);
			_thrusters -= 0.001f;
		}
		else if ((Input.GetKey(KeyCode.Mouse1) == true || Input.GetKey(KeyCode.LeftShift) == true) && Time.time < _canThrust)
			transform.Translate(new Vector3(0, vInput, 0) * Time.deltaTime * _speed);
		else
		{
			transform.Translate(new Vector3(0, vInput, 0) * Time.deltaTime * _speed);
			if (_thrusters < 1 && Time.time > _canThrust)
				_thrusters += 0.002f;
		}
		transform.Rotate(new Vector3(0, 0, hInput * -1) * Time.deltaTime * _rotationSpeed, Space.Self);
		if (_thrusters <= 0)
			ThrusterCooldown();
		_uiManager.UpdateThrust(_thrusters);

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

	//When called, fire a laser based on current Power Up
	//Priority: Bomb, Tripleshot, Basic
	void FireLaser()
	{
		_ammo--;
		_uiManager.UpdateAmmo(_ammo);
		if (_activeTriple == true && _activeBomb == false)
		{
			Instantiate(_laserTriple, transform.position, transform.rotation);
			_canFire = Time.time + (_cooldown + 0.5f);
		}
		else if (_activeBomb == true)
		{
			Instantiate(_laserBomb, transform.position, transform.rotation);
			_canFire = Time.time + (_cooldown + 1.5f);
		}
		else
		{
			Instantiate(_laser, transform.position, transform.rotation);
			_canFire = Time.time + _cooldown;
		}
		_laserSound.Play(0);
	}

	public void Damage()
	{
		if (_activeShield == true)
		{
			ShieldDamage();
			AddScore(100);
		}
		else
		{
			_lives--;
			ShowDamage();
			_uiManager.UpdateLives(_lives);
			_animator.SetTrigger("Player Damaged");
		}
		if (_lives < 1)
		{
			Destroy(gameObject);
			//tell Spawn Manager to stop spawning
			_spawnManager.OnPlayerDeath();
			_uiManager.GameOverSequence();
		}
	}

	//Function designed to handle taking and healing damage (from the Health Power Up)
	private void ShowDamage()
	{
		switch (_lives)
		{
			case 3:
				_damage_l.SetActive(false);
				_damage_r.SetActive(false);
				break;
			case 2:
				_damage_l.SetActive(true);
				_damage_r.SetActive(false);
				break;
			case 1:
				_damage_r.SetActive(true);
				break;
			default:
				break;
		}
	}

	//1 = Speed, 2 = Shield, 3 = Tripleshot, 4 = HP, 5 = Ammo, 6 = Bomb
	public void Powerup(int power)
	{
		switch(power)
		{
			case 1:
				_speed += 3.0f;
				_rotationSpeed += 30.0f;
				StartCoroutine(SpeedPowerdownCoroutine(8.0f));
				break;
			case 2:
				_activeShield = true;
				_shieldHP = 3;
				_shieldSpriteRenderer.color = Color.white;
				_shieldVisual.SetActive(true);
				break;
			case 3:
				_activeTriple = true;
				StartCoroutine(TripleshotPowerdownCoroutine(8.0f));
				break;
			case 4:
				if (_lives < 3)
				{
					_lives++;
					ShowDamage();
					_uiManager.UpdateLives(_lives);
				}
				break;
			case 5:
				_ammo += 10;
				_uiManager.UpdateAmmo(_ammo);
				break;
			case 6:
				_activeBomb = true;
				StartCoroutine(BombPowerdownCoroutine(5.0f));
				break;
			default:
				break;
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
	}

	IEnumerator BombPowerdownCoroutine(float wait)
	{
		yield return new WaitForSeconds(wait);
		_activeBomb = false;
	}

	public void AddScore(int points)
	{
		_score += points;
		_uiManager.UpdateScore(_score);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Laser")
		{
			if (other.gameObject.GetComponent<Laser>().isEnemyLaser == true)
			{
				Destroy(other.transform.parent.gameObject);
				Damage();
			}
		}
	}

	//Designed to render the shield in different colors based on damage taken, resets if shield power up is collected again
	private void ShieldDamage()
	{
		_shieldHP--;
		switch (_shieldHP)
		{
			case 2:
				_shieldSpriteRenderer.color = Color.yellow;
				break;
			case 1:
				_shieldSpriteRenderer.color = Color.red;
				break;
			case 0:
				_activeShield = false;
				_shieldVisual.SetActive(false);
				_shieldSpriteRenderer.color = Color.white;
				break;
			default:
				break;
		}
	}

	private void ThrusterCooldown()
	{
		_thrusters = 0.01f;
		_canThrust = Time.time + _thrustCooldown;
	}
}