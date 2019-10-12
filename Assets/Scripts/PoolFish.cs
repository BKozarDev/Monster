using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolFish
{
    private static Dictionary<string, LinkedList<GameObject>> poolsDictionary;
    private static Transform deactivatedObjectsParent;

    public static void init(Transform pooledObjectsContainer)
    {
        deactivatedObjectsParent = pooledObjectsContainer;
        poolsDictionary = new Dictionary<string, LinkedList<GameObject>>();
    }

    public static GameObject getGameObjectFromPool(GameObject prefab)
    {
        //Debug.Log("PREFAB: " + prefab.name);

        if (!poolsDictionary.ContainsKey(prefab.name))
        {
            Debug.Log("!!!");
            poolsDictionary[prefab.name] = new LinkedList<GameObject>();
        }

        //Debug.Log("POOL: " + poolsDictionary.Values);

        Debug.Log(prefab.name + " POOL COUNT: " + poolsDictionary[prefab.name].Count);

        GameObject result;

        if(poolsDictionary[prefab.name].Count > 0)
        {
            result = poolsDictionary[prefab.name].First.Value;
            poolsDictionary[prefab.name].RemoveFirst();
            result.SetActive(true);
            return result;
        }

        result = GameObject.Instantiate(prefab);
        result.name = prefab.name;

        return result;
    }

    public static void putObjectToPool(GameObject target)
    {
        poolsDictionary[target.name].AddFirst(target);
        Debug.Log("PUT: " + target.name);
        target.transform.SetParent(deactivatedObjectsParent);
        //target.transform.parent = deactivatedObjectsParent;
        target.SetActive(false);
    }

    public static void debugShow()
    {
        foreach (var dick in poolsDictionary)
        {
            Debug.Log("DEBUG: " + dick.Key + " " + dick.Value);
        }
    }
}
