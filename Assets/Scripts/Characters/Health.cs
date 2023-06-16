using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    [SerializeField] Animator anim;
    int a_damage = Animator.StringToHash("Damage");
    int a_directionX = Animator.StringToHash("DirectionX");
    int a_directionZ = Animator.StringToHash("DirectionZ");
    int a_death = Animator.StringToHash("Dead");

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public float GetHealth01()
    {
        return (float)currentHealth / maxHealth;
    }
    public virtual void DealDamage(int damage, Vector3 origin)
    {
        SimpleDamage(damage);

        if (anim)
        {
            anim.SetTrigger(a_damage);
            Vector3 worldDirection = transform.position - origin;
            Vector3 localDirection = transform.InverseTransformDirection(worldDirection);
            anim.SetFloat(a_directionX, localDirection.x);
            anim.SetFloat(a_directionZ, localDirection.z);
        }
    }
    public virtual void SimpleDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth == 0)
            Death();

        if (anim)
            anim.SetTrigger(a_damage);
    }
    protected virtual void Death()
    {
        Debug.Log(name + " died");
        anim.SetBool(a_death, true);
    }
}
