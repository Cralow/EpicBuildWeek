using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class EnemyAttack : MonoBehaviour
{
    [Header("Melee Option")]
    [SerializeField] int damageMelee = 5;
    [SerializeField] private float timerAttkMelee = 0.5f;

    [Header("Range Option")]
    [SerializeField] private EnemyBullet bulletPreFab;
    [SerializeField] private int damageBullet = 5;
    [SerializeField] private float speedBullet = 5;
    [SerializeField] float fireRate = 0.5f;

    [Header("Suicide Option")]
    [SerializeField] int damageExplosion = 20;

    private float lastShootTimer = 0f;
    private float lastTimeAttkMelee = 0;

    private bool canAttkMelee;

    private EnemyAudio enemyAudio;
    private EnemyType enemyTypeAttack;

    private void Start()
    {
        enemyAudio = GetComponent<EnemyAudio>();
    }

    public void SetEnemyAttack(EnemyType enemyAttack) => enemyTypeAttack = enemyAttack;

    public void OnAttackMelee() => canAttkMelee = Time.time - lastTimeAttkMelee >= timerAttkMelee;

    public void ShootCloseOnTarget(PlayerController pc)
    {
        if (pc == null) return;
        EnemyBullet bullet = Instantiate(bulletPreFab,transform.position,Quaternion.identity,transform);

        Vector3 randomPos = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3));
        Vector3 randomPosTarget = pc.transform.position + randomPos;    

        bullet.BulletPositionDirection(randomPosTarget - transform.position);
        bullet.DamageBullet = damageBullet;
        bullet.SpeedBullet = speedBullet;

        lastShootTimer = Time.time;
    }

    public void TryShootCloseOnTarget(PlayerController pc)
    {
        if(pc == null) return;
        bool canShoot = Time.time  - lastShootTimer >= fireRate;
        float dist = Vector2.Distance(transform.position, pc.transform.position);

        if (!canShoot || dist >= 20) return;
        ShootCloseOnTarget(pc);
    }

    public void DoSuicideExplosion(Collision2D collision)
    {
        DoDamageToPlayerOnSuicide(collision, damageExplosion);

        LifeController life = GetComponent<LifeController>();
        life.AddHp(-life.life);
    }

    private void DoDamageToPlayerOnSuicide(Collision2D collision,int damage)
    {
        PlayerController player = GetComponent<PlayerController>();
        if (player == null) return;
        LifeController lifeController = player.GetComponent<LifeController>();

        lifeController.AddHp(-damage);
        enemyAudio.AttkSuicideSound();
    }
    private void DoDamageToPlayerAttckMelee(Collider2D collider, int damage)
    {
        LifeController lifeController = collider.GetComponent<LifeController>();

        lifeController.AddHp(-damage);
        enemyAudio.AttkMeleeSound();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<PlayerController>())
        {
            if (enemyTypeAttack == EnemyType.Suicide) DoSuicideExplosion(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        PlayerController pc = collider.GetComponent<PlayerController>();
        if (pc == null) return;
        if(!canAttkMelee) return;

        DoDamageToPlayerAttckMelee(collider, damageMelee);
        lastTimeAttkMelee = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerController pc = collider.GetComponent<PlayerController>();
        Debug.Log(collider);
        if (pc == null) return;

        DoDamageToPlayerAttckMelee(collider, damageMelee);
        lastTimeAttkMelee = Time.time;
    }
}
