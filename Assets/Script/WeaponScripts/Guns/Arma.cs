using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{
    public Transform bullet;
    [SerializeField] float weaponRange;
    public GameObject savedEnemyObj;




     public float fireRate;
    public float fireCoolDown;
    public float gunDamage;
    public float shootForce;

    public bool isEquipped;
    public Animator anim;


    public void PlayAttackAnim()
    {
        anim.Play("Attack");
    }
    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }
    void Update()
    {
        fireCoolDown -= Time.deltaTime;

        if (fireCoolDown <= 0f)
        {

                 Shoot();
                fireCoolDown = fireRate;
            





        }
    }
    public virtual void Shoot()
    {
        if (isEquipped)
        {
            FindNearestEnemy();
            if (savedEnemyObj != null)
            {
                var a = Instantiate(bullet);
                PlayAttackAnim();
                a.transform.position = transform.position;
                Vector2 fireDirection = savedEnemyObj.transform.position - transform.position;
                a.GetComponent<Rigidbody2D>().AddForce(fireDirection.normalized * shootForce, ForceMode2D.Impulse);


            }
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
                    if (distanzaAttuale < weaponRange)
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
