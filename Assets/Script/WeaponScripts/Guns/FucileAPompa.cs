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

        if (savedEnemyObj == null || isEquipped) return;


        Vector2 fireDirection = savedEnemyObj.transform.position - transform.position;
        PlayAttackAnim();
        for (int i = 0; i < pelletsPerShot; i++)
        {


            Vector2 spreadDir = Quaternion.Euler(Random.Range(-spreadAngle * 2f, spreadAngle * 2f), Random.Range(-spreadAngle * 2f, spreadAngle * 2f), transform.position.z) * fireDirection;

            var a = Instantiate(bullet);
            PlayAttackAnim();
            a.transform.position = transform.position;

            a.GetComponent<Rigidbody2D>().AddForce(spreadDir.normalized * shootForce, ForceMode2D.Impulse);
        }
    }
}