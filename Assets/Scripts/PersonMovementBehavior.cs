using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonMovementBehavior : MonoBehaviour
{
    public float speed;
    public float speedRotation;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Walk(Vector3 input, Vector3 lookAt)
    {
        //Movement
        Vector3 velocity = rb.velocity;

        velocity.x += input.x * speed * Time.deltaTime;
        velocity.z += input.y * speed * Time.deltaTime;

        rb.velocity = velocity;

        Quaternion targetRotation = Quaternion.LookRotation(lookAt - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
    }
}
