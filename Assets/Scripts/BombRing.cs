using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRing : MonoBehaviour
{
	[SerializeField] private float _grow = 15.0f;
	[SerializeField] private float _deathTimer = 0.2f;

	private void Start()
	{
		Destroy(gameObject, _deathTimer);
	}

	//Grow in size
	void Update()
	{
		transform.localScale += new Vector3(_grow, _grow, _grow) * Time.deltaTime;
	}
}
