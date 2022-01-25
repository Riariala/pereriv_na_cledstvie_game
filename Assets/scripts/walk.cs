using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walk : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 newPosition;
    private Vector3 touchPosition;
    private float speed =  0.2f;
    //private Vector3 postn;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        touchPosition = transform.position;
        touchPosition.y = 0;
        //postn = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Debug.Log("Touch 1!");
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;
            touchPosition.y = 0;
        }
        if (Input.touchCount == 0)
        {
            Debug.Log("Touch 1!");
            touchPosition = Vector3.zero;
        } 
        newPosition = Vector3.MoveTowards(newPosition, touchPosition, speed);
        transform.position = newPosition;
        //rb.velocity = new Vector3 (speed/10, 0f,0f);
        // if (Input.touchCount == 1)
        // {
        //     Debug.Log("Touch 1!");
        //     rb.velocity = new Vector3 (speed, 0f,0f);
        // }
        if (Input.touchCount == 2)
        {
            Debug.Log("Touch 2!");
            rb.velocity = new Vector3 (0f, speed,0f);
        }
        // if (Input.touchCount == 0)
        // {
        //     Debug.Log("Touch 0!");
        //     rb.velocity = new Vector3 (-speed, -speed,0f);
        //     while (GetComponent<Transform>().position != postn)
        //     {
        //         rb.velocity = new Vector3 (-speed, -speed,0f);
        //     }
        // }
    }
}
