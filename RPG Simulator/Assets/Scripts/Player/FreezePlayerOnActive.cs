using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePlayerOnActive : MonoBehaviour
{
    public GameObject playerGameObject;

    private PlayerController playerController;
    private Animator playerAnimator;

    void Awake()
    {
        if (playerGameObject != null)
        {
            playerController = playerGameObject.GetComponent<PlayerController>();
            playerAnimator = playerGameObject.GetComponent<Animator>();
        }
    }

    private void OnEnable()
    {
        if (playerController != null && playerAnimator != null)
        {
            playerAnimator.SetFloat("speed", 0);
            playerController.enabled = false;
        }
    }

    private void OnDisable()
    {
        if (playerController != null)
        {
            playerController.enabled = true;
        }
    }
}