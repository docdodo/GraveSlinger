using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float damage;
    public int range;
    [SerializeField]
    private GameObject explosion;
    private Rigidbody rb;
    public bool doubleExplosion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * 5;
        if (range <= 0)
            range = 10;
    }

    // Update is called once per frame
    void Update()
    {
        range--;
        if (range <= 0)
        {
            Destroy(this.gameObject);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject explode = GameObject.Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        explode.transform.localScale = gameObject.transform.localScale * 3;
        Collider[] tmp = Physics.OverlapSphere(collision.gameObject.transform.position, 3f);
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
        Debug.Log("Enemy hit");
        if (doubleExplosion)
        {
            doubleExplosion = false;
            rb.velocity = Vector3.Reflect(collision.relativeVelocity * -1, collision.contacts[0].normal);
           
        }
        else
        {
            Destroy(this.gameObject);

        }
    }
}
