using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Velocity")]
    [SerializeField] private bool randomVelocity = false;
    [SerializeField] private int speed = 6;
    [SerializeField] private int minSpeed = 4;
    [SerializeField] private int maxSpeed = 12;

    [Header("OptionRange")]
    [SerializeField] private float minDistanceTarget = 7f;
    [SerializeField] private float maxDistanceTarget = 9f;

    [Header("OptionCicle")]
    [SerializeField] private bool  randomCicle = false;
    [SerializeField] private float rotationSpeed = 0.5f;
    [SerializeField] private float circleRadius = 7;
    [SerializeField] private float minCircleRadius = 4f;
    [SerializeField] private float maxCircleRadius = 10f;

    private Rigidbody2D rb;
    private Vector3 setCicle;
    private float angle;
    private float factor;
    private float rangeRandomX;
    private float rangeRandomY;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        RandomSetting();
    }

    private void RandomSetting()
    {
        RandomVelocity();
        RandomCiclePreset();
        RandomRangePosOnTarget();
    }
    public void PositionOnTarget(PlayerController pc)
    {
        if (pc == null) return;
        Vector3 posPlayer = pc.transform.position;
        setCicle.Set(Mathf.Cos(angle) * circleRadius,// X
                     Mathf.Sin(angle) * circleRadius,// Y
                                                  0);// Z

        Vector3 posTarget = posPlayer + setCicle;
        Vector2 dir = (posTarget - transform.position).normalized;

        rb.MovePosition(rb.position + dir * (speed * Time.fixedDeltaTime));
        angle += Time.fixedDeltaTime * rotationSpeed;
    }
    public void MoveOnPosition(PlayerController pc)
    {
        if (pc == null) return;
        Vector3 posPlayer = pc.transform.position;
        Vector2 posTarget = (posPlayer - transform.position).normalized;  

        Vector3 posPlayerAround = pc.transform.position;
        posPlayerAround.x += factor * rangeRandomX; posPlayerAround.y += factor * rangeRandomY;

        Vector2 posTargetAround = (posPlayerAround - transform.position).normalized;

        float dist = Vector2.Distance(transform.position, posPlayer);

        if (dist >= maxDistanceTarget + 1) rb.MovePosition(rb.position + posTargetAround * (speed * Time.fixedDeltaTime));
        if (dist <= minDistanceTarget - 1) rb.MovePosition(rb.position - posTarget * (speed * Time.fixedDeltaTime));
    }
    public void MoveOnTarget(PlayerController pc)
    {
        if (pc == null) return;
        Vector2 posTarget = (pc.transform.position - transform.position).normalized;
        rb.MovePosition(rb.position + posTarget * (speed * Time.fixedDeltaTime));
    }
    private void RandomVelocity()
    {
        if(!randomVelocity) return;

        speed = Random.Range(minSpeed, maxSpeed);
    }

    private void RandomCiclePreset()
    {
        if (!randomCicle) return;

        float factor = Random.Range(0, 2) == 0 ? -1f : 1f;

        rotationSpeed = factor * Random.Range(0.25f, 0.75f);

        circleRadius = 0;
        circleRadius += Random.Range(minCircleRadius, maxCircleRadius);

        rotationSpeed = speed / circleRadius;
        rotationSpeed *= factor;
    }

    private void RandomRangePosOnTarget()
    {
        factor = Random.Range(0, 2) == 0 ? -1f : 1f;

        rangeRandomX = Random.Range(5, 10);
        rangeRandomY = Random.Range(5, 10);
    }
}
