using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float speed;
    private float horizontalDir;
    private float verticalDir;

    public Vector2 Direction { get; private set; }


    private Rigidbody2D rb;
    private WeaponHandler wH;
    private Animator anim;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        wH = GetComponentInChildren<WeaponHandler>();
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
        anim.SetFloat("xDir", Direction.x);
        anim.SetFloat("yDir", Direction.y);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("PickUp"))
        {
            var item = Instantiate(collision.gameObject, wH.shootPosition.parent);
            //item.GetComponent<SpriteRenderer>().enabled = false;

           Destroy(collision.gameObject);   
        }
    }





}
