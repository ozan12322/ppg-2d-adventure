using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // mengarahkan karakter ketika bergerak ke kanan/kiri
        if(horizontalInput > 0.1f)
            transform.localScale = new Vector3(5, 5, 1);
        else if(horizontalInput < -0.1f)
            transform.localScale = new Vector3(-5, 5, 1);

        if(Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        // set animator parameter
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Ground")
            grounded = true;
    }
}
