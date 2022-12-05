using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuBound : MonoBehaviour
{
   public GameObject unUsedSkulls;
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Skull")
        {
            unUsedSkulls.GetComponent<UnusedSkulls>().AddSkull();
           
        }
        Destroy(collision.gameObject);
    }
}
