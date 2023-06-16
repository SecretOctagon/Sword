using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MobMovement
{
    public static PlayerMovement active;

    [HideInInspector] public Vector2 rawInput;
    [HideInInspector] public bool jump = false;

    [SerializeField] AttackSpawner attackSpawner;

    [SerializeField] float PeckCooldown;
    [HideInInspector] public bool peckReady = true;

    [Header("Space")]
    [SerializeField] Transform relativeSpace;
    public float highiestPointReached;

    int a_Attack = Animator.StringToHash("Attack");

    private void Awake()
    {
        active = this;
    }

    public Vector3 GetDirection()
    {
        Vector3 flatDirection = new Vector3(rawInput.x, 0f, rawInput.y);
        Vector3 worldDirection = relativeSpace.TransformDirection(flatDirection);
        return worldDirection;
    }

    public void InputWalk(InputAction.CallbackContext context)
    {
        rawInput = context.ReadValue<Vector2>();
    }
    public void InputJump(InputAction.CallbackContext context)
    {
        jump = true;
        StartCoroutine(JumpBoolCor());
    }
    IEnumerator JumpBoolCor()
    {
        yield return null;
        jump = false;
    }
    public void InputAttack(InputAction.CallbackContext context)
    {
        if (attackSpawner.canSpawn)
        {
            Debug.Log(name + " attacking");
            animator.SetTrigger(a_Attack);
            return;
        }

        Debug.Log(name + " cannot spawn, attack won't be triggered");
    }
}
