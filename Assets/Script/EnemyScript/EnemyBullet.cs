using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float lifeSpan = 5f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private int damage = 3;
    [SerializeField] private GameObject sfxImpact;

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
        rb.velocity = dir * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //LifeController life = collision.collider.GetComponent<LifeController>();

        if (sfxImpact != null) Instantiate(sfxImpact, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
