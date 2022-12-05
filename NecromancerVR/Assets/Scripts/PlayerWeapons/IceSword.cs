using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSword : MonoBehaviour
{
    public float damage;
    public bool isHeld;
    [SerializeField]
    private float range;
    private Rigidbody rb;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> swordHitSounds;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if(!isHeld)
        {
            range-=Time.deltaTime;
            if(range<=0)
            {
                Destroy(gameObject);
            }
           
        }
    }
   
    private void OnTriggerEnter(Collider collision)
    {
        
      
        if (isHeld)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                if (collision.gameObject.TryGetComponent(out EnemyHealth eHealth)&&rb)
                {

                    float e = rb.velocity.magnitude;
                    Debug.Log("mag"+ e);
                    if(e>20)
                    {
                        e = 1;
                    }
                    if(e>10)
                    {
                        e = 10;
                    }
                    if (e > 5)
                    {
                        int z = Random.Range(0, swordHitSounds.Count);
                        if (!audioSource.isPlaying)
                        {
                            audioSource.clip = swordHitSounds[z];
                            audioSource.Play();
                        }
                    }
                    e *= damage;
                    eHealth.TakeDamage(e);
                    Debug.Log("Damage : "+e);
                }
            }
        }
    }
    
}
