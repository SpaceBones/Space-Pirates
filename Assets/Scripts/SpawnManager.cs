using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField] private GameObject _enemy;
	[SerializeField] private GameObject _tripleshot;
	[SerializeField] private GameObject _newEnemy;
	[SerializeField] private GameObject _newPower;
	[SerializeField] private GameObject _containerEnemy;
	[SerializeField] private GameObject _containerPower;
	private bool _stopSpawning = false;
	private string[] _directions = { "north", "south", "east", "west" };
	private Vector3 _spawnPos = new Vector3(0.0f, 0.0f, 0.0f);
	
	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(SpawnEnemyCoroutine(Random.Range(1.0f, 6.0f)));
		StartCoroutine(SpawnPowerupCoroutine(Random.Range(10.0f, 15.0f)));
	}

	// Update is called once per frame
	void Update()
	{

	}

	IEnumerator SpawnEnemyCoroutine(float wait)
	{
		while (_stopSpawning == false)
		{
			yield return new WaitForSeconds(wait);
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

	IEnumerator SpawnPowerupCoroutine(float wait)
	{
		while (_stopSpawning == false)
		{
			yield return new WaitForSeconds(wait);
			_spawnPos = new Vector3(Random.Range(-11.0f, 11.0f), 6.5f, 0.0f);
			_newPower = Instantiate(_tripleshot, _spawnPos, Quaternion.identity);
			_newPower.transform.parent = _containerPower.transform;
		}
	}

	public void OnPlayerDeath()
	{
		_stopSpawning = true;
	}
}
