using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public int health;
    private bool isDead = false;

    public float attackRange = 1.5f;
    public int attackDamage = 1;
    public float attackCooldown = 1.5f;
    public Transform attackPoint;
    public LayerMask playerLayer;
    private float lastAttackTime = 0;
    

    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            animator.SetFloat("speed", 0);
            StartCoroutine(Attack());
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 startPosition = rb.position;
        Vector2 direction = (player.position - transform.position).normalized;
        Vector2 newPosition = rb.position + direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);

        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);  // 向右移动
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // 向左移动
        }

        float actualSpeed = (newPosition - startPosition).magnitude / Time.fixedDeltaTime;
        animator.SetFloat("speed", actualSpeed);

    }

    IEnumerator Attack()
    {
        if (Time.time > lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            animator.SetTrigger("attack");

            yield return new WaitForSeconds(0.6f);

            Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
            if (hitPlayer != null)
            {
                hitPlayer.GetComponent<PlayerController>()?.TakeDamage(attackDamage);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Enemy died.");
        Destroy(gameObject);
    }
}