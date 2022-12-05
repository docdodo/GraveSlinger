using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RageHealth : EnemyHealth
{
    private bool healthUp;
    public float healthIncreasePercent;
    public Animator anim;
    public NavMeshAgent agent;
    public bool canFly;
    private float timeToFly;
   
    private void Update()
    {
        if(timeToFly>0&&canFly)
        {
            agent.baseOffset += Time.deltaTime/2;
            timeToFly -= Time.deltaTime;
        }
    }
    public override void TakeDamage(float damage_)
    {
        Debug.Log("flyMaxHealth " + Maxhealth);
        Debug.Log("flycurrentHealth " + health);
        health -= damage_;
        if (!Dead)
        {
            if(!healthUp && health <= Maxhealth / 2)
            {
               
                healthUp = true;
                health += Maxhealth / 100 * healthIncreasePercent;
                anim.SetBool("Rage", true);
                healthSlider.value = health;
                if (canFly)
                    timeToFly = 1.0f;
                
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
