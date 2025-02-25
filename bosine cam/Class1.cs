using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator; // Riferimento all'animatore
    public Transform attackPoint; // Punto da cui parte l'attacco
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public LayerMask enemyLayers; // Layer dei nemici

    private float attackRate = 1f;
    private float nextAttackTime = 0f;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.X)) // Attacco con il tasto "X"
            {
            {
                attack 1();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void attack 1()
    {
        // Attiva animazione di attacco
        animator.SetTrigger("attack 1");

        // Controlla i nemici colpiti
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
