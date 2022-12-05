using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTower : Tower
{
    [SerializeField]
    private float chargerate;
    [SerializeField]
    private float baseChargerate;
    [SerializeField]
  private  GameObject dragon;
    [SerializeField]
    private GameObject fireball;
    [SerializeField]
    private GameObject spawnLocation;
    [SerializeField]
    private BoxCollider boxRange;
    [SerializeField]
    private Animator DragonAnimation;
    private bool enemyinRange;
    private float shootTimer;
    private bool doubleExplosion;
    private float maxSize;
    public float baseMaxSize;
    [Header("Audio")]
    [SerializeField]
    private AudioSource audioSource;
    protected override void Start()
    {
        base.Start();
        boxRange.size = new Vector3(range,range,range);
        
    }
    protected override void Update()
    {
        base.Update();
        if(enemyinRange)
        {
            shootTimer-=chargerate;
            if (shootTimer<=0)
            {
                Invoke("ShootFireBall", 0.2f);
               
                DragonAnimation.SetTrigger("Shoot");
                
                shootTimer = speed;
            }
           
        }
        enemyinRange = false;
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("stay");
        if (other.gameObject.tag == "Enemy")
        {
            
            dragon.transform.LookAt(other.gameObject.transform.position);
            enemyinRange = true;
        }
    }
    private void ShootFireBall()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
        GameObject obj = Instantiate(fireball, spawnLocation.transform.position, spawnLocation.transform.rotation);
        obj.GetComponent<FireBall>().damage = damage;
        obj.GetComponent<FireBall>().doubleExplosion = doubleExplosion;
        
    }
    protected override void CalculateDamage()
    {
        base.CalculateDamage();
        chargerate = baseChargerate;
        maxSize = baseMaxSize;
        GameObject obj = GameObject.FindGameObjectWithTag("SaveManager");
        if (obj != null)
        {
            damage += obj.GetComponent<SaveManager>().GetSkulls("Dragon")*1.5f;
            chargerate += obj.GetComponent<SaveManager>().GetSkulls("Dragon") * 0.05f;
            maxSize = baseMaxSize;
            if (obj.GetComponent<SaveManager>().GetSkulls("Dragon") >= 5)
            {
                maxSize *= 1.2f;
            }
            if (obj.GetComponent<SaveManager>().GetSkulls("Dragon") >= 10)
            {
                doubleExplosion = true;
            }
        }
    }
}
