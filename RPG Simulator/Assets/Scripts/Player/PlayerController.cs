using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MoveController moveController;
    //private Interactable currentInteractable; // Track the current interactable the player is looking at
    private Animator animator;

    void Awake()
    {
        moveController = GetComponent<MoveController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        moveController.Move(moveInput);
        animator?.SetFloat("speed", Mathf.Abs(moveInput));
        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void event_PlayerHide(object[] args)
    {
        this.gameObject.SetActive(false);
    }

    private void event_PlayerShow(object[] args)
    {
        this.gameObject.SetActive(true);
    }
}
