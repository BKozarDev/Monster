using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFish : MonoBehaviour
{
    GrabObject go;
    GameObject player;

    Manager manager;

    int count = 0;

    [SerializeField]
    GameObject grab_fish;

    bool isFish = false;

    [HideInInspector]
    public List<GameObject> fishList;
    List<GameObject> fishList_Temp;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();
        fishList = new List<GameObject>();
        fishList_Temp = new List<GameObject>();
        go = FindObjectOfType<GrabObject>();
        count = 0;
        isFish = false;
    }

    LinkedList<GameObject> fishka = new LinkedList<GameObject>();
    int cc = 0;
    // Update is called once per frame
    void Update()
    {
        if (count > 1)
        {
            count = 0;
        }

        if (fishka.Count > 1)
        {
            isFish = false;
            StartCoroutine("removeFish");
            if (vse)
            {
                fishka.RemoveLast();
                StopCoroutine("removeFish");
                vse = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.F) && !go.isGrabbed)
            {
                fishka.AddFirst(PoolFish.getGameObjectFromPool(manager.getFish(count)));
                count++;
                isFish = true;
                go.TakeFish(fishka.Last.Value);
            }
        }
    }

    bool vse = false;

    IEnumerator removeFish()
    {
        yield return new WaitForSeconds(0);
        PoolFish.putObjectToPool(fishka.Last.Value);
        vse = true;
    }

    //IEnumerator removeFish(float delay)
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(delay);
    //        GameObject f = fishList[count - 2];
    //        Destroy(fishList[count - 2]);
    //        Debug.Log(count);
    //        fishList.Remove(f);
    //        count--;
    //        CopyList();
    //    }
    //}

    //void CopyList()
    //{
    //    fishList_Temp.Clear();
    //    foreach (GameObject g in fishList)
    //    {
    //        fishList_Temp.Add(g);
    //    }
    //    fishList.Clear();
    //    fishList = fishList_Temp;
    //    fishList_Temp.Clear();
    //}

    //void SpawnGFish()
    //{
    //    var fish = Instantiate(grab_fish, transform.position, Quaternion.identity);
    //    count++;
    //    fishList.Add(fish);
    //}
}
