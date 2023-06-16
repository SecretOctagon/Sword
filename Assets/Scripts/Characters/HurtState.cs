using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : StateMachineBehaviour
{
    [SerializeField] ParticleSystem hurtParticles;

    int a_X = Animator.StringToHash("DirectionX");
    int a_Z = Animator.StringToHash("DirectionZ");

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!hurtParticles)
            hurtParticles = animator.GetComponentInChildren<ParticleSystem>();

        Vector3 direction = Vector3.zero;
        direction.x = animator.GetFloat(a_X);
        direction.z = animator.GetFloat(a_Z);
        Quaternion rotation = Quaternion.LookRotation(direction);
        //animator direction is in world coordinates
        hurtParticles.transform.rotation = rotation;
    }
}
