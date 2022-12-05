using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullBasket : MonoBehaviour
{
    [SerializeField]
    UIManager UI;
    [SerializeField]
    private ParticleSystem effect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Skull")
        {
            Destroy(other.gameObject);
            UI.AddSkull(1);
            if(effect.isPlaying==false)
            effect.Play();
        }
    }
}
