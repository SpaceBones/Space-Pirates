using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	[SerializeField] private float _speed = 25.0f;
	[SerializeField] private GameObject _bombRing;
	private float _yBarrier = 6.55f;
	private float _xBarrier = 11.28f;
	// Start is called before the first frame update
	void Update()
	{
		transform.Translate(Vector3.up * _speed * Time.deltaTime);
		_speed -= 0.17f;
		
		if (_speed < 1.0f)
		{
			Detonate();
		}

		if (transform.position.y >= _yBarrier || transform.position.y <= (_yBarrier * -1.0f))
			transform.position = new Vector3(transform.position.x, transform.position.y * -1.0f, transform.position.z);
		if (transform.position.x >= _xBarrier || transform.position.x <= (_xBarrier * -1.0f))
			transform.position = new Vector3(transform.position.x * -1.0f, transform.position.y, transform.position.z);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			Detonate();
		}
	}

	private void Detonate()
	{
		Instantiate(_bombRing, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
