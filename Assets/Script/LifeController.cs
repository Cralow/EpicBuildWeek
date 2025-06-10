using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField]private float maxLife;
    public int life { get; private set; }

    [SerializeField] private LIFE_BEHAVIOUR lifeBehaviour;

    bool isEnemy;
    private int danno;

    private bool isAttackingPlayer;

    public void SetHp(int hp)
    {
        life = hp;
        if (life <= 0)
        {

            lifeBehaviour = LIFE_BEHAVIOUR.DESTROY;
            //animazione death comune enemys e player
           // GetComponent<Animator>().Play("Death");
            


        }

    }
    public void AddHp(int amount)
    {
        if (life <= maxLife)
        {

            int hp = amount += life;
            SetHp(hp);


            if (lifeBehaviour==LIFE_BEHAVIOUR.NONE)
            {

                //animazione di danno comune a enemys e player
                // GetComponent<Animator>().Play("Hitted");
            }


        }


    }

    private void Awake()
    {
        if(gameObject.tag == "Player"){
            isEnemy = false;
        }
        else
        {
            isEnemy = true;
        }
    }
    public enum LIFE_BEHAVIOUR
    {
        NONE,
        DISABLE,
        DESTROY,
    }




    private void Update()
    {

        switch (lifeBehaviour)
        {
            case LIFE_BEHAVIOUR.NONE:
                break;

            case LIFE_BEHAVIOUR.DESTROY:
                //DESTROY
                Destroy(gameObject, 1f);
                break;




        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isAttackingPlayer = true;
        }
        if (collision.transform.CompareTag("Enemy"))
        {
            isAttackingPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isAttackingPlayer = false;

        }
        if (collision.transform.CompareTag("Enemy"))
        {
            isAttackingPlayer = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (isEnemy && collision.transform.tag == "Gun")
        //{
        //    danno = collision.gameObject.GetComponent<Bullet>().bDmg;
        //    Destroy(collision.gameObject);
        //    gameObject.GetComponent<Animator>().Play("EnemyHitted");
        //    AddHp(-danno);
        //}

        if (!isEnemy && collision.transform.tag == "Enemy")
        {

           





        }


    }

}
