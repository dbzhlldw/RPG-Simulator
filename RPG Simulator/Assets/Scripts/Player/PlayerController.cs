using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MoveController moveController;
    //private Interactable currentInteractable; // Track the current interactable the player is looking at
    private Animator animator;

    public int health = 10;
    private bool isDead = false; 

    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayers;
    public int attackDamage = 3;

    void Awake()
    {
        moveController = GetComponent<MoveController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();

        if (!isDead && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            StartCoroutine(Attack());
        }
    }

    private void HandleMovement()
    {
        if (isDead || animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            return;
        }

        float moveInput = Input.GetAxis("Horizontal");
        moveController.Move(moveInput);
        animator?.SetFloat("speed", Mathf.Abs(moveInput));
        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    IEnumerator Attack()
    {
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(0.6f);
        

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyController>()?.TakeDamage(attackDamage);
        }
        
    }


    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        Debug.Log("Player took " + damage + " damage! Remaining health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Player died!");
        Destroy(gameObject);
    }
}
