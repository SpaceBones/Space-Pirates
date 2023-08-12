using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _speed = 15.0f;
    private float yBarrier = 6.55f;
    private float xBarrier = 11.28f;

    // Update is called once per frame
    void Update()
    {
        //translate laser up by a speed
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        //delete the laser if it goes full screen
        if (transform.position.y >= yBarrier || transform.position.y <= (yBarrier * -1.0f))
            Destroy(gameObject);
        if (transform.position.x >= xBarrier || transform.position.x <= (xBarrier * -1.0f))
            Destroy(gameObject);
    }
}
