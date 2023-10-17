using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject newEnemy;
    [SerializeField] private GameObject _container;
    private bool _stopSpawning = false;
    private string[] directions = {"north", "south", "east", "west"};
    private Vector3 spawnPos = new Vector3(0.0f, 0.0f, 0.0f);
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
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(wait);
            int x = Random.Range(0, 4);
            if(directions[x] == "north")
            {
                spawnPos = new Vector3(Random.Range(-11.0f, 11.0f), 6.5f, 0);
                newEnemy = Instantiate(_enemy, spawnPos, Quaternion.identity);
            }
            else if(directions[x] == "south")
            {
                spawnPos = new Vector3(Random.Range(-11.0f, 11.0f), -6.5f, 0);
                newEnemy = Instantiate(_enemy, spawnPos, Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f)));
            }
            else if (directions[x] == "east")
            {
                spawnPos = new Vector3(11.2f, Random.Range(-5.5f, 5.5f), 0);
                newEnemy = Instantiate(_enemy, spawnPos, Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f)));
            }
            else if (directions[x] == "west")
            {
                spawnPos = new Vector3(-11.2f, Random.Range(-5.5f, 5.5f), 0);
                newEnemy = Instantiate(_enemy, spawnPos, Quaternion.Euler(new Vector3(0.0f, 0.0f, -90.0f)));
            }
            newEnemy.transform.parent = _container.transform;
        }

    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
