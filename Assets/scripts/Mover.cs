using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    float forward;
    float sideways;
    private Rigidbody body;
    public float speed = 1.0f;

    private void Start () {
        body = GetComponent<Rigidbody>();
	}
	
	private void Update () {
        forward = Input.GetAxis("Vertical");
        sideways = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        Vector3 movement = (transform.right * sideways + transform.forward * forward).normalized * speed * Time.deltaTime;
        Debug.Log("" + transform.forward + " " + forward+ " " +speed + " " + Time.deltaTime);

        // Apply this movement to the rigidbody's position.
        body.MovePosition(body.position + movement);
    }
}
