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
                FindNearestObjectAndGrab();
            }
            else
            {
                Debug.Log("UnGrab");
                UnGrab();
            }

        }
    }


    private void FindNearestObjectAndGrab()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, grabRadius, grabObjectLayer);
        float nearestDist = grabRadius;
        if (hitColliders.Length > 0)
        {
            foreach (Collider collider in hitColliders)
            {
                float dist = Vector3.Distance(transform.position, collider.transform.position);
                if (nearestDist > dist)
                {
                    nearestDist = dist;
                    grabbedObject = collider.gameObject;

                }
            }
            GrabToHand();
        }

    }

    private void GrabToHand()
    {
        // Анимация с вытянутыми руками
        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
        grabbedObject.GetComponent<Rigidbody>().useGravity = false;
        grabbedObject.transform.position = handTransform.position;
        grabbedObject.transform.position += transform.forward * (grabbedObject.GetComponent<Collider>().bounds.size.z / 2);
        grabbedObject.transform.SetParent(handTransform);

        isGrabbed = true;
    }

    private void UnGrab()
    {
        grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
        grabbedObject.GetComponent<Rigidbody>().useGravity = true;
        grabbedObject.transform.SetParent(null);
        grabbedObject = null;
        isGrabbed = false;
    }
}
