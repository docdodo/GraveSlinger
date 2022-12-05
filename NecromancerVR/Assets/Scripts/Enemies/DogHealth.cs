using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogHealth : EnemyHealth
{
    private bool speedUp;
    public float SpeedIncrease;
    public Animator anim;
    public override void TakeDamage(float damage_)
    {
        health -= damage_;
        if (!Dead)
        {
            if(!speedUp&& health <= Maxhealth / 2)
            {
                GetComponent<EnemyMovement>().SetPermanentSpeed(SpeedIncrease);
                    speedUp = true;
                anim.SetBool("Fast", true);
            }


            if (health <= 0)
            {
                Dead = true;
                Die();
            }
            else
            {
                healthSlider.value = health;
            }
           
        }
    }
}
