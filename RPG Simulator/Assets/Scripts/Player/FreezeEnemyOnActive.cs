using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEnemyOnActive : MonoBehaviour
{
    public GameObject enemyGameObject;

    private EnemyController enemyController;
    private Animator enemyAnimator;

    void Awake()
    {
        if (enemyGameObject != null)
        {
            enemyController = enemyGameObject.GetComponent<EnemyController>();
            enemyAnimator = enemyGameObject.GetComponent<Animator>();
        }
    }

    private void OnEnable()
    {
        if (enemyController != null && enemyAnimator != null)
        {
            enemyAnimator.SetFloat("speed", 0);
            enemyController.enabled = false;
        }
    }

    private void OnDisable()
    {
        if (enemyController != null)
        {
            enemyController.enabled = true;
        }
    }
}