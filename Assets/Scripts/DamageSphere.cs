using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSphere : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] LayerMask layerMask;
    [SerializeField] int damageAmount;
    [SerializeField] float lifetime;
    [SerializeField] new ParticleSystem particleSystem;

    private void OnEnable()
    {
        if (particleSystem)
        {
            particleSystem.Clear();
            particleSystem.Simulate(0, true, true);
            particleSystem.Play();
        }

        StartCoroutine(DamageCor());
    }

    IEnumerator DamageCor()
    {
        float timeElapsed = 0;
        List<Health> healths = new List<Health>();

        while (timeElapsed <= lifetime)
        {
            timeElapsed += Time.deltaTime;
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask, QueryTriggerInteraction.Ignore);
            foreach (Collider c in colliders)
            {
                Health health = c.GetComponent<Health>();
                if (!healths.Contains(health))
                {
                    healths.Add(health);
                    health.DealDamage(damageAmount, transform.position);
                }
            }
            yield return null;
        }


        gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
