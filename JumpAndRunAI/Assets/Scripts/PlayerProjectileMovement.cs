using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileMovement : MonoBehaviour
{
    private Transform shieldtransform;
    private Vector3 shieldorientation;

    private Vector3 currVelocity;

    private float fmaxForce = 20.0f;
    private float fmaxVelocity = 10.0f;
    private float fdestroyDistance = 60.0f;
    // Start is called before the first frame update
    void Start()
    {
        currVelocity.Set(0.0f, 0.0f, 0.0f);
        shieldtransform = GameObject.FindGameObjectWithTag("Shield").transform;
        shieldorientation.Set(-1.0f, 0.0f, 0.0f);
        shieldorientation = shieldtransform.rotation * shieldorientation;
        //shieldorientation is the direction of our force, so we can normalize and * to get our force
        shieldorientation = shieldorientation.normalized * fmaxForce;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.transform.position.x < shieldtransform.position.x - fdestroyDistance)
        {
            Destroy(this);
        }
        currVelocity += shieldorientation * Time.deltaTime;
        currVelocity = Vector3.ClampMagnitude(currVelocity, fmaxVelocity);

        this.transform.position += currVelocity * Time.deltaTime;
        this.transform.forward += currVelocity * Time.deltaTime;
    }
}
