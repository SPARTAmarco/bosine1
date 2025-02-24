using UnityEngine;

public class playerCombact : MonoBehaviour
{
    [SerializeField]
    private bool combactEnabled;
    [SerializeField]
    private bool inputTimer;

    private bool gotInput, isAttacking, isFirstAttack;

    private float lastInputTime;

    private Animator animator;

    private void Start()
    {
        animator =GetComponent<Animator>();
        animator.SetBool("canAttack", combactEnabled);
    }

    private void Update()
    {
        CheckCombatInput();
    }

    private void CheckCombatInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(combactEnabled)
            {
                //Attempt combact
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }

    private void ChecksAttack()
    {
        if(gotInput)
        {
            //perfrorm attack1
            if (!isAttacking)
            {
                gotInput =false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                animator.SetBool("attack1", true);
                animator.SetBool("firstAttack", isFirstAttack);
                animator.SetBool("isAttacking", isAttacking);
            }
        }

        if(Time.time >=lastInputTime)
        {
            //wait for a new input
            gotInput =false;
        }
    }

}
