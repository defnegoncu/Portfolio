using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.UI;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{

   

    

    Animator anim;
    public int health = 10;
    int targetPos;
    int zähler=0;
    public GameObject stern;
    public GameObject lightball;
    public GameObject roterstern;
    public GameObject tailcollider;
    Transform player;
    AutoScroll levelPos;
    Slider healthSlider;
    // Start is called before the first frame update
    void Start()
    {
        levelPos = GameObject.Find("GameManager").GetComponent<AutoScroll>();
        levelPos.bossbar.SetActive(true);
        healthSlider= GameObject.Find("BossHealthBar").GetComponent<Slider>();
       
        anim= GetComponent<Animator>();
        player = GameObject.Find("PlayerForward").transform;
        targetPos = levelPos.startRow + levelPos.LevelLänge;
        InvokeRepeating("SternHagel", 0f, 2f);
        InvokeRepeating("Logic", 5f,3f);

    }
    public void SpinCheckTrigger()
    {
        StartCoroutine(SpinCheck());
    }
    void SternHagel()
    {
        GameObject temp;
        int randXpos = Random.Range(0, 10);
        int randZpos = Random.Range(-5, 6);
        int sterntyp = Random.Range(0, 3);
        float speed;
        if (sterntyp == 0 || sterntyp == 1)
        {
            speed = 3.5f;
             temp=(GameObject)Instantiate(stern, new Vector3(-(targetPos + randXpos+11), levelPos.tileOffset, randZpos),Quaternion.identity);
        }
        else
        {
            speed = 5f;
             temp = (GameObject)Instantiate(roterstern, new Vector3(-(targetPos + randXpos+11), levelPos.tileOffset, randZpos), Quaternion.identity);
        }



        StartCoroutine(Hagel(temp,speed));
        zähler++;
    }

    IEnumerator Hagel(GameObject obj,float speed)
    {
        
        while(obj.transform.position.y>7500)
        {
            float pos = obj.transform.position.x;
            // interpolate position
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, new Vector3(pos+10, 7500f, obj.transform.position.z), speed*Time.deltaTime );

            // wait for end of frame to proceed
            yield return null;
        }
        Destroy(obj);
    }
    IEnumerator LichtballMov(Vector3 destination)
    {
        
        GameObject obj = (GameObject)Instantiate(lightball,new Vector3(-(targetPos+10f),levelPos.tileOffset,0f), Quaternion.identity);
        while(obj.transform.position!= destination)
        {
            // interpolate position
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, destination, 6*Time.deltaTime);

            // wait for end of frame to proceed
            yield return null;
        }
        Destroy(obj);

        yield return new WaitForSeconds(3);


    }

    public IEnumerator SpinCheck()
    {
        UnityEngine.Debug.Log("aidghlaidgh");
    
        GameObject obj = (GameObject)Instantiate(tailcollider, new Vector3(-(targetPos+5), 7501.69f, 8f), Quaternion.identity);
        FindObjectOfType<AudioManager>().PlayDelayedbyTime("tail", 3f);
        yield return new WaitForSecondsRealtime(3f);
        while (obj.transform.position.z != -8f)
        {
            // interpolate position
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, new Vector3(obj.transform.position.x,obj.transform.position.y,-8),  30* Time.deltaTime);

            // wait for end of frame to proceed
            yield return null;
        }
        Destroy(obj);


        
        
    }

    public IEnumerator BangBang()
    {
        FindObjectOfType<AudioManager>().Play("coin");
        UnityEngine.Debug.Log("works2");
        GameObject obj = (GameObject)Instantiate(stern, new Vector3(player.position.x,player.position.y+2f,player.position.z), Quaternion.identity);
        for (int i = 0; i < 100; i++)
        {
            // interpolate position
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, new Vector3(transform.position.x,transform.position.y+5f,transform.position.z),   Time.deltaTime *7);

            // wait for end of frame to proceed
            yield return null;
        }
        health--;
        healthSlider.value = health;
        Destroy(obj);
    }

    

     void Logic()    {

        
            

            

            int prob2 = Random.Range(0, 2);
            if (prob2 == 1)
            {

                UnityEngine.Debug.Log("Feuerball");
                Vector3 destinatation = GameObject.Find("FirstPersonPlayer").transform.position;
                destinatation = new Vector3(destinatation.x + 0.5f, destinatation.y - 0.5f, destinatation.z);
                StartCoroutine(LichtballMov(destinatation));
                anim.SetBool("Schuss", true);
                FindObjectOfType<AudioManager>().Play("dragonshoot");
                

            }
            else
            {

                
                    anim.SetTrigger("Spin");
                   
                
            }

        }

       

    

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("PlayerForward").transform;
        if (health==0)
        {
            CancelInvoke("Logic");
            levelPos.winscreen.SetActive(true);
        }
        
    }
}
