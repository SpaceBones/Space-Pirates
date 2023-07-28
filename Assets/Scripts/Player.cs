using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    //It's high because it's basically "how many degrees per second am I rotating?"
    [SerializeField] private float _rotationSpeed = 250.0f;
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
        //AlternateMovement();
    }

    void CalculateMovement()
    {
        //Movement
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        
        //The Top is forward and backward movement, while the Bottom is left and right rotation
        transform.Translate(new Vector3(0, vInput, 0) * Time.deltaTime * _speed);
        transform.Rotate(new Vector3(0, 0, hInput * -1) * Time.deltaTime * _rotationSpeed, Space.Self);

        //If the current transform meets or exceeds the barrier, spawn them on the opposite side
        if (transform.position.y >= yBarrier || transform.position.y <= (yBarrier * -1.0f))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1.0f, transform.position.z);
        }
        if (transform.position.x >= xBarrier || transform.position.x <= (xBarrier * -1.0f))
        {
            transform.position = new Vector3(transform.position.x * -1.0f, transform.position.y, transform.position.z);
        }

        //Boost Button?
    }

// A scrapped idea that I really liked finding the answer to, so I left it in.
//Basically; How to make the player follow the mouse. Could be used for an alternate control scheme
    // void AlternateMovement()
    // {
    //     var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
    //     var angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg * -1.0f;
    //     transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0,0,1));
    // }
}
