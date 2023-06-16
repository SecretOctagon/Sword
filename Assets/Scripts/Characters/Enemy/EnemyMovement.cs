using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MobMovement
{
    [Header("Enemy")]
    public bool followPlayer;
    public float StopFollowDistance;

    public Vector2 AttackDistance;
    public float attackAngle;

    public bool InAttackRange()
    {
        Vector3 playerRelativePos = PlayerMovement.active.transform.position - transform.position;
        
        //distance
        float distanceSqr = playerRelativePos.sqrMagnitude;
        bool distanceCheck =
            (distanceSqr > AttackDistance.x * AttackDistance.x) &&
            (distanceSqr < AttackDistance.y * AttackDistance.y);

        //angle
        float angle = Vector3.Angle(playerRelativePos, transform.forward);
        bool angleCheck = (angle <= attackAngle);

        return distanceCheck && angleCheck;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        //follow radius
        Gizmos.DrawWireSphere(transform.position, StopFollowDistance);

        //attack angle
        Gizmos.color = Color.red;

        Vector3 leftEdge = Quaternion.Euler(0, -attackAngle, 0) * transform.forward;
        Vector3 rightEdge = Quaternion.Euler(0, attackAngle, 0) * transform.forward;

        Vector3 leftProx = leftEdge * AttackDistance.x + transform.position;
        Vector3 leftDist = leftEdge * AttackDistance.y + transform.position;
        Gizmos.DrawLine(leftProx, leftDist);
        Vector3 rightProx = rightEdge * AttackDistance.x + transform.position;
        Vector3 rightDist = rightEdge * AttackDistance.y + transform.position;
        Gizmos.DrawLine(rightProx, rightDist);
    }
}
