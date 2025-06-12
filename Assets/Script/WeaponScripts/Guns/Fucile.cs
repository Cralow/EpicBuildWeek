using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FucileAPompa : Arma
{
    public int pelletsPerShot = 7;
    public float spreadAngle = 15f; 

    public override void Shoot()
    {
        FindNearestEnemy();

        if (savedEnemyObj == null)return;


        Vector2 fireDirection = savedEnemyObj.transform.position - transform.position;

        for (int i = 0; i < pelletsPerShot; i++)
        {

            float randomAngle = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);

            Vector2 spreadDir = Quaternion.Euler(0, 0, randomAngle) * fireDirection;

            var a = Instantiate(bullet, transform);

            a.GetComponent<Rigidbody2D>().AddForce(fireDirection.normalized * shootForce, ForceMode2D.Impulse);
        }
    }
}