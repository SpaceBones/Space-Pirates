using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCoroutine(Random.Range(1.0f, 6.0f)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnCoroutine(float wait)
    {
        while (true)
        {
            yield return new WaitForSeconds(wait);
            Vector3 spawnPos = new Vector3(Random.Range(-11.0f, 11.0f), 6.55f, 0);
            Instantiate(_enemy, spawnPos, Quaternion.identity);
        }

    }
}
