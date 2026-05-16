using System.Collections;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private GameObject sprite;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float attackDistance = 1.2f;

    [Header("Attack")]
    [SerializeField] private float attackCooldown = 1.5f;

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;

    private bool isDead = false;
    private bool canAttack = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = sprite.GetComponent<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        animator.SetBool("IsWalking", true);
    }

    private void FixedUpdate()
    {
        if (player == null || isDead)
            return;

        float distance = Vector2.Distance(
            transform.position,
            player.position
        );

        // Chase player
        if (distance > attackDistance)
        {
            MoveToPlayer();
        }
        // Attack player
        else
        {
            rb.linearVelocity = Vector2.zero;

            animator.SetBool("IsWalking", false);

            if (canAttack)
            {
                StartCoroutine(AttackRoutine());
            }
        }

        FlipSprite();
    }

    private void MoveToPlayer()
    {
        Vector2 newPos = Vector2.MoveTowards(
            rb.position,
            player.position,
            moveSpeed * Time.fixedDeltaTime
        );

        rb.MovePosition(newPos);

        animator.SetBool("IsWalking", true);
    }

    private IEnumerator AttackRoutine()
    {
        canAttack = false;

        animator.SetTrigger("Attack");

        // Wait for attack animation
        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

    private void FlipSprite()
    {
        if (player.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackShot") && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        rb.linearVelocity = Vector2.zero;

        animator.SetBool("IsWalking", false);
        animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;

        Destroy(gameObject, 2f);
    }
}