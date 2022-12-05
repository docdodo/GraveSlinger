using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerMovement : EnemyMovement
{
    public int sprintTimeMax;
    private int sprintTime;
    public float postChargeSpeed;
    bool sprintEnded;
    Animator animator;
    // Start is called before the first frame update
    protected override void  Start()
    {
        base.Start();
        sprintTime = sprintTimeMax;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!sprintEnded)
        {
            if (sprintTime <= 0)
            {
                sprintEnded = true;
                ChargeEnd();
                animator.SetTrigger("ChargeEnd");
            }
            else
            {
                sprintTime--;
            }
        }
    }
    private void ChargeEnd()
    {
        speed = postChargeSpeed;
        SetSpeed(speed);
    }
}
