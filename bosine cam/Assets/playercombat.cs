using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator; // Riferimento all'animatore

    public Transform attackpoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;


    void Update()
    {


        if (Input.GetKeyDown(KeyCode.X)) // Attacco con il tasto "X"
        {
            {
                attack();

            }
        }
        

        void attack()
        {
            // Attiva animazione di attacco
            animator.SetTrigger("Attack");

            //
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayers);

        }
    }
}