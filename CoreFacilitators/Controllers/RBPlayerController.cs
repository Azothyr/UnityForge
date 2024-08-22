/* inputActions.Player.Move is causing a compiler error and needs to be resolved before this script can be used.
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : RbControllerBase
{
    [SerializeField] private Vector3 velocity;

    public Vector3Data playerV3;
    private InputActions inputActions;
    private Quaternion skew = Quaternion.Euler(new Vector3(0, -45, 0));
    private Vector2 inputVector;
    private Vector3 inputDirection;
    private float negativeTopSpeed;

    protected override void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        SetCurrentV3();
        
        inputActions = new InputActions();
        
        inputActions.Player.Enable();

        inputActions.Player.Move.performed += GetMoveInput;
        inputActions.Player.Move.canceled += StopMovement;
        
        speed = controllerData.speed.value;
        topSpeed = controllerData.topSpeed.value;
        negativeTopSpeed = topSpeed * -1;
        knockBackResistance = controllerData.knockBackResistance;
    }
    
    private void GetMoveInput(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }

    private void StopMovement(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
        StopCoroutine(Move());
    }

    protected override IEnumerator Move()
    {
        while (controllerData.canRun.value)
        {
            inputDirection = new Vector3(inputVector.x, 0, inputVector.y).normalized;
            moveDirection = (skew * inputDirection);
            rigidBody.AddForce(moveDirection * speed, ForceMode.Acceleration);

            if (rigidBody.velocity.x > topSpeed)
            {
                rigidBody.velocity = new Vector3(topSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            }

            if (rigidBody.velocity.x < negativeTopSpeed)
            {
                rigidBody.velocity = new Vector3(negativeTopSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            }

            if (rigidBody.position.y > 0)
            {
                rigidBody.position = new Vector3(rigidBody.position.x, 0, rigidBody.position.z);
            }

            if (rigidBody.velocity.z > topSpeed)
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, topSpeed);
            }

            if (rigidBody.velocity.z < negativeTopSpeed)
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, negativeTopSpeed);
            }

            velocity = rigidBody.velocity;

            SetCurrentV3();
            yield return wffuObj;
        }
    }

    protected override void SetCurrentV3()
    {
        currentLocation = rigidBody.position;
        playerV3.Set(currentLocation);
    }
}
*/