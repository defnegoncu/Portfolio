using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixxieTrigger : MonoBehaviour{
    DeployEnemy deployEnemy;
    private void Start()
    {
        deployEnemy = FindObjectOfType<DeployEnemy>();
    }
    private void OnTriggerEnter(Collider other){

        if (other.tag == "Player") {
            Debug.Log("Pixie Triggert");
            //Index0=object, 1= leftSP, 2=rightSP
            deployEnemy.SpawnPixxie(this.transform.GetChild(1).gameObject,
                this.transform.GetChild(2).gameObject, this.transform.GetChild(0).gameObject);
        }
    }
}
