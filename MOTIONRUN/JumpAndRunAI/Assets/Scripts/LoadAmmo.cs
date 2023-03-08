using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAmmo : MonoBehaviour
{
    float ammoTime=3.0f;
    float ammoTimeRemaining;
    bool gotAmmo = false;
    Shoot shoot;
    GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        shoot = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Shoot>();
        projectile = GameObject.FindGameObjectWithTag("Projectile");
        ammoTimeRemaining = ammoTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (gotAmmo)
        {
            ammoTimeRemaining -= Time.deltaTime;
            if (ammoTimeRemaining <= 0.0f)
            {
                gotAmmo = false;
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile"&&!gotAmmo)
        {
            projectile = other.gameObject;
            gotAmmo = true;
            ammoTimeRemaining = ammoTime;
            Destroy(other.gameObject);
        }
    }
    public void OnTriggerChanged(int analogId, float val)
    {
        if (val <= 0.5&&gotAmmo)
        {
            shoot.ShootProjectile(this.gameObject, projectile,false);
        }
    }

}
