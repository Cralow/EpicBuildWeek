using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType {None = 0, Melee = 1, Suicide = 2, Range = 3}
    [SerializeField] private EnemyType enemyTypeMovement;
    [SerializeField] private EnemyType enemyTypeAttack;

    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private PlayerController playerController;

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void Update()
    {
        EnemyLogicAttack();
    }
    private void FixedUpdate()
    {
        EnemyLogicMovement();
    }

    private void EnemyLogicAttack()
    {
        switch (enemyTypeMovement)
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
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.GetComponent<PlayerController>() == playerController)
        {
            if (enemyTypeMovement == EnemyType.Suicide) enemyAttack.DoSuicideExplosion(collision);
        }
    }
}
