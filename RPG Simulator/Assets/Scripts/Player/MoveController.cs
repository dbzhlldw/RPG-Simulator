using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;

    private bool canMove = true;

    private void Awake()
    {
        //animator = GetComponent<Animator>();
    }

    public void SetMovement(bool status)
    {
        canMove = status;
        if (!canMove)
        {
            //animator.SetBool("isWalking", false);
        }
    }

    public void Move(float inputValue)
    {
        if (canMove)
        {
            if (inputValue != 0)
            {
                transform.position += new Vector3(inputValue * moveSpeed * Time.deltaTime, 0, 0);
                //animator.SetBool("isWalking", true);
            }
            else
            {
                //animator.SetBool("isWalking", false);
            }
        }
    }
}
