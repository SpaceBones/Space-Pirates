using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    private float yBarrier = 6.55f;
    private float xBarrier = 11.28f;

    // Start is called before the first frame update
    void Start()
    {
        //take the current position = new position (x,y,z)
        transform.position = new Vector3 (0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        //Movement
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        
        transform.Translate(new Vector3(hInput, vInput, 0) * Time.deltaTime * _speed);

        //if the current transform meets or exceeds the barrier, spawn them on the opposite side
        if (transform.position.y >= yBarrier || transform.position.y <= (yBarrier * -1.0f))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1.0f, transform.position.z);
        }
        if (transform.position.x >= xBarrier || transform.position.x <= (xBarrier * -1.0f))
        {
            transform.position = new Vector3(transform.position.x * -1.0f, transform.position.y, transform.position.z);
        }
    }
}
