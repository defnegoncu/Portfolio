using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] Transform playerForwardTransform;
  public  void ShootProjectile(GameObject Shooter,GameObject Projectile, bool shooterIsEnemy)
    {
        GameObject tmp = Instantiate(Projectile) as GameObject;
        tmp.transform.position = Shooter.transform.position;
        tmp.transform.parent = playerForwardTransform;
        tmp.SetActive(true);
        if (shooterIsEnemy)
        {
            tmp.GetComponent<ProjectileMovement>().enabled = true;
            tmp.GetComponent<PlayerProjectileMovement>().enabled = false;
        }
        else
        {
            tmp.GetComponent<ProjectileMovement>().enabled = false;
            tmp.GetComponent<PlayerProjectileMovement>().enabled = true;
        }
    }
}
