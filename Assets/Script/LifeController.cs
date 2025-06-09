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



    public void SetHp(int hp)
    {
        life = hp;
        if (life <= 0)
        {

            lifeBehaviour = LIFE_BEHAVIOUR.DESTROY;

            GetComponent<Animator>().Play("Death");
            


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
                //provvisorio
                //animazione di danno 
                GetComponent<Animator>().Play("Hitted");
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
}
