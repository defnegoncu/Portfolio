using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixxieShoot : MonoBehaviour
{
    [SerializeField] float delayTime = 3.0f;
    float timeRemaining;

    private int shootCounter;

    //Not sure if Serialize works with Prefab Autogeneration 
    [SerializeField] Shoot shoot; 

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = delayTime;
        shoot = FindObjectOfType<Shoot>();
    }

     // Update is called once per frame
    void Update()
    {

        if (shootCounter > 2) Destroy(this.gameObject);
        
       

        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 0)
        {
            shootCounter++;

            timeRemaining = delayTime;
            //this= Pixxie, Child[0]= Lightball
            FindObjectOfType<AudioManager>().Play("pixxieshoot");
            shoot.ShootProjectile(this.gameObject, this.transform.GetChild(0).gameObject,true);
        }
    }
}
