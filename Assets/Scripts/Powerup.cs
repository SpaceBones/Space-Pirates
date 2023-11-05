using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
	[SerializeField] private float _speed = 3.0f;
	private float _yBarrier = -6.55f;
	[SerializeField] private int _powerupID; // 1 = Speed | 2 = Shield | 3 = Triple Shot
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		transform.Translate(Vector3.down * Time.deltaTime * _speed);
		if (transform.position.y < _yBarrier)
			Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Player player = other.transform.GetComponent<Player>();
			if (player != null)
			{
				player.Powerup(_powerupID);
				Destroy(gameObject);
			}
		}
	}
}


//move down at a speed of 3
//delete when you leave the screen or collected
//collision checks (player only)