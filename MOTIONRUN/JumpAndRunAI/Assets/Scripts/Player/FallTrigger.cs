using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FallTrigger : MonoBehaviour {

    public Transform playerPostion;

    // Update is called once per frame
    void Update(){
        this.transform.position = new Vector3(playerPostion.position.x, playerPostion.position.y, playerPostion.position.z);
    }
}
