using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;


    public PlayerMotor motor;
    public PlayerLook look;

   public void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        onFoot.Jump.performed += ctx => motor.Jump();

    }
    public void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
    public void Update()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }
    public void OnEnable()
    {
        onFoot.Enable();
    }
    public void OnDisable()
    {
        onFoot.Disable();
    }
}
