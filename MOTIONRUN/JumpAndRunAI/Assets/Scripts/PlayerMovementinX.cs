using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementinX : MonoBehaviour{

    public int gameSpeed;
    void Start(){
        gameSpeed = GameObject.FindGameObjectWithTag("GameManager").
            GetComponent<AutoScroll>().gameSpeed;
    }

    void Update(){
       transform.Translate(-gameSpeed * Time.deltaTime, 0, 0);
    }
}
