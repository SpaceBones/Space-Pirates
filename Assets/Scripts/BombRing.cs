using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRing : MonoBehaviour
{
	[SerializeField] private float _grow = 25.0f;
	[SerializeField] private float _deathTimer = 0.3f;

	private void Start()
	{
		Destroy(gameObject, _deathTimer);
	}
	void Update()
	{
		transform.localScale += new Vector3(_grow, _grow, _grow) * Time.deltaTime;
	}
}
