using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : MonoBehaviour
{
    public float damage;
    public int range;
    private bool pierce;
    private Rigidbody rb;
    private bool hitTarget;
    private bool dealtDamage;
    private Collider collider;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        if (range <= 0)
            range = 10;
        rb.velocity = transform.forward*10;
    }

    // Update is called once per frame
    void Update()
    {

       
        if (!dealtDamage&&!hitTarget)
        {
            rb.velocity = transform.forward * 10;
        }
        else if(hitTarget&&!dealtDamage)
        {
            gameObject.transform.position += gameObject.transform.forward * 0.1f;
            
        }
        else if(!hitTarget&& dealtDamage)
        {

        }
            range--;
        if (range <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!dealtDamage&&!hitTarget)
            {
                
                    if (collision.gameObject.TryGetComponent(out EnemyHealth eHealth))
                        eHealth.TakeDamage(damage);

                    if (!pierce)
                    {
                        dealtDamage = true;
                    }
                    else
                    {
                        hitTarget = true;
                        rb.isKinematic = true;
                    collider.isTrigger = true;
                   
                    }
                
            }
        }
        else
        {
            dealtDamage = true;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!dealtDamage&&hitTarget)
            {

                if (collision.gameObject.TryGetComponent(out EnemyHealth eHealth))
                    eHealth.TakeDamage(damage);

               

            }
        }
        
    }
    public void SetPierce()
    {
        pierce = true;
        
    }

}
