using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyHealth : MonoBehaviour
{
    [SerializeField]
     protected float health;
    public int life;
    public float Maxhealth;
    [SerializeField]
    private int value;
    [SerializeField]
    protected Slider healthSlider;
    protected bool Dead;
    // Start is called before the first frame update
    void Start()
    {
        
        health = Maxhealth;
        healthSlider.maxValue =  Maxhealth;
        healthSlider.value =  health;
    }

    // Update is called once per frame
    
    public virtual void TakeDamage(float damage_)
    {
        health -= damage_;
        if (!Dead)
        {


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
    protected virtual void Die()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            player.GetComponent<Player>().AddMoney(value);
       
           
        

            DestroyObject(this.gameObject);
    }
   public virtual void IncreaseBaseHealth(float increase_)
    {
        Maxhealth *= increase_;
        health = Maxhealth;
        healthSlider.maxValue = Maxhealth;
        healthSlider.value = health;
    }

    protected virtual void OnDestroy()
    {
        GameObject spawnManager = GameObject.FindGameObjectWithTag("SpawnManager");
        if (spawnManager != null)
            spawnManager.GetComponent<SpawnManager>().EnemyDead();
    }
}
