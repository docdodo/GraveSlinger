using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowerSpinner : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField]
    private AudioSource audioSource;
    public float damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth eHealth))
                eHealth.TakeDamage(damage);
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
