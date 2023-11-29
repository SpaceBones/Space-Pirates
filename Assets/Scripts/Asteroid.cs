using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
	[SerializeField] private float _rotSpeed = 1.8f;
	[SerializeField] private GameObject _explosion;
	[SerializeField] private SpawnManager _spawnManager;
	// Start is called before the first frame update
	void Start()
	{
		_spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
		if (_spawnManager == null)
			Debug.LogError("The Spawn Manager is NULL!");
	}

	// Update is called once per frame
	void Update()
	{
		//rotate on z-axis
		transform.Rotate(Vector3.forward * _rotSpeed * Time.deltaTime, Space.Self);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Laser")
		{
			_spawnManager.StartSpawning();
			Instantiate(_explosion, transform.position, Quaternion.identity);
			Destroy(other.gameObject);
			Destroy(gameObject, 0.3f);
		}
	}
}
