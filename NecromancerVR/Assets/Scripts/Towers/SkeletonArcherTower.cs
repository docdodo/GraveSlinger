using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonArcherTower : Tower
{
    [SerializeField]
    private float chargerate;
    [SerializeField]
    private float baseChargerate;
    [SerializeField]
    private GameObject Skeleton;
    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private GameObject spawnLocation;
    [SerializeField]
    private BoxCollider boxRange;
    [SerializeField]
    private Animator SkeletonAnimation;
    private bool enemyinRange;
    private bool pierce;
    private float shootTimer;
    [Header("Audio")]
    [SerializeField]
    private AudioSource audioSource;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        boxRange.size = new Vector3(range, range, range);

    }

    // Update is called once per frame
   protected override void Update()
    {
        base.Update();
        if (enemyinRange)
        {
            shootTimer-=chargerate;
            if (shootTimer <= 0)
            {
                Invoke("ShootArrow", 0.2f);

                SkeletonAnimation.SetTrigger("Attack");

                shootTimer = speed;
            }

        }
        enemyinRange = false;
    }
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("stay");
        if (other.gameObject.tag == "Enemy")
        {

            Skeleton.transform.LookAt(other.gameObject.transform.position+new Vector3(0,1.0f,0));
            enemyinRange = true;
        }
    }
    private void ShootArrow()
    {
        if(!audioSource.isPlaying)
        audioSource.Play();
        GameObject obj = Instantiate(arrow, spawnLocation.transform.position, spawnLocation.transform.rotation);
        obj.GetComponent<Arrow>().damage = damage;
        obj.GetComponent<Arrow>().pierce = pierce;
    }
    protected override void CalculateDamage()
    {
        base.CalculateDamage();
        chargerate = baseChargerate;
        GameObject obj = GameObject.FindGameObjectWithTag("SaveManager");
        if (obj != null)
        {
            damage += obj.GetComponent<SaveManager>().GetSkulls("Archer")*1.5f;
            chargerate+=obj.GetComponent<SaveManager>().GetSkulls("Archer") * 0.05f;
            if (obj.GetComponent<SaveManager>().GetSkulls("Archer") >= 5)
            {
                pierce = true;
            }
            if (obj.GetComponent<SaveManager>().GetSkulls("Archer") >= 10)
            {
                chargerate *= 1.25f;
                
            }
        }
    }
}
