using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Bomb : OVRGrabbable
{
    Rigidbody rb;
   public Vector3 force;
    private bool released;
    public GameObject explosion;
    public float damage;
    public int range;
    private bool collided;
    private void Start()
    {
         rb= gameObject.GetComponent<Rigidbody>();

    }
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
   
        rb.isKinematic = m_grabbedKinematic;
        rb.velocity = linearVelocity;
        force = rb.velocity;
       // rb.angularVelocity = angularVelocity;
        m_grabbedBy = null;
        m_grabbedCollider = null;
        released = true;
        gameObject.transform.forward = force;
        rb.useGravity = true;
        range = 1000;
    }
   
    private void FixedUpdate()
    {
        if (released)
        {
            rb.velocity += force * 0.03f;
            range--;
            if(range<=0)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collided)
        {
            collided = true;
            float money = 0;
            GameObject explode = GameObject.Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);

            Collider[] tmp = Physics.OverlapSphere(collision.gameObject.transform.position, gameObject.transform.localScale.z * 10f);

            foreach (Collider collider in tmp)
            {
                if (collider.gameObject.tag == "Enemy")
                {
                    if (collider.gameObject.TryGetComponent(out EnemyHealth eHealth))
                    {

                        eHealth.TakeDamage(damage);
                        money += damage;
                    }
                }
            }
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            money /= 10;
            player.GetComponent<Player>().AddMoney(Convert.ToInt32(money));
            Destroy(gameObject);
        }

    }

}
