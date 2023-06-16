using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public string LookupString;

    private void OnDisable()
    {
        ObjectPooling.disabledObjects.Add(this);
    }
}
