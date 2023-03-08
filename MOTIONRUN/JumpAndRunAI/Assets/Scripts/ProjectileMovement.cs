using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public Transform playerPos;
    //private float playervelocity;

    private Vector3 currVelocity;
    private Vector3 steeringForce;

    private float fmaxForce = 10.0f;
    private float fmaxVelocity = 5.0f;
    private float fdestroyDistance = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        steeringForce =  (playerPos.position- this.transform.position).normalized
            *fmaxForce;
        // playervelocity = playerPos.GetComponent<PlayerMovmentLevel>().velocity;
        //currVelocity.Set(-playervelocity, 0.0f, 0.0f);
        currVelocity.Set(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.transform.position.x > playerPos.position.x + fdestroyDistance)
        {
            Destroy(this);
        }
        currVelocity += steeringForce * Time.deltaTime;
        currVelocity = Vector3.ClampMagnitude(currVelocity, fmaxVelocity);

        this.transform.position += currVelocity * Time.deltaTime;
        this.transform.forward += currVelocity * Time.deltaTime;
    }
}
