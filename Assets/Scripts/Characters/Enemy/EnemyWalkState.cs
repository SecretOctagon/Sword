using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkState : WalkState
{
    EnemyMovement enemyMovement;
    AttackSpawner attackSpawner;
    int a_Attack = Animator.StringToHash("Attack");

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!enemyMovement)
            enemyMovement = animator.GetComponentInParent<EnemyMovement>();
        mobMovement = enemyMovement;

        if (!attackSpawner)
            attackSpawner = animator.GetComponentInParent<AttackSpawner>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        direction = PlayerMovement.active.transform.position - animator.transform.position;
        direction.y = 0;
        float distanceSqr = direction.sqrMagnitude;
        direction.Normalize();

        lateral = (distanceSqr > enemyMovement.StopFollowDistance);// * enemyMovement.StopFollowDistance);
        //Debug.Log("Distance is " + distanceSqr + " lateral movement is " + false);

        bool canAttack = enemyMovement.InAttackRange() && attackSpawner.canSpawn;
        animator.SetBool(a_Attack, canAttack);

        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }
}
