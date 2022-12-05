using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Farmer : Tower
{
    private float speedTimer;
    private float bombTimer;
    private float bombDamage;
    [SerializeField]
    private float baseBombDamage;
    private bool bombActive;
    Player player;
    [SerializeField]
    private ParticleSystem system;
    [SerializeField]
    private GameObject bombPrefab;
    [SerializeField]
    private GameObject spawn;

    private GameObject bomb;
    [Header("Audio")]
    [SerializeField]
    private AudioSource audioSource;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        speedTimer = speed;
    }

    // Update is called once per frame
    protected override void  Update()
    {
        base.Update();
        ProduceIncome();
    }
    private void ProduceIncome()
    {
        speedTimer-=Time.deltaTime;
        if(speedTimer<=0)
        {
            speedTimer = speed;
            system.Play();
            int damage_ = Convert.ToInt32(damage);
            audioSource.Play();
            player.AddMoney(damage_);
        }
        if(bombActive)
        {
            if (bomb == null)
            {
                bombTimer-=Time.deltaTime;
                if (bombTimer <= 0)
                {
                    bombTimer = speed * 1.5f;
                    bomb = Instantiate(bombPrefab, spawn.transform.position, spawn.transform.rotation);
                    bomb.GetComponent<Bomb>().damage = bombDamage;
                }
            }
        }
    }
    protected override void CalculateDamage()
    {
        base.CalculateDamage();
        GameObject obj = GameObject.FindGameObjectWithTag("SaveManager");
        bombDamage = baseBombDamage;
        if (obj != null)
        {
            if (obj.GetComponent<SaveManager>().GetSkulls("GraveYard") >= 5)
            {
                damage += 1;
            }
            if (obj.GetComponent<SaveManager>().GetSkulls("GraveYard") >= 10)
            {
                bombActive = true;
                
            }
        }
    }
        
}
