using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixxieMovement : MonoBehaviour{

    public Transform targetPos;
    public Transform playerPos;
    //not sure if Inspector assign works for prefabs, so assigned them in start for good measure
    [SerializeField] Transform leftTarget;
    [SerializeField] Transform rightTarget;

    private Vector3 currVelocity;
    private Vector3 steeringForce;

    private float fmaxForce = 10.0f;
    private float fmaxVelocity = 5.0f;
 
    void Start(){
        currVelocity.Set(0.0f, 0.0f, 0.0f);
        leftTarget = GameObject.Find("LeftPixxieTarget").transform;
        rightTarget = GameObject.Find("RightPixxieTarget").transform;

        playerPos = GameObject.Find("FirstPersonPlayer").transform;

    }

    // Update is called once per frame
    void FixedUpdate(){

        if (playerPos.position.z < 0.0f){
            targetPos = leftTarget;
        }
        else{
            targetPos = rightTarget;
        }

        //target vector
        steeringForce = (targetPos.position - this.transform.position).normalized;
        steeringForce *= fmaxForce;
        //Limit Steering force by max force
        steeringForce = Vector3.ClampMagnitude(steeringForce, fmaxForce);

        //Calculate current Velocity
        currVelocity += steeringForce * Time.deltaTime;
        currVelocity = Vector3.ClampMagnitude(currVelocity, fmaxVelocity);

        //Move pixxie
        this.transform.position += currVelocity * Time.deltaTime;
        this.transform.forward += currVelocity * Time.deltaTime;
    }
}
