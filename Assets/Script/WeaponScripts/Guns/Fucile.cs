using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FucileAPompa : Arma
{
    public int pelletsPerShot = 7;
    public float spreadAngle = 15f; // Angolo totale di apertura (in gradi)

    public override void Shoot()
    {
        FindNearestEnemy();

        if (savedEnemyObj == null) return;

        // Direzione centrale verso il nemico
        Vector2 baseDirection = (savedEnemyObj.transform.position - transform.position).normalized;

        for (int i = 0; i < pelletsPerShot; i++)
        {
            // Calcola un angolo random attorno alla direzione base
            float randomAngle = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);

            // Ruota la direzione base
            Vector2 spreadDir = Quaternion.Euler(0, 0, randomAngle) * baseDirection;

            // Istanzia e spara il pellet
            var pellet = Instantiate(bullet, transform.position, Quaternion.identity);
            pellet.GetComponent<Rigidbody2D>().AddForce(spreadDir * shootForce, ForceMode2D.Impulse);
        }
    }
}