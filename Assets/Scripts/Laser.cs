using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	[SerializeField] private float _speed = 15.0f;
	private float _yBarrier = 6.55f;
	private float _xBarrier = 11.28f;
	public bool _isEnemyLaser = false;

	// Update is called once per frame
	void Update()
	{
		if (_isEnemyLaser == false)
			transform.Translate(Vector3.up * _speed * Time.deltaTime);
		else if (_isEnemyLaser == true)
			transform.Translate(Vector3.down * _speed * Time.deltaTime);
		if (transform.position.y >= _yBarrier || transform.position.y <= (_yBarrier * -1.0f))
		{
			if (transform.parent != null)
				Destroy(transform.parent.gameObject);
			Destroy(gameObject);
		}
		else if (transform.position.x >= _xBarrier || transform.position.x <= (_xBarrier * -1.0f))
		{
			if (transform.parent != null)
				Destroy(transform.parent.gameObject);
			Destroy(gameObject);
		}
	}

	public void AssignEnemyLaser()
	{
		_isEnemyLaser = true;
	}
}
