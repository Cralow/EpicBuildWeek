using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerGun : MonoBehaviour
{

    public Transform bullet;
    [SerializeField] float weaponRange;
    private GameObject savedEnemyObj;





    [SerializeField] float fireRate;
    public float fireCoolDown;
    public float gunDamage;


    void Update()
    {
        fireCoolDown -= Time.deltaTime;

        if (fireCoolDown <= 0f)
        {
            Shoot();

            fireCoolDown = fireRate;



        }
    }
    void Shoot()
    {
        FindNearestEnemy();
        if (savedEnemyObj != null)
        {
            var a = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            a.GetComponent<Rigidbody2D>().AddForce(savedEnemyObj.transform.position,ForceMode2D.Impulse);


        }
 

    }

    public GameObject FindNearestEnemy()
    {
        GameObject[] listaNemici = GameObject.FindGameObjectsWithTag("Enemy");



        if (listaNemici.Length > 0)
        {
            float distanzaAttuale;
            float distanza2 = 1000;


            foreach (GameObject go in listaNemici)
            {

                Vector3 diff = gameObject.transform.position - go.transform.position;
                distanzaAttuale = diff.magnitude;

                if (distanzaAttuale < distanza2)
                {
                    if(distanzaAttuale < weaponRange)
                    {
                    distanza2 = distanzaAttuale;

                    savedEnemyObj = go;

                    }
                    else
                    {
                        savedEnemyObj = null;
                    }

                }





            }
        }

        return savedEnemyObj;
     


    }

}
