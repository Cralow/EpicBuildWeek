using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fucile : Arma
{
    public float burstDelay = 0.1f; // Ritardo tra un colpo e l'altro nella raffica

    public override void Shoot()
    {
        StartCoroutine(ShootBurst());
    }

    private IEnumerator ShootBurst()
    {
        FindNearestEnemy();

        if (savedEnemyObj != null)
        {
            for (int i = 0; i < 3; i++)
            {
                var a = Instantiate(bullet, transform.position, Quaternion.identity); 
                Vector2 fireDirection = savedEnemyObj.transform.position - transform.position;
                a.GetComponent<Rigidbody2D>().AddForce(fireDirection.normalized * shootForce, ForceMode2D.Impulse);

                yield return new WaitForSeconds(burstDelay); 
            }
        }
    }
}