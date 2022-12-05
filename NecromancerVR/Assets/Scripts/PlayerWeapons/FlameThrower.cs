using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
public class FlameThrower : UseableObject
{
   
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected int baseDamage;
    [SerializeField]
    protected int range;
    public GameObject fireBall;
    [SerializeField]
    private GameObject fireBallTemplate;
    [SerializeField]
    private GameObject spawnPosition;
    
    
    [SerializeField]
    private Vector3 increaseAmount;
    [SerializeField]
    private float chargerate;
    [SerializeField]
    private float baseChargerate;
    [SerializeField]
    private float basemaxSize;
    [SerializeField]
    private float maxSize;
    private bool isBeingUsed;
    private bool ballFired;
    private bool doubleExplosion;
    [Header("Audio")]
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioSource audioSource2;
    // Start is called before the first frame update
    void Awake()
    {

        ballFired = true;
        CalculateDamage();

    }

    // Update is called once per frame
    void Update()
    {
        if(isBeingUsed)
        {
            if(!fireBall)
            {
                fireBall = GameObject.Instantiate(fireBallTemplate, spawnPosition.transform.position, spawnPosition.transform.rotation);
                fireBall.GetComponent<PlayerFireBall>().doubleExplosion = doubleExplosion;
                audioSource.Play();
                //instatiate fireball
            }
            else if (fireBall.transform.localScale.z<maxSize)
            {
                
                fireBall.transform.localScale += increaseAmount;

               
            }
            if(fireBall)
            {
                fireBall.transform.position = spawnPosition.transform.position;
                fireBall.transform.rotation = spawnPosition.transform.rotation;
                if(fireBall.GetComponent<PlayerFireBall>().damage<damage)
                {
                    fireBall.GetComponent<PlayerFireBall>().damage += chargerate;
                }
                
                

            }
        }
        else if(ballFired==false)
        {
            audioSource.Stop();
            audioSource2.Play();
            ballFired = true;
            Vector3 force_ = fireBall.transform.forward*10;
            fireBall.GetComponent<Rigidbody>().AddForce(force_,ForceMode.Impulse);
            fireBall.GetComponent<Rigidbody>().useGravity = true;
           fireBall.GetComponent<PlayerFireBall>().Fire();
            
            fireBall = null;
        
         }
        isBeingUsed = false;
    }
    public override void Use()
    {

        Debug.Log("use");
        isBeingUsed = true;
        ballFired = false;
        


    }
    public override void AltUse()
    {
        Debug.Log("Altuse");
    }

    private void CalculateDamage()
    {
        damage = baseDamage;
        maxSize = basemaxSize;
        chargerate = baseChargerate;
        doubleExplosion = false;
        GameObject obj = GameObject.FindGameObjectWithTag("SaveManager");
        if (obj != null)
        {
            Debug.Log("OBJ=not null");
            Debug.Log(obj);
            damage +=(obj.GetComponent<SaveManager>().GetSkulls("FireBall")*6);
            maxSize += (obj.GetComponent<SaveManager>().GetSkulls("FireBall")*0.05f);
            chargerate += (obj.GetComponent<SaveManager>().GetSkulls("FireBall") * 0.05f);
            if (obj.GetComponent<SaveManager>().GetSkulls("FireBall") >= 5)
            {
                maxSize *= 1.2f;
            }
            if (obj.GetComponent<SaveManager>().GetSkulls("FireBall") >= 10)
            {
                doubleExplosion = true;
            }
            if (maxSize < 3)
                maxSize = 3;
        }

    }

    private void OnDisable()
    {

        
        if (fireBall)
        {


            audioSource.Stop();
            audioSource2.Play();
            ballFired = true;
            Vector3 force_ = fireBall.transform.forward * 10;
            fireBall.GetComponent<Rigidbody>().AddForce(force_, ForceMode.Impulse);
            fireBall.GetComponent<Rigidbody>().useGravity = true;
            fireBall.GetComponent<PlayerFireBall>().Fire();

            fireBall = null;
        }
    }
}
