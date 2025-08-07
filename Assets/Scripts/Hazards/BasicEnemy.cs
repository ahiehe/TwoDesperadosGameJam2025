using System.Collections;
using UnityEngine;

public class BasicEnemy : EnemyNavigation
{
    private bool flipped = false;

    protected override void Update()
    {
        base.Update();
    }

    protected override void Move()
    {
        if (flipped)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        base.Move();
    }

    public void TakeDamage(float damage)
    {
        if (!flipped)
        {
            StartCoroutine(Flipped());
        }

    }

    private IEnumerator Flipped()
    {
        flipped = true;
        gameObject.layer = 6;
        anim.SetBool("Flipped", true);
        yield return new WaitForSeconds(3);
        anim.SetBool("Flipped", false);
        yield return new WaitForSeconds(0.3f);
        gameObject.layer = 8;
        flipped = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().Die();
        }
    }
}
