using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField] private GameObject _enemy;
	[SerializeField] private GameObject _tripleshotPower;
	[SerializeField] private GameObject _speedPower;
	[SerializeField] private GameObject _shieldPower;
	[SerializeField] private GameObject _healthPower;
	[SerializeField] private GameObject _ammoPower;
	[SerializeField] private GameObject _bombPower;
	private GameObject _newEnemy;
	private GameObject _newPower;
	[SerializeField] private GameObject _containerEnemy;
	[SerializeField] private GameObject _containerPower;
	private bool _stopSpawning = false;
	private string[] _directions = { "north", "south", "east", "west" };
	private Vector3 _spawnPos = new Vector3(0.0f, 0.0f, 0.0f);
	[SerializeField] private GameObject[] _powerups;
	
	// Start is called before the first frame update
	void Start()
	{
		
	}

	public void StartSpawning()
	{
		StartCoroutine(SpawnEnemyCoroutine());
		StartCoroutine(SpawnPowerupCoroutine());
		StartCoroutine(SpawnAmmoCoroutine()); //There is no way I'm giving the player a 25% chance every 10-15 Seconds to get ammo, I'm giving it it's own coroutine
		StartCoroutine(SpawnBombCoroutine());
	}

	IEnumerator SpawnEnemyCoroutine()
	{
		while (_stopSpawning == false)
		{
			yield return new WaitForSeconds(Random.Range(1.0f, 6.0f));
			int x = Random.Range(0, 4);
			string result = _directions[x];
				switch (result)
				{
					case "north":
						_spawnPos = new Vector3(Random.Range(-11.0f, 11.0f), 6.5f, 0);
						_newEnemy = Instantiate(_enemy, _spawnPos, Quaternion.identity);
						break;
					case "south":
						_spawnPos = new Vector3(Random.Range(-11.0f, 11.0f), -6.5f, 0);
						_newEnemy = Instantiate(_enemy, _spawnPos, Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f)));
						break;
					case "east":
						_spawnPos = new Vector3(11.2f, Random.Range(-5.5f, 5.5f), 0);
						_newEnemy = Instantiate(_enemy, _spawnPos, Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f)));
					break;
					case "west":
						_spawnPos = new Vector3(-11.2f, Random.Range(-5.5f, 5.5f), 0);
						_newEnemy = Instantiate(_enemy, _spawnPos, Quaternion.Euler(new Vector3(0.0f, 0.0f, -90.0f)));
					break;
				}
			_newEnemy.transform.parent = _containerEnemy.transform;
		}

	}

	IEnumerator SpawnPowerupCoroutine()
	{
		while (_stopSpawning == false)
		{
			yield return new WaitForSeconds(Random.Range(10.0f, 15.0f));
			int i = Random.Range(0, 4);
			_spawnPos = new Vector3(Random.Range(-10.0f, 10.0f), 6.5f, 0.0f);
			_newPower = Instantiate(_powerups[i], _spawnPos, Quaternion.identity);
			_newPower.transform.parent = _containerPower.transform;
		}
	}

	IEnumerator SpawnAmmoCoroutine()
	{
		while(_stopSpawning == false)
		{
			yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));
			_spawnPos = new Vector3(Random.Range(-10.0f, 10.0f), 6.5f, 0.0f);
			_newPower = Instantiate(_powerups[4], _spawnPos, Quaternion.identity);
			_newPower.transform.parent = _containerPower.transform;
		}
	}

	IEnumerator SpawnBombCoroutine()
	{
		while (_stopSpawning == false)
		{
			yield return new WaitForSeconds(Random.Range(20.0f, 30.0f));
			_spawnPos = new Vector3(Random.Range(-10.0f, 10.0f), 6.5f, 0.0f);
			_newPower = Instantiate(_powerups[5], _spawnPos, Quaternion.identity);
			_newPower.transform.parent = _containerPower.transform;
		}
	}

	public void OnPlayerDeath()
	{
		_stopSpawning = true;
	}
}
