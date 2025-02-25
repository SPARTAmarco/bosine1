using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator; // Riferimento all'animatore


    void Update()
    {


        if (Input.GetKeyDown(KeyCode.X)) // Attacco con il tasto "X"
        {
            {
                attack ();

            }
        }
        

        void attack ()
        {
            // Attiva animazione di attacco
            animator.SetTrigger("attack 1");

        }
    }
}