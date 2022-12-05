using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerFireBall : MonoBehaviour
{
    public float damage;
    public int range;
   
    private Rigidbody rb;
    private Collider collider;
    private bool fired;
    public bool doubleExplosion;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip explosionSoundA;
    [SerializeField]
    private AudioClip explosionSoundB;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        rb.detectCollisions = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (fired)
        {
            range--;
            if (range <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
      
        GameObject explode=GameObject.Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        audioSource=explode.GetComponent<AudioSource>();
        if (audioSource)
        {
            if (gameObject.transform.localScale.z > 1)
            {
                audioSource.clip = explosionSoundA;
                audioSource.Play();
            }
            else
            {
                audioSource.clip = explosionSoundB;
                audioSource.Play();
            }
        }
        explode.transform.localScale = gameObject.transform.localScale * 4;
        Collider[] tmp = Physics.OverlapSphere(collision.gameObject.transform.position, gameObject.transform.localScale.z*4f);
        foreach (Collider collider in tmp)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                if (collider.gameObject.TryGetComponent(out EnemyHealth eHealth))
                {
                    int a = Convert.ToInt32(damage);
                    eHealth.TakeDamage(a);
                }
            }
        }

        if (!doubleExplosion)
        {
            Destroy(this.gameObject);
        }
        else
        {
            rb.velocity = Vector3.Reflect(collision.relativeVelocity * -1, collision.contacts[0].normal);
            doubleExplosion = false;
        }
    }
    public void Fire()
    {
        rb.detectCollisions = true;
        collider.enabled = true;
        fired = true;
    }
    
}
