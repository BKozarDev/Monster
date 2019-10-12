using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public float speed;
    public LayerMask groundLayer;
    public DrawScript drawScript;

    private Rigidbody rb;
    private RaycastHit hit;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (rb.velocity.z < speed)
                rb.AddForce(Vector3.forward * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (rb.velocity.z < speed)
                rb.AddForce(Vector3.back * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (rb.velocity.x < speed)
                rb.AddForce(Vector3.left * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (rb.velocity.x < speed)
                rb.AddForce(Vector3.right * speed);
        }

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, groundLayer))
        {
            drawScript.DrawOnPoint(hit.textureCoord);
        }
    }
}
