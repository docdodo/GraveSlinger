using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damage;
    public bool pierce;
    public int range;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
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
        rb.velocity = transform.forward * 20;
    }
    private void OnTriggerEnter(Collider collision)
    {

        
       
            if (collision.gameObject.tag == "Enemy")
            {
                if (collision.gameObject.TryGetComponent(out EnemyHealth eHealth))
                    eHealth.TakeDamage(damage);
            if (!pierce)
                Destroy(this.gameObject);
        }
        
       
    }
}
