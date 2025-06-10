using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Melee Option")]
    [SerializeField] int damageMelee = 5;
    [SerializeField] private float timerAttkMelee = 0.5f;

    [Header("Range Option")]
    [SerializeField] private EnemyBullet bulletPreFab;
    [SerializeField] float fireRate = 0.5f;

    [Header("Suicide Option")]
    [SerializeField] int damageExplosion = 20;

    private float lastShootTimer = 0f;

    private float lastTimeAttkMelee = 0;
    private bool canAttkMelee;
    private EnemyAudio enemyAudio;
    private void Start()
    {
        enemyAudio = GetComponent<EnemyAudio>();
    }
    public void ShootCloseOnTarget(PlayerController pc)
    {
        EnemyBullet bullet = Instantiate(bulletPreFab,transform.position,Quaternion.identity);

        Vector3 randomPos = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3));
        Vector3 randomPosTarget = pc.transform.position + randomPos;    

        bullet.BulletPositionDirection(randomPosTarget - transform.position);
        lastShootTimer = Time.time;
    }

    public void TryShootCloseOnTarget(PlayerController pc)
    {
        bool canShoot = Time.time  - lastShootTimer >= fireRate;
        float dist = Vector2.Distance(transform.position, pc.transform.position);

        if (!canShoot || dist >= 20) return;
        ShootCloseOnTarget(pc);
    }

    public void DoSuicideExplosion(Collision2D collision)
    {
        DoDamageToPlayerOnSuicide(collision, damageExplosion);

        Destroy(gameObject);
    }

    public void OnAttackMelee()
    {
        canAttkMelee = Time.time - lastTimeAttkMelee >= timerAttkMelee;
    }

    private void DoDamageToPlayerOnSuicide(Collision2D collision,int damage)
    {
        //LifeController life = collision.GetComponent<LifeController>();
        //if(life !=) Do Damage

        enemyAudio.AttkSuicideSound();
        Debug.Log("Make Damage on Touch " + damage);
    }
    private void DoDamageToPlayerAttckMelee(Collider2D collider, int damage)
    {
        //LifeController life = collision.GetComponent<LifeController>();
        //if(life !=) Do Damage

        enemyAudio.AttkMeleeSound();
        Debug.Log("Make Damage on Touch " + damage);
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
        if (pc == null) return;

        DoDamageToPlayerAttckMelee(collider, damageMelee);
        lastTimeAttkMelee = Time.time;
    }
}
