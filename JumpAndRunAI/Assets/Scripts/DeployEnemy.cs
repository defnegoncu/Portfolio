using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployEnemy : MonoBehaviour{

    public Transform playerTransform;
    public Transform playerForwardTransform;

    public void SpawnPixxie(GameObject spawnLeft, GameObject spawnRight, GameObject Pixxie){

        GameObject NewPixie = Instantiate(Pixxie) as GameObject;
        //if the player is on the left side
        if (playerTransform.position.z < 0){
            NewPixie.transform.position = spawnLeft.transform.position;
        }
        else{
            NewPixie.transform.position = spawnRight.transform.position;
        }
        NewPixie.SetActive(true);
        NewPixie.transform.parent = playerForwardTransform;
    }
}
