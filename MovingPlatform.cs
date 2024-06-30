using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform targetA, targetB;
    [SerializeField] float speed = 500f; 
    private bool switching = false;
    private Rigidbody rb;

    private void Start()
    {
       rb=GetComponent<Rigidbody>();
    }
    void Update()
    {
       // Debug.Log(Vector3.Distance(transform.position, targetB.position));
        if (!switching)
        {
            //transform.position = Vector3.MoveTowards(transform.position, targetA.position, speed * Time.deltaTime);
            rb.velocity = new Vector3(0, speed, 0) * Time.deltaTime;
        }
        else if (switching)
        {
            //transform.position = Vector3.MoveTowards(transform.position, targetB.position, speed * Time.deltaTime);
            rb.velocity = new Vector3(0, -speed, 0) * Time.deltaTime;
        }
        if (Vector3.Distance(transform.position, targetB.position) <= 1)
        {
            switching = true;
        }
        else if (Vector3.Distance(transform.position, targetA.position) <= 1f)
        {
            //Debug.Log("switch");
            switching = false;
        }
    }
}

