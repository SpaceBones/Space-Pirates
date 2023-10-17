using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    private float yBarrier = 6.55f;
    private float xBarrier = 11.28f;
    //private float x_move = 0.0f;
    //private float y_move = 0.0f;
    // private float[] _angles = {0.0f, 45.0f, 90.0f, 135.0f, 180.0f, -225.0f, 270.0f, 315.0f};
    // private int _angleCount = 8;
    // private int _tmpIndex = 0;
    // private float _tmpAngle = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        //if you are on the positive end of the x axis AND your y value
        // if(transform.position.x == 11.20f)
        //     {
        //         x_move = -1.0f;
        //     }
        // else if(transform.position.x == -11.20f)
        //     {
        //         x_move = 1.0f;
        //     }
        // else if(transform.position.y == 6.50f)
        //     {
        //         y_move = -1.0f;
        //     }
        // else if(transform.position.y == -6.50f)
        //     {
        //         y_move = 1.0f;
        //     }
        // Debug.Log("I have spawned at " + transform.position + " and have x_move value of " + x_move + " and a y_move value of " + y_move);
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
        if (transform.position.y >= yBarrier || transform.position.y <= (yBarrier * -1.0f))
            transform.position = new Vector3(Random.Range(xBarrier * -1.0f, xBarrier), transform.position.y * -1.0f, transform.position.z);
        if (transform.position.x >= xBarrier || transform.position.x <= (xBarrier * -1.0f))
            transform.position = new Vector3(transform.position.x * -1.0f, Random.Range(yBarrier * -1.0f, yBarrier), transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
                player.Damage();
            Destroy(gameObject);
        }
        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}