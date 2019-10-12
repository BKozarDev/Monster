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
        player = GameObject.FindGameObjectWithTag("Player");
        go = player.GetComponent<GrabObject>();
        count = -1;
        fishka = new GameObject[2];
        isFish = false;
    }

    GameObject[] fishka;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            count++;
            fishka[count] = PoolFish.getGameObjectFromPool(manager.getFish(count));
            isFish = true;
        }

        if (isFish && count > 0)
        {
            isFish = false;
            StartCoroutine("removeFish", 3f);
            count--;
        }
    }

    IEnumerator removeFish(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            PoolFish.putObjectToPool(fishka[count]);
            isFish = false;
        }
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
