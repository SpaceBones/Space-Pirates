using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _speed = 15.0f;
    private float yBarrier = 6.55f;
    private float xBarrier = 11.28f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //translate laser up by a speed
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if (transform.position.y >= yBarrier || transform.position.y <= (yBarrier * -1.0f))
            Destroy(gameObject);
        if (transform.position.x >= xBarrier || transform.position.x <= (xBarrier * -1.0f))
            Destroy(gameObject);
    }
}
