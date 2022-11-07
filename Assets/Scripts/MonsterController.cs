using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class MonsterController : MonoBehaviour
{
    // delegates
    public delegate void TestPlayerDelegate();
    public static TestPlayerDelegate boop;

    // input
    private InputActionAsset mnstrActionAsset;
    private InputActionMap playerMonster;
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

    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    GameObject rotateVisuals;

    private void Awake()
    {
        playerCamRef = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        //mnstrActionAsset = new MonsterInputActions();
        rb = this.GetComponent<Rigidbody>();
        anim = this.GetComponentInChildren<Animator>();
        mnstrActionAsset = this.GetComponent<PlayerInput>().actions;
        playerMonster = mnstrActionAsset.FindActionMap("Player");
    }

    private void OnEnable()
    {
        playerMonster.FindAction("Jump").started += DoJump;
        playerMonster.FindAction("PrimaryAttack").started += DoPrimaryAttack;
        movement = playerMonster.FindAction("Move");
        playerMonster.Enable();
    }

    private void OnDisable()
    {
        playerMonster.FindAction("Jump").started -= DoJump;
        playerMonster.FindAction("PrimaryAttack").started -= DoPrimaryAttack;
        playerMonster.Disable();
    }

    private void FixedUpdate()
    {

        forceDirection += new Vector3(movement.ReadValue<Vector2>().x, 0f, movement.ReadValue<Vector2>().y) * moveForce;

        if (Keyboard.current.leftShiftKey.isPressed) forceDirection *= 1.75f;

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
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = playerCamRef.farClipPlane;

        Ray mouseRay = playerCamRef.ScreenPointToRay(mousePos);
        Debug.DrawRay(mouseRay.origin, mouseRay.direction);
        
        Vector3 direction = Vector3.zero;

        if(Physics.Raycast(mouseRay, out RaycastHit hit, playerCamRef.farClipPlane, groundLayer))
        {
            direction = hit.point - transform.position;
        }

        Debug.DrawRay(transform.position, direction - transform.position);

        direction.y = 0f;

        transform.forward = direction;
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

    private bool IsGrounded()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1.3f))
            return true;
        else return false;
    }
    #region ACTIONS
    void DoJump(InputAction.CallbackContext ctx)
    {
        if (IsGrounded())
        {
            forceDirection += Vector3.up * jumpForce;
        }
    }

    private void DoPrimaryAttack(InputAction.CallbackContext ctx)
    {
        anim.SetTrigger("PrimaryAttack");
    }
    #endregion

    #region DEBUG
    // debugging the raycast from IsGrounded()
    private void OnDrawGizmos()
    {
        Ray debugRay = new Ray(this.transform.position, Vector3.down); // + Vector3.up * 0.25f, Vector3.down);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position + Vector3.up * 0.25f, Vector3.down * 1.3f);
    }

    private void OnCollisionEnter(Collision col)
    {
        string objName = col.gameObject.name;
        float impulseForce = col.GetContact(0).impulse.magnitude / Time.fixedDeltaTime;
        Debug.Log($"HIT {objName} WITH {impulseForce:N0} FORCE");
    }
    #endregion
}
// Hi I love you dum dum <3 *pees on this to mark it as mine*