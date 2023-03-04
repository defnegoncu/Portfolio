using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    [Header("Movement")]
    public Transform orientation;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;
    bool jumping;
    bool inBoss;
    bool iscrouching;

    float horizontalInput;
    float verticalInput;
    int gameSpeed;

    CapsuleCollider CapsuleCollider;
    Transform playerCamera;

    Vector3 moveDirection;
    Rigidbody rb;

    private void Start(){

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        CapsuleCollider = GameObject.Find("PlayerObject").GetComponent<CapsuleCollider>();
        playerCamera = GameObject.Find("PlayerCamera").transform;
        FindObjectOfType<AudioManager>().Play("walk");
        inBoss = GameObject.Find("FirstPersonPlayer").GetComponent<DTrack.DTrackReceiver6Dof>().boss;
        iscrouching = false;
    }

    private void Update(){
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + whatIsGround);
        MyInput();

        if (jumping && grounded){
            FindObjectOfType<AudioManager>().Play("jumpland");
            if (!inBoss)
            {
                FindObjectOfType<AudioManager>().PlayDelayed("walk", "jumpland");
            }
            jumping = false;
        }
    }

    private void MyInput(){

        if (Input.GetKeyDown(KeyCode.A) && transform.position.z >= -0.1){
            transform.Translate(0f, 0f, -1.0f, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.D) && transform.position.z <= 0.1){
            transform.Translate(0f, 0f, 1.0f, Space.World);
        }
        
        if (Input.GetKeyDown(jumpKey) && readyToJump && grounded){
            Debug.Log("Wir springen");
            Jump();
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            CapsuleCollider.height = 0.975f;
            CapsuleCollider.center = new Vector3(CapsuleCollider.center.x, -0.5f, CapsuleCollider.center.z);
            playerCamera.position = new Vector3(playerCamera.position.x, 7502.0f, playerCamera.position.z);
            //SOUNDTEST
            if (!iscrouching)
            {
                FindObjectOfType<AudioManager>().Stop("walk");
                FindObjectOfType<AudioManager>().Play("slidebegin");
                FindObjectOfType<AudioManager>().PlayDelayed("slideloop", "slidebegin");
            }
            iscrouching = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            CapsuleCollider.height = 1.95f;
            CapsuleCollider.center = new Vector3(CapsuleCollider.center.x, 0.0f, CapsuleCollider.center.z);
            playerCamera.position = new Vector3(playerCamera.position.x, 7503.0f, playerCamera.position.z);
            //SOUNDTEST 
            if (iscrouching)
            {
                FindObjectOfType<AudioManager>().Stop("slideloop");
                FindObjectOfType<AudioManager>().Play("slideend");
                iscrouching = false;
            }
        }
    }

    public void Jump6Dof(){
        if (readyToJump && grounded){

            Debug.Log("Wir springen 6dof");
            Jump();

        }
    }

    private void Jump(){

        readyToJump = false;

        FindObjectOfType<AudioManager>().Stop("walk");
        FindObjectOfType<AudioManager>().Play("jump");

        rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        Invoke(nameof(ResetJump), jumpCooldown);
        Invoke(nameof(ResumeSound), 0.5f);
    }

    private void Crouch(){
        FindObjectOfType<AudioManager>().Stop("walk");

    }

    private void ResetJump(){
        readyToJump = true;
    }

    private void ResumeSound(){
        jumping = true;
    }

}
