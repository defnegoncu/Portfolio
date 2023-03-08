using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour{

    private GameManager gameManager;
    public PlayerMovementinX PlayerMovementinX;
    BossBehaviour boss;
    private void Start(){
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter(UnityEngine.Collision collision){

        if (collision.gameObject.tag == "EndProjectileBangBangIntoDraagon")
        {
            Debug.Log("workes");
            boss= GameObject.Find("SkyDragonDan").GetComponent<BossBehaviour>();
            Destroy(collision.gameObject);
            StartCoroutine(boss.BangBang());
        }
        if (collision.gameObject.tag == "Projectile"){
            Debug.Log("Hit Projectile -> Lost");
            //Restart the Game upon collision with Projectile
            gameManager.EndGame();
        }

        if (collision.gameObject.tag=="Obstacle"){
            Debug.Log("Hit Obstacle -> Lost");
            Debug.Log(collision.gameObject. name);
            //Restart the Game upon collision with Obstacle
            gameManager.EndGame();
        }

        if (collision.gameObject.tag == "GameEnd")
        {
            Debug.Log("Completed Game -> GZ");
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        //if (collision.gameObject.tag == "Floor1"){
        //    Debug.Log("Standing on Floor");
        //    FindObjectOfType<PlayerMovmentLevel>().onFloorHit = true;
        //}        
    }
    

    private void OnCollisionExit(UnityEngine.Collision collision){

        //if (collision.gameObject.tag == "Floor1"){
        //    Debug.Log("left Floor");
        //    FindObjectOfType<PlayerMovmentLevel>().onFloor = false;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile"){
            Debug.Log("Hit: Projectile");
            gameManager.EndGame();
        }

        if (other.tag == "Coin"){
            Debug.Log("Collected Coin!");
            gameManager.IncreaseScore();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "StopMovement")        {
            Debug.Log("Movement Stoped");
            PlayerMovementinX.gameSpeed = 0;
        }

        if (other.gameObject.tag == "GameEnd")
        {
            Debug.Log("Completed Game -> GZ");
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

    }
}
