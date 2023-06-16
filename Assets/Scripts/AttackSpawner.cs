using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpawner : MonoBehaviour
{
    public bool canSpawn = true;
    [SerializeField] GameObject prefab;
    [SerializeField] float cooldown;
    [SerializeField] Transform[] spawnPoints;

    [SerializeField] float gizmoRadius;

    public void Spawn()
    {
        if (canSpawn)
        {
            PoolObject prefabPoolObject = prefab.GetComponent<PoolObject>();
            string poolLookup = prefabPoolObject?.LookupString;

            foreach (Transform point in spawnPoints)
            {
                GameObject spawned = ObjectPooling.manager.Spawn(poolLookup, point.position, point.rotation);
                if (!spawned)
                {
                    Instantiate(prefab, point.position, point.rotation);
                }
            }

            StartCoroutine(CooldownCor());
        }
    }
    IEnumerator CooldownCor()
    {
        canSpawn = false;
        yield return new WaitForSeconds(cooldown);
        canSpawn = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        foreach (Transform T in spawnPoints)
            Gizmos.DrawWireSphere(T.position, gizmoRadius);
    }
}
