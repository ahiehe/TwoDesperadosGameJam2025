using System.Collections;
using UnityEngine;

public class EnemyNavigation : MonoBehaviour
{
    [SerializeField] private Transform leftBorder;
    [SerializeField] private Transform rightBorder;
    [SerializeField] protected float walkSpeed;

    protected Rigidbody2D rb;
    [SerializeField] protected Animator anim;

    protected bool isIdle = false;
    protected bool facingRight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        facingRight = Mathf.Abs(transform.rotation.y) == 0 ? true : false;
    }

    protected virtual void Update()
    {
        if (!isIdle && ReachedEdge())
        {
            StartCoroutine(Idle());
        }
    }

    private bool ReachedEdge()
    {
        var x = transform.position.x;
        return (!facingRight && x <= leftBorder.position.x) || (facingRight && x >= rightBorder.position.x);
    }

    private void FixedUpdate()
    {
        if (!isIdle)
        {
            Move();
        }

    }

    protected virtual void Move()
    {
        rb.linearVelocity = new Vector2((facingRight ? 1 : -1) * walkSpeed, rb.linearVelocity.y);
    }


    private void Flip()
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

    private void EnterIdle()
    {
        isIdle = true;
        anim.SetBool("Idle", true);
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    private IEnumerator Idle()
    {
        EnterIdle();
        yield return new WaitForSeconds(1);
        Flip();
        anim.SetBool("Idle", false);
        isIdle = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == gameObject.layer)
        {
            Flip();
        }
    }
}
