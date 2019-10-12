using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> fishList = new List<GameObject>();

    [SerializeField]
    public List<GameObject> noPref = new List<GameObject>();

    int index;

    private void Start()
    {
        index = 0;
        PoolFish.init(transform);
        for (int i = 0; i < fishList.Count; i++)
        {
            noPref.Add(PoolFish.getGameObjectFromPool(getFish(i)));
            PoolFish.putObjectToPool(noPref[i]);
        }
    }

    public GameObject getFish(int index)
    {
        return fishList[index];
    }

    
}
