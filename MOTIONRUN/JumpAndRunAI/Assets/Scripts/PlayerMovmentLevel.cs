using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovmentLevel : MonoBehaviour{
    
    // Start is called before the first frame update
    float gravity = -9.81f;
    //need to access velocity for projectile system-Defne
    public float velocity=5;
    float velocityInstance;
    float velocityInstance1;

    public bool jumpPause=false;
    public bool jumpInitiated = false;
    public bool onFloor = false;
    public bool onFloorHit = false;
    //int gameSpeed;
    void Start(){
    float velocityInstance = velocity;
        float velocityInstance1 = velocity;
     //   gameSpeed = GameObject.Find("GameManager").GetComponent<AutoScroll>().gameSpeed;
    }

    void Jump(){

        if(onFloor){
            velocityInstance += gravity * Time.deltaTime;
            transform.Translate(new Vector3(0, velocityInstance, 0) * Time.deltaTime);

            if (transform.position.y <= 1.5f){
                velocityInstance = 5;
                transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
                jumpInitiated = false;
            }
        }

        if (onFloorHit){
                velocityInstance = 5;
                transform.position = new Vector3(transform.position.x, 2.5f, transform.position.z);
                jumpInitiated = false;
                onFloorHit = false;
                onFloor = true;
        }

        if (!onFloor)
        {
            velocityInstance += gravity * Time.deltaTime;
            transform.Translate(new Vector3(0, velocityInstance, 0) * Time.deltaTime);

            if (transform.position.y <= 1.5f){
                velocityInstance = 5;
                transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
                jumpInitiated = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(-gameSpeed * Time.deltaTime, 0, 0);
        if (Input.GetKeyDown(KeyCode.A) && transform.position.z >= 0){
            transform.Translate(0f, 0f, -1f, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.D) && transform.position.z <= 0){
            transform.Translate(0f, 0f, 1f, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpInitiated = true;
        }

        if (jumpInitiated)
        {
            Jump();
        }

        if (onFloor && transform.position.y >= 2.5f)
        {

            if (transform.position.y <= 1.5f)
            {
                velocityInstance = 5;
                transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
                jumpInitiated = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
        }
    }
}
