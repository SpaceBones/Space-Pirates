using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private float _speed = 3.0f;
	private float _yBarrier = 6.55f;
	private float _xBarrier = 11.28f;
	[SerializeField] private Player _player;
	private Animator _animator;
	[SerializeField] private AudioSource _audSource;

	// Start is called before the first frame update
	void Start()
	{
		_player = GameObject.Find("Player").GetComponent<Player>();
		_animator = GetComponent<Animator>();
		_audSource = GetComponent<AudioSource>();
		if (_player == null)
			Debug.LogError("Custom Error: The Player is NULL!");
		else if (_animator == null)
			Debug.LogError("Custom Error: The Animator is NULL!");
		else if (_audSource == null)
			Debug.LogError("Custom Error: The Audio Source is NULL!");
	}

	// Update is called once per frame
	void Update()
	{
		//move in the chosen direction at 4 m/s
		Movement();
		// **EXPERIMENTAL CODE**
		// if you touch the barrier, reappear on another random point on the barrier
		// if (Mathf.Abs(transform.position.y) >= yBarrier || Mathf.Abs(transform.position.x) >= xBarrier)
		// {
		//     if(Random.value < 0.5f) //Moving on the X Axis
		//         transform.position = new Vector3(xBarrier * -1.0f, Random.Range(yBarrier * -1.0f, yBarrier), transform.position.z);
		//     else //Moving on the Y Axis
		//         transform.position = new Vector3(Random.Range(xBarrier * -1.0f, xBarrier), yBarrier, transform.position.z);
		//     _tmpIndex = Random.Range(0, _angleCount);
		//         _tmpAngle = _angles[_tmpIndex];
		//         this.transform.Rotate(0.0f, 0.0f, _tmpAngle, Space.Self);
		// }
	}

	private void Movement()
	{
		transform.Translate(Vector3.down * Time.deltaTime * _speed);
		if (transform.position.y >= _yBarrier || transform.position.y <= (_yBarrier * -1.0f))
			transform.position = new Vector3(Random.Range(_xBarrier * -1.0f, _xBarrier), transform.position.y * -1.0f, transform.position.z);
		if (transform.position.x >= _xBarrier || transform.position.x <= (_xBarrier * -1.0f))
			transform.position = new Vector3(transform.position.x * -1.0f, Random.Range(_yBarrier * -1.0f, _yBarrier), transform.position.z);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Player player = other.transform.GetComponent<Player>();
			if (player != null)
				player.Damage();
		}
		else if (other.tag == "Laser")
		{
			_player.AddScore(100);
			Destroy(other.gameObject);
		}
		GetComponent<Collider2D>().enabled = false;
		_speed = 0.0f;
		_animator.SetTrigger("On_Enemy_Death");
		_audSource.Play();
		Destroy(gameObject, 2);
	}
}