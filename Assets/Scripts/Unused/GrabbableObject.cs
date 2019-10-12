using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    [SerializeField]
    private GameObject grabText;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            grabText.SetActive(true);
            //grabText.transform.LookAt(Camera.main.transform.position);
            grabText.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            grabText.SetActive(false);
        }
    }
}
