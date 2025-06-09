using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField]private float maxLife;
     float life;

    [SerializeField] private LIFE_BEHAVIOUR lifeBehaviour;
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
                break;




        }
    }
}
