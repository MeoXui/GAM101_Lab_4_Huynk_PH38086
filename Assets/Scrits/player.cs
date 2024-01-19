using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{

    public Rigidbody2D rigidbody2d;
    public Transform transform;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public float jumpPower,
        moveSeped;
    Vector3 move;
    bool isCanJump = false, isDead = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Door")
        {
            SceneManager.LoadScene("EndScene");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Mushroom")
        {
            isDead = true;
            animator.SetBool("dead", true);
        }
        else if (other.gameObject.tag == "Ground")
        {
            isCanJump = true;
            animator.SetBool("jump", false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        move = new Vector3(moveSeped, 0, 0) * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) dead();
        else if ((Input.GetKeyDown(KeyCode.Space)
            || Input.GetKeyDown(KeyCode.UpArrow)
            || Input.GetKeyDown(KeyCode.W))
            && isCanJump) jump();
        else if (Input.GetKey(KeyCode.LeftArrow)
            || Input.GetKey(KeyCode.A)) moveLeft();
        else if (Input.GetKey(KeyCode.RightArrow)
            || Input.GetKey(KeyCode.D)) moveRight();
        else idle();
        if (Input.GetMouseButtonDown(0)) attack();
    }

    void dead()
    {
        animator.SetBool("idle", false);
        animator.SetBool("run", false);
        animator.SetBool("jump", false);
    }

    void idle()
    {
        animator.SetBool("idle", true);
        animator.SetBool("run", false);
    }

    void jump()
    {
        rigidbody2d.AddForce(Vector2.up* jumpPower, ForceMode2D.Impulse);
        isCanJump = false;
        animator.SetBool("jump", true);
    }

    void fall()
    {
        rigidbody2d.AddForce(Vector2.down, ForceMode2D.Impulse);
        animator.SetBool("fall", true);
    }

    void moveLeft()
    {
        //spriteRenderer.flipX = true;
        transform.localScale = new Vector3(-1, 1, 1);
        transform.Translate(-move);
        animator.SetBool("idle", false);
        animator.SetBool("run", true);
    }

    void moveRight()
    {
        //spriteRenderer.flipX = false;
        transform.localScale = new Vector3(1, 1, 1);
        transform.Translate(move);
        animator.SetBool("idle", false);
        animator.SetBool("run", true);
    }

    void attack()
    {
        animator.SetBool("attack", true);
    }
}
