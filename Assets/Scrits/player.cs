using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public TextMeshProUGUI txtScore;
    int score = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            score++;
            txtScore.SetText(score.ToString());
        }
        else if (other.gameObject.tag == "Door")
        {
            score += 100;
            txtScore.SetText(score.ToString());
            SceneManager.LoadScene("EndScene");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Mushroom")
        {
            isDead = true;
            animator.SetBool("dead", true);
            SceneManager.LoadScene("SampleScene");
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
        if (Input.GetMouseButtonDown(0)) attack();

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
        transform.localScale = new Vector3(-1, 1, 1);
        transform.Translate(-move);
        animator.SetBool("idle", false);
        animator.SetBool("run", true);
    }

    void moveRight()
    {
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
