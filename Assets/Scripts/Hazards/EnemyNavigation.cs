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

    [Header("Rules")]
    [SerializeField] ScriptableRule invertedGravityRule;


    #region GravityInvertedRule
    private RuleListener gravityInvertedRuleListener;
    private void InvertGravity()
    {
        rb.gravityScale = -Mathf.Abs(rb.gravityScale);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); 


        transform.position += Vector3.up * 0.05f;

        transform.localScale = new Vector3(1f, -1f, 1f);
    }

    private void MakeGravityNormal()
    {
        rb.gravityScale = Mathf.Abs(rb.gravityScale);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

        transform.position += Vector3.down * 0.05f;

        transform.localScale = new Vector3(1f, 1f, 1f);
    }
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        facingRight = Mathf.Abs(transform.rotation.y) == 0 ? true : false;

        gravityInvertedRuleListener = new RuleListener(invertedGravityRule, InvertGravity, MakeGravityNormal);
        gravityInvertedRuleListener.AddSubscription();
    }

    private void OnDisable()
    {
        gravityInvertedRuleListener.RemoveSubscription();
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


    protected void Rotate()
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
        Rotate();
        anim.SetBool("Idle", false);
        isIdle = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == gameObject.layer)
        {
            Rotate();
        }
    }
}
