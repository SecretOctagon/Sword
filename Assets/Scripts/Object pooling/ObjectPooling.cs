using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling manager;
    public static List<PoolObject> disabledObjects;

    private void Awake()
    {
        manager = this;
        disabledObjects = new List<PoolObject>();
    }

    public GameObject Spawn(string lookupString, Vector3 position, Quaternion rotation)
    {
        PoolObject po = disabledObjects.Find(p => p.LookupString == lookupString);

        if (po == null)
        {
            Debug.Log(lookupString + " is not pooled");
            return null;
        }

        Debug.Log("spawning " + lookupString + " from pool");
        GameObject go = po.gameObject;
        go.SetActive(true);
        go.transform.position = position;
        go.transform.rotation = rotation;
        disabledObjects.Remove(po);
        return go;
    }
}
