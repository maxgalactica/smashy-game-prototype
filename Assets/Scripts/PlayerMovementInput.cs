using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementInput : MonoBehaviour
{
    //Stompygamemovement plyInputActions;
    InputAction movement;

    private void Awake()
    {
        //plyInputActions = new Stompygamemovement();
    }

    private void OnEnable()
    {
        // this is a constant stream of input data
        // no events are needed
        //movement = plyInputActions.Player.Move;
        //movement.Enable();

        // button presses don't need to be read every frame
        // instead we will add our methods to the events listening for them
        //plyInputActions.Player.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        //plyInputActions.Player.Jump.performed -= Jump;
    }

    private void FixedUpdate()
    {
        Debug.Log("Movement Values: " + movement.ReadValue<Vector2>());
    }

    void Jump(InputAction.CallbackContext ctx)
    {
        Debug.Log("JOOMP");
    }
}
