using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    private float horizontalDir;
    private float verticalDir;

    public Vector2 Direction { get; private set; }
    private Rigidbody2D rb;





    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        PlayerMovement();
    }
    public void PlayerMovement()
    {

        horizontalDir = Input.GetAxis("Horizontal");
        verticalDir = Input.GetAxis("Vertical");
        Direction = new Vector2(horizontalDir, verticalDir).normalized;
        rb.velocity = Direction * (speed + Time.deltaTime);

    }
}
