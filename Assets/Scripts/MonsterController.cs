using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class MonsterController : MonoBehaviour
{
    // input
    private MonsterInputActions mnstrActionAsset;
    private InputAction movement;

    //movement
    private Rigidbody rb;
    [SerializeField]
    private float moveForce = 1f;
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private float maxSpeed = 5f;
    private Vector3 forceDirection = Vector3.zero;

    [SerializeField]
    private Camera playerCamRef;
    private Animator anim;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        mnstrActionAsset = new MonsterInputActions();
        anim = this.GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        mnstrActionAsset.Player.Jump.performed += DoJump;
        mnstrActionAsset.Player.PrimaryAttack.started += DoAttack;
        movement = mnstrActionAsset.Player.Move;
        mnstrActionAsset.Player.Enable();
    }

    private void OnDisable()
    {
        mnstrActionAsset.Player.Jump.started -= DoJump;
        mnstrActionAsset.Player.PrimaryAttack.started -= DoAttack;
        mnstrActionAsset.Player.Disable();
    }

    private void FixedUpdate()
    {
        forceDirection += movement.ReadValue<Vector2>().x * GetCameraRight(playerCamRef) * moveForce;
        forceDirection += movement.ReadValue<Vector2>().y * GetCameraForward(playerCamRef) * moveForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if (rb.velocity.y < 0f)
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;

        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;

        LookAt();
    }

    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if (movement.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else rb.angularVelocity = Vector3.zero;
    }

    private Vector3 GetCameraForward(Camera p_Cam)
    {
        Vector3 forward = p_Cam.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera p_Cam)
    {
        Vector3 right = p_Cam.transform.right;
        right.y = 0;
        return right.normalized;
    }

    void DoJump(InputAction.CallbackContext ctx)
    {
        if (IsGrounded())
        {
            forceDirection += Vector3.up * jumpForce;
        }
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1.3f))
            return true;
        else return false;
    }

    private void DoAttack(InputAction.CallbackContext ctx)
    {
        anim.SetTrigger("PrimaryAttack");
    }

    // debugging the raycast from IsGrounded()
    private void OnDrawGizmos()
    {
        Ray debugRay = new Ray(this.transform.position, Vector3.down); // + Vector3.up * 0.25f, Vector3.down);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position + Vector3.up * 0.25f, Vector3.down * 1.3f);
    }
}
// Hi I love you dum dum <3 *pees on this to mark it as mine*