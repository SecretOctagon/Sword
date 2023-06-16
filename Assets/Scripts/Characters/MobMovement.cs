using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
    [SerializeField] CharacterController cc;
    [SerializeField] Color gizmoColor;

    [Header("movement")]
    [SerializeField] float baseSpeed;
    public float speedMultiplier = 1;
    [SerializeField] bool dotAffectsSpeed;

    [Header("rotation")]
    [SerializeField] float baseRotationSpeed;
    public float rotationMultiplier = 1;
    [SerializeField] bool allowBackwardMovement;
    [SerializeField] float RotationMinDot;

    [Header("jump")]
    [SerializeField] float jumpHeight;
    protected float jumpVelocity;
    [SerializeField] protected float baseGravity;
    [SerializeField] float fallMultiplier;
    protected float VerticalVelocity = 0;
    [SerializeField] float CoyoteTime;
    [SerializeField] bool ignoreGround;
    protected float longGrounded;
    public bool isGrounded { get => longGrounded > 0; }

    [Header("animator")]
    [SerializeField] protected Animator animator;
    [SerializeField] float animTurnMultiplier = 1;

    public Vector3 saveMoveDirection;

    protected virtual void Start()
    {
        if (!cc)
        {
            cc = GetComponent<CharacterController>();
            Debug.Log(name + "'s character controller is " + cc);
        }
        if (!animator)
           animator = GetComponentInChildren<Animator>();

        longGrounded = 0;

        jumpVelocity = Mathf.Sqrt(-2.0f * baseGravity * jumpHeight) / 7;
        Debug.Log(name + " jump velocity is " + jumpVelocity);
    }

    #region always updated
    protected virtual void Update()
    {
        CheckGrounded();
    }
    void CheckGrounded()
    {
        switch (cc.isGrounded)
        {
            case true:
                longGrounded = CoyoteTime;
                //VerticalVelocity = baseGravity * Time.deltaTime;
                break;
            case false:
                longGrounded -= Time.deltaTime;
                break;
        }
    }
    #endregion

    #region movement
    public void Move(Vector3 moveDirection, bool sideways, bool vertical, out float dot)
    {
        Vector3 motion = Vector3.zero;
        float initialHeight = transform.position.y;
        dot = GetDot(moveDirection);

        transform.rotation = GetRotation(moveDirection, dot);

        if (sideways)
        {
            moveDirection.y = 0;

            motion = GetLateralMovement(moveDirection, dot);
        }
        saveMoveDirection = motion;

        if (vertical)
        {
            VerticalVelocity += GetVerticalMovement();
            motion.y = VerticalVelocity;
        }

        cc.Move(motion);

        if (vertical)
            VerticalVelocity = transform.position.y - initialHeight;

    }
    protected float GetDot(Vector3 direction)
    {
        return Vector3.Dot(transform.forward, direction.normalized);
    }
    protected Vector3 GetLateralMovement(Vector3 direction, float dot)
    {
        switch (allowBackwardMovement)
        {
            case true:
                dot = Mathf.Abs(dot);
                break;
            case false:
                dot = Mathf.Clamp01(dot);
                break;
        }
        Vector3 translation = direction * baseSpeed * speedMultiplier * Time.deltaTime;
        if (dotAffectsSpeed) translation *= dot;
        return translation;
    }
    protected float GetVerticalMovement()
    {
        switch (VerticalVelocity > 0)
        {
            case true: //going up
                return baseGravity * Time.deltaTime;
            case false: //falling
                return baseGravity * Time.deltaTime * fallMultiplier;
        }
    }
    protected Quaternion GetRotation(Vector3 direction, float dot)
    {
        switch (direction.sqrMagnitude > Mathf.Epsilon && dot > RotationMinDot)
        {
            case true:
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                float maxAngle = baseRotationSpeed * rotationMultiplier * Time.deltaTime;
                Quaternion endRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxAngle);
                return endRotation;
            case false:
                return transform.rotation;
        }
    }
    #endregion
    #region action transitions
    public virtual void StartJump()
    {
        VerticalVelocity = jumpVelocity;
    }
    #endregion

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawLine(transform.position, transform.position + saveMoveDirection);
    }
}