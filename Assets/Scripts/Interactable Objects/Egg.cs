using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : InteractableObject
{
    [SerializeField]
    private GameObject brokenEgg;
    [SerializeField]
    private float activationTime = 0.5f;
    [SerializeField]
    private bool isDropped = false;

    public override void OnGrabAction()
    {
        // Другие пингвины начинают гнаться за тобой
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>().isHunted = true;
    }

    public override void UnGrabAction()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>().isHunted = false;
        Destroy(gameObject);
        Instantiate(brokenEgg, transform.position, transform.rotation);
        // isDropped = true;
        // StartCoroutine(BrokeEggAfterDelay(activationTime));
    }

    //IEnumerator BrokeEggAfterDelay(float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    Destroy(gameObject);
    //    Instantiate(brokenEgg, transform.position, transform.rotation);
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (isDropped)
    //    {
    //        Destroy(gameObject);
    //        Instantiate(brokenEgg, transform.position, transform.rotation);
    //    }
    //}
}
