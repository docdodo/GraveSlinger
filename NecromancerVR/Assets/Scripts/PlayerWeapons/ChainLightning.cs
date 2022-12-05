using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ChainLightning : UseableObject
{
    [SerializeField]
    protected float damage;
    [SerializeField]
    protected float baseDamage;
    [SerializeField]
    protected int range;
    protected int chaintargets;
    [SerializeField]
    private GameObject startLocation;
    [SerializeField]
    private LineRenderer lineComp;
    [SerializeField]
    private List <LightningBoltScript> bolts;

    public float laserWidth = 0.1f;
    public float laserMaxLength;
    private bool isBeingUsed;
    [SerializeField]
    private int maxNumberOfChains;
    public int baseMaxNumberOfChains;
    
    public int chainRadius;
    public int BaseChainRadius;
    private bool hitEnemy;
    private Vector3 hitlocation;
    private List<GameObject> EnemiesHit;
    [Header("Audio")]
    [SerializeField]
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Awake()
    {
        lineComp.SetWidth(laserWidth, laserWidth);
        EnemiesHit = new List<GameObject>();
        CalculateDamage();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingUsed)
        {
            lineComp.enabled = true;
            ShootRay(startLocation.transform.position, startLocation.transform.forward, laserMaxLength);
          if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
            lineComp.enabled = false;
            lineComp.positionCount = 2;

        }
        isBeingUsed = false;
        
    }
    public override void Use()
    {
        
        
       
        isBeingUsed = true;
    }
    public override void AltUse()
    {
        Debug.Log("Altuse");
    }
    private void ShootRay(Vector3 targetPosition, Vector3 direction, float length)
    {
        lineComp.positionCount = 2;
        foreach(LightningBoltScript bolt in bolts)
        {
            bolt.StartPosition = gameObject.transform.position;
            bolt.EndPosition = gameObject.transform.position;
        }
         Ray ray = new Ray(targetPosition, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = targetPosition + (length * direction);
             hitEnemy = false;
        if (Physics.Raycast(ray, out raycastHit, length))
        {
            endPosition = raycastHit.point;
           if (raycastHit.transform.gameObject.tag=="Enemy")
            {
                hitEnemy = true;
                hitlocation = raycastHit.transform.position;
                EnemiesHit.Add(raycastHit.transform.gameObject);
                Debug.Log("hit");
                

            }
        }
        lineComp.SetPosition(0, targetPosition);
        lineComp.SetPosition(1, endPosition);
        bolts[0].EndPosition = endPosition;
        bolts[0].Trigger();
       
        if (hitEnemy)
        {
        
               

           Collider[] tmp= Physics.OverlapSphere(hitlocation, chainRadius);
            Debug.Log(tmp);
            int currentChains=0;
            foreach (Collider collider in tmp)
            {

                if (collider.gameObject.tag == "Enemy" && !EnemiesHit.Contains(collider.gameObject))
                {
                    lineComp.positionCount += 1;
                    Debug.Log("hit another enemy");
                    currentChains += 1;
                    bolts[currentChains].StartPosition = bolts[currentChains - 1].EndPosition;
                    lineComp.SetPosition((lineComp.positionCount-1), collider.transform.position);
                    EnemiesHit.Add(collider.gameObject);
                    bolts[currentChains].EndPosition = collider.transform.position + new Vector3(0, 1, 0) ;
                    bolts[currentChains].Trigger();
                }
                if (currentChains >= maxNumberOfChains)
                    break;

            }
           
            //deal damage based on list
            foreach (GameObject enemy in EnemiesHit)
            {
                if (enemy.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
                    enemyHealth.TakeDamage(damage);

                
            }
           






        }
        
       

        EnemiesHit.Clear();
    }
    private void CalculateDamage()
    {
        damage = baseDamage;
        maxNumberOfChains = baseMaxNumberOfChains;
        chainRadius = BaseChainRadius;
        GameObject obj = GameObject.FindGameObjectWithTag("SaveManager");
        if (obj != null)
        {
            damage += obj.GetComponent<SaveManager>().GetSkulls("ChainLightning") * 0.1f;
            
            if (obj.GetComponent<SaveManager>().GetSkulls("ChainLightning") >= 5)
            {
                maxNumberOfChains += 1;
            }
            if (obj.GetComponent<SaveManager>().GetSkulls("ChainLightning") >= 10)
            {
                maxNumberOfChains += 2;
                chainRadius += chainRadius / 4;
            }
        }

    }
}
