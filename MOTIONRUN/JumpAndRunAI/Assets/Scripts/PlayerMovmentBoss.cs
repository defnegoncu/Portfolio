using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovmentBoss : MonoBehaviour
{
    Rigidbody rb;
    public PlayerMovement pm;
    // Start is called before the first frame update
    void Start()
    {
         rb = GetComponent<Rigidbody>();
        pm= FindObjectOfType(typeof(PlayerMovement)) as PlayerMovement;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            transform.Translate(0, 0, -3*Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(0, 0, 3 * Time.deltaTime);
        if (Input.GetKey(KeyCode.W))
            transform.Translate(-3 * Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(3 * Time.deltaTime, 0, 0);
        if(Input.GetKey(KeyCode.Space))
        {
            pm.Jump6Dof();
        }
    }
}
