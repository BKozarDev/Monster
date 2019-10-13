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
    private float grabDistance = 3f;
    [SerializeField]
    private GameObject grabbedObject;
    private Rigidbody goRb;
    private InteractableObject goIo;
    [SerializeField]
    private bool isGrabbed = false;
    [SerializeField]
    private LayerMask grabObjectLayer;
    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (handTransform == null)
        {
            handTransform = GetComponentInChildren<Transform>();
        }
    }

    private void Update()
    {
        GrabNearObject();
    }

    private void GrabNearObject()
    {
        if (Input.GetKeyDown(grabKey))
        {
            //Debug.Log("Grab Key Down");
            if (!isGrabbed)
            {
                //Debug.Log("Grab");
                GrabFrontObject();
            }
            else
            {
                //Debug.Log("UnGrab");
                UnGrab();
            }

        }
    }

    private void GrabFrontObject()
    {
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, grabDistance, grabObjectLayer))
        //{
        //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        //    Debug.Log("Grab raycast hit");
        //    grabbedObject = hit.collider.gameObject;
        //    goRb = grabbedObject.GetComponent<Rigidbody>();
        //    goIo = grabbedObject.GetComponent<InteractableObject>();
        //    GrabToHand();
        //}

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 1f, transform.forward, out hit, grabDistance))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Grab raycast hit");
            grabbedObject = hit.collider.gameObject;
            goRb = grabbedObject.GetComponent<Rigidbody>();
            goIo = grabbedObject.GetComponent<InteractableObject>();
            GrabToHand();
        }
    }

    /*
    private void FindNearestObjectAndGrab()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, grabDistance, grabObjectLayer);
        float nearestDist = grabDistance;
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
    */
    private void GrabToHand()
    {
        // Анимация с вытянутыми руками
        goRb.isKinematic = true;
        goRb.useGravity = false;
        grabbedObject.transform.position = handTransform.position;
        grabbedObject.transform.position += transform.forward * (grabbedObject.GetComponent<Collider>().bounds.size.z / 2);
        grabbedObject.transform.SetParent(handTransform);

        isGrabbed = true;
        animator.SetBool("isGrab", true);
        animator.SetLayerWeight(1, 1f);
        if (goIo != null)
            goIo.OnGrabAction();
    }

    private void UnGrab()
    {
        goRb.isKinematic = false;
        goRb.useGravity = true;
        grabbedObject.transform.SetParent(null);
        if(goIo!=null)
            goIo.UnGrabAction();
        grabbedObject = null;
        isGrabbed = false;
        animator.SetBool("isGrab", false);
        animator.SetLayerWeight(1, 0f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == grabObjectLayer)
        {
            // Делаем обводку контура
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == grabObjectLayer)
        {
            // Снимаем обводку контура
        }
    }
}
