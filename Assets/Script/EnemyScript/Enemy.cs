using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType {None = 0, Melee = 1, Suicide = 2, Range = 3, Boss = 4}
    [SerializeField] private EnemyType enemyTypeMovement;
    [SerializeField] private EnemyType enemyTypeAttack;

    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private PlayerController playerController;
    private Animator anim;

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();
        anim = GetComponentInChildren<Animator>();

        playerController = FindAnyObjectByType<PlayerController>();

        enemyAttack.playerController = playerController;
        enemyAttack.SetEnemyAttack(enemyTypeAttack);
    }

    private void Update()
    {
        anim.SetFloat("xDir", enemyMovement.direction.x);

        EnemyLogicAttack();
    }
    private void FixedUpdate()
    {
        EnemyLogicMovement();
    }

    private void EnemyLogicAttack()
    {
        switch (enemyTypeAttack)
        {
            default:
                Debug.Log("Empty");
                break;
            case EnemyType.None:
                break;
            case EnemyType.Melee:
                enemyAttack.OnAttackMelee();
                break;
            case EnemyType.Suicide:
                break;
            case EnemyType.Range:
                enemyAttack.OnAttackMelee();
                enemyAttack.TryShootCloseOnTarget(playerController);
                break;
            case EnemyType.Boss:
                enemyAttack.OnAttackMelee();
                enemyAttack.TryShootCloseOnTarget(playerController);
                break;
        }
    }
    private void EnemyLogicMovement()
    {
        switch (enemyTypeMovement)
        {
            default:
                Debug.Log("Empty");
                break;
            case EnemyType.None:
                break;
            case EnemyType.Melee:
                enemyMovement.MoveOnTarget(playerController);
                break;
            case EnemyType.Suicide:
                enemyMovement.PositionOnTarget(playerController);
                break;
            case EnemyType.Range:
                enemyMovement.MoveOnPosition(playerController);
                break;
            case EnemyType.Boss:
                enemyMovement.PositionOnTarget(playerController);
                break;
        }
    }
}
