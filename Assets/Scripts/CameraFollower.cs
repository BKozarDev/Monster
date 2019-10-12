using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 offset = new Vector3(0f,4f,-2f);
    [SerializeField]
    private float followingSpeed = 1f;
    [Range(0f,1f)]
    [SerializeField]
    private float followingTreshold = 0.01f;

    void Update()
    {
        if ((target.position - transform.position).magnitude <= followingTreshold)
            return;
        var offsetedTarget = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, offsetedTarget, Time.deltaTime* followingSpeed);
    }
}
