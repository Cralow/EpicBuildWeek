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
                Debug.Log("None Attack");
                break;
            case EnemyType.Melee:
                Debug.Log("Do Melee Attack");
                enemyAttack.OnAttackMelee();
                break;
            case EnemyType.Suicide:
                Debug.Log("Do Suicide Attack");
                break;
            case EnemyType.Range:
                Debug.Log("Do Range Attack");
                enemyAttack.OnAttackMelee();
                enemyAttack.TryShootCloseOnTarget(playerController);
                break;
            case EnemyType.Boss:
                Debug.Log("Do Boss Attack");
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
                Debug.Log("None Movement");
                break;
            case EnemyType.Melee:
                Debug.Log("Do Melee Movement");
                enemyMovement.MoveOnTarget(playerController);
                break;
            case EnemyType.Suicide:
                Debug.Log("Do Suicide Movement");
                enemyMovement.PositionOnTarget(playerController);
                break;
            case EnemyType.Range:
                Debug.Log("Do Range Movement");
                enemyMovement.MoveOnPosition(playerController);
                break;
            case EnemyType.Boss:
                Debug.Log("Do Boss Movement");
                enemyMovement.PositionOnTarget(playerController);
                break;
        }
    }
}
