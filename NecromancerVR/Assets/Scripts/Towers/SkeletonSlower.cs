using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSlower : Tower
{
    [SerializeField]
    private GameObject sphere1;
    [SerializeField]
    private GameObject sphere2;
    [SerializeField]
    private GameObject Skeleton;
    [SerializeField]
    private BoxCollider boxRange;
    [SerializeField]
    private Animator SkeletonAnimation;
    [SerializeField]
    private ParticleSystem particleSys;
   
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        boxRange.size = new Vector3(range, range, range);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if(other.gameObject.TryGetComponent(out EnemyMovement movement))
            movement.ApplySlow(true);
            if (!particleSys.isPlaying)
            {
                particleSys.Play();
             
            }
            SkeletonAnimation.SetTrigger("AOEATTACK");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.TryGetComponent(out EnemyMovement movement);
            movement.ApplySlow(false);
            particleSys.Stop();
        }
    }
    protected override void CalculateDamage()
    {
        base.CalculateDamage();
        damage = baseDamage;
        GameObject obj = GameObject.FindGameObjectWithTag("SaveManager");
        if (obj != null)
        {
            damage += obj.GetComponent<SaveManager>().GetSkulls("Slower") * 4;
            if (obj.GetComponent<SaveManager>().GetSkulls("Slower") >= 5)
            {
                sphere1.SetActive(true);
                sphere1.GetComponent<SlowerSpinner>().damage = damage;
            }
            if (obj.GetComponent<SaveManager>().GetSkulls("Slower") >= 10)
            {
                sphere2.SetActive(true);
                sphere2.GetComponent<SlowerSpinner>().damage = damage;
            }

        }
    }
    
}
