using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyProjectiles : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "RedStar") {
            Debug.Log("Destroyed RedStar");
            Destroy(other.gameObject);
        }

        if (other.tag == "Projectile")
        {
            Debug.Log("Destroyed Projectile");
            Destroy(other.gameObject);
        }

        if (other.tag == "EndProjectileBangBangIntoDraagon")
        {
            Debug.Log("Destroyed Star");
            Destroy(other.gameObject);
        }
    }

}
