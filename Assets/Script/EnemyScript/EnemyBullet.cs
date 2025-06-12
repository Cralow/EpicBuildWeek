using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float lifeSpan = 5f;
    [SerializeField] private float speedBullet = 5f;
    [SerializeField] private int damageBullet = 3;
    [SerializeField] private GameObject sfxImpact;
    public int DamageBullet
    {
        get => damageBullet;
        set => damageBullet = Mathf.Max(1,value);
    }
    public float SpeedBullet
    {
        get => speedBullet;
        set => speedBullet = Mathf.Max(1, value);
    }

    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    public void BulletPositionDirection(Vector2 dir)
    {
        dir = dir.normalized;
        rb.velocity = dir * speedBullet;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            LifeController life = collision.collider.GetComponent<LifeController>();
            life.AddHp(-damageBullet);
        }

        if (sfxImpact != null) Instantiate(sfxImpact, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
