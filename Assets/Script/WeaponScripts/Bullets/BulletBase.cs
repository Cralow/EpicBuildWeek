using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    [SerializeField] public int damage;



    public float GetDamage()
    {
        return damage;
    }

    protected virtual void DestroyBullet()
    {
        Destroy(gameObject, 7f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.gameObject.GetComponent<LifeController>().AddHp(-damage);
            Destroy(gameObject);
        }
    }


}
