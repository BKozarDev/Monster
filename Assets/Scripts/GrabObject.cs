using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    [SerializeField]
    private Transform handTransform;
    private KeyCode grabKey = KeyCode.F;
    [SerializeField]
    private float grabRadius = 3f;
    [SerializeField]
    private GameObject grabbedObject;
    [SerializeField]
    private bool isGrabbed = false;
    [SerializeField]
    private LayerMask grabObjectLayer;

    private void Update()
    {
        GrabNearObject();
    }

    private void GrabNearObject()
    {
        if (Input.GetKeyDown(grabKey))
        {
            Debug.Log("Grab Key Down");
            if (!isGrabbed)
            {
                Debug.Log("Grab");
                FindNearestObject();
                GrabToHand();
            }
            else
            {
                Debug.Log("UnGrab");
                UnGrab();
            }

        }
    }


    private void FindNearestObject()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, grabRadius, grabObjectLayer);
        float nearestDist = grabRadius;
        foreach (Collider collider in hitColliders)
        {
            float dist = Vector3.Distance(transform.position, collider.transform.position);
            if (nearestDist > dist)
            {
                nearestDist = dist;
                grabbedObject = collider.gameObject;

            }
        }
    }

    private void GrabToHand()
    {
        // Анимация с вытянутыми руками
        Debug.Log("Collider Size : " + grabbedObject.GetComponent<Collider>().bounds.size.z);
        handTransform.position += new Vector3(0f, 0f, grabbedObject.GetComponent<Collider>().bounds.size.z / 2);
        grabbedObject.transform.position = handTransform.position;
        grabbedObject.transform.SetParent(handTransform);
        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
        grabbedObject.GetComponent<Rigidbody>().useGravity = false;
        isGrabbed = true;
    }

    private void UnGrab()
    {
        grabbedObject.transform.SetParent(null);
        grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
        grabbedObject.GetComponent<Rigidbody>().useGravity = true;
        isGrabbed = false;
    }
}
