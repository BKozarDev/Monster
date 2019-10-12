using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;

    //[SerializeField]
    //DrawScript drawScript;

    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    public float dashDistance;

    private Rigidbody rb;
    private Vector3 inputs = Vector3.zero;
    public bool isHunted = false;
    [SerializeField]
    private Animator animator;
    private RaycastHit hit;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.drag = 0.05f;
    }

    private void Update()
    {
        inputs = Vector3.zero;
        inputs.x = Input.GetAxis("Horizontal");
        inputs.z = Input.GetAxis("Vertical");
        if (inputs != Vector3.zero)
        {
            transform.forward = inputs;
            animator.SetFloat("speed", (Mathf.Abs(inputs.x) + Mathf.Abs(inputs.z)) / 2);
        }
        if(inputs != Vector3.zero)
            Dash();

        //if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, groundLayer))
        //{
        //    hit.collider.GetComponent<DrawScript>().DrawOnPoint(hit.textureCoord);
        //}
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + inputs * speed * Time.fixedDeltaTime);
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log(Vector3.Scale(transform.position, dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * rb.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * rb.drag + 1)) / -Time.deltaTime))));
            Vector3 dashVel = Vector3.Scale(transform.forward, dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * rb.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * rb.drag + 1)) / -Time.deltaTime)));
            rb.AddForce(transform.forward * dashDistance, ForceMode.VelocityChange);
            //animator.SetBool("isDash", true);
            //speed = 10f;
            // Minus stamina
        }
        //else
        //{
        //    animator.SetBool("isDash", false);
        //    speed = 5f;
        //    // Plus stamina
        //}
    }
}
