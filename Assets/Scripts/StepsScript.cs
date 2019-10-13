using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepsScript : MonoBehaviour
{
    public float stepsOffset = 0.3f;
    public float stepsTimer = 0.1f;
    public LayerMask groundLayer;
    public bool draw;

    private float timer;
    private int offsetSide;
    private RaycastHit hit;
    private Vector3 stepPosition;
    private Vector3 prevPosition;

    void Start()
    {
        offsetSide = 1;
        timer = stepsTimer;
    }

    void Update()
    {
        if (transform.position == prevPosition)
        {
            return;
        }
        prevPosition = transform.position;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            stepPosition = transform.position + transform.right * stepsOffset * offsetSide;
            if (Physics.Raycast(stepPosition, Vector3.down, out hit, 1f, groundLayer))
            {
                hit.collider.GetComponent<DrawScript>().DrawOnPoint(hit.textureCoord);
            }
            timer = stepsTimer;
            offsetSide *= -1;
        }
    }
}
