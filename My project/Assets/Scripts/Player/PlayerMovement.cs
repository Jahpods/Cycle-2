using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float Speed;
    [SerializeField]
    private float SprintMultiplier;
    [SerializeField]
    private float MaxSpeed;
    [SerializeField]
    private float MinSpeed;
    [Range(0f,1f), SerializeField]
    private float Friction;
    private Vector3 moveVector;
    private bool isSprinting;

    [Header("Jumping and Gravity")]
    [SerializeField]
    private float Gravity;
    [SerializeField]
    private float JumpForce;
    [SerializeField]
    private float JumpBuffer;
    [SerializeField]
    private float CoyoteTime;
    private bool tryingJump;
    private bool IsJumping;
    private float cJumpBuffer;
    private float cCoyoteTime;
    private float jCooldown = 0.05f;
    private float cJCooldown;

    //Setup Looking Variables
    [Header("Mouse")]
    [SerializeField]
    private float sensitivity;
    private Vector2 mouseVector;
    private float cameraVerticalRotation;

    //Setup reference variables
    [SerializeField]
    private Transform ch;
    private Rigidbody rb;

    //Visual
    [Header("Visuals")]
    [SerializeField]
    private ParticleSystem ps;

    //Animations
    private Animator anim;
    private string currentState;
    private bool landing;

    // Start is called before the first frame update
    void Start()
    {
        //Set references
        rb = GetComponent<Rigidbody>();

        //Lock cursor
        Cursor.lockState = CursorLockMode.Locked;

        //Set Timers
        cJumpBuffer = JumpBuffer;
        cCoyoteTime = CoyoteTime;

        //Animator
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get Input from wasd and mouse movement
        moveVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if(moveVector.magnitude > 1f){
            moveVector = moveVector.normalized;
        }
        mouseVector = new Vector2(Input.GetAxis("Mouse X"), 
                                  Input.GetAxis("Mouse Y"));
        if(Input.GetKeyDown(KeyCode.Space)){
            tryingJump = true;
            cJumpBuffer = JumpBuffer;
        }

        if(Input.GetKey(KeyCode.LeftShift)){
            isSprinting = true;
        }else{
            isSprinting = false;
        }

        if(IsGrounded() && !IsJumping){
            cCoyoteTime = CoyoteTime;
        }else if(CoyoteTime >= 0){
            cCoyoteTime -= Time.deltaTime;
        }

        if(tryingJump){
            if(IsGrounded()){
                IsJumping = true;
                cCoyoteTime = 0;
                tryingJump = false;
                rb.velocity = new Vector3(rb.velocity.x, JumpForce, rb.velocity.z);
            }else if(cCoyoteTime > 0){
                IsJumping = true;
                cCoyoteTime = 0;
                tryingJump = false;
                rb.velocity = new Vector3(rb.velocity.x, JumpForce, rb.velocity.z);
            }else{
                cJumpBuffer -= Time.deltaTime;
                if(cJumpBuffer <= 0){
                    tryingJump = false;
                }
            }
        }

        //Animations
        if(IsGrounded()){
            if(currentState == "PlayerAirborn" || landing){
                landing = true;
                ChangeAnimationState("PlayerLand");
                Invoke("Land", 0.1f);
            }else{
                if(moveVector != Vector3.zero){
                    if(isSprinting){
                        ChangeAnimationState("PlayerRun");
                    }else{
                        ChangeAnimationState("PlayerWalk");
                    }  
                }else{
                    ChangeAnimationState("PlayerIdle");
                }
            }
        }else{
            if(!IsGrounded()){
                landing = true;
                if(rb.velocity.y > 0){
                    ChangeAnimationState("PlayerAirborn");
                }else{
                    ChangeAnimationState("PlayerFall");

                }
                
            }
        }

        if(moveVector != Vector3.zero && isSprinting){
            ps.gameObject.SetActive(true);
        }else{
            ps.gameObject.SetActive(false);
        }
    }

    void Land(){
        landing = false;
    }

    void FixedUpdate(){
        //Vertical Rotation
        cameraVerticalRotation -= mouseVector.y * sensitivity;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation,-80,80);

        ch.localEulerAngles = Vector3.right * cameraVerticalRotation;

        //Horizontal Rotation
        transform.Rotate(Vector3.up * mouseVector.x * sensitivity);

        //Apply Gravity
        if(IsGrounded() && rb.velocity.y <= 0){
            //rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }else{
            rb.velocity += Vector3.down * Gravity;       
        }
        float xzMagnitude = new Vector2(rb.velocity.x, rb.velocity.z).magnitude;
        //Apply Friction
        if(xzMagnitude > MinSpeed){
            rb.velocity -= new Vector3(rb.velocity.x, 0, rb.velocity.z) * Friction;
        }else{
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        //Apply Movement
        if(xzMagnitude < (MaxSpeed + Sprinting() * SprintMultiplier)){
            rb.velocity += (transform.forward * moveVector.z + transform.right * moveVector.x) * Speed;
        }     
    }

    bool IsGrounded(){
        if(IsJumping){
            if(cJCooldown <= 0){
                IsJumping = false;
                cJCooldown = jCooldown;
            }else{
                cJCooldown -= Time.deltaTime;
            }
            return false;
        }        
        return Physics.BoxCast(transform.position - transform.up * 0.5f, 
                               transform.localScale/2 - Vector3.up * transform.localScale.y/4, -transform.up, transform.rotation, 0.3f);
    }

    int Sprinting(){
        if(isSprinting){
            return 1;
        }else{
            return 0;
        }
    }

    public float GetSens(){
        return sensitivity;
    }

    public void SetSens(float sens){
        sensitivity = sens;
    }

    private void ChangeAnimationState(string newState){
        if(newState == currentState) return;

        anim.Play(newState);

        currentState = newState;
    }
}
