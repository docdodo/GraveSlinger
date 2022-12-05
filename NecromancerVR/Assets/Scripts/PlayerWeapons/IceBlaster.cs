using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class IceBlaster : UseableObject
{
    [SerializeField]
    private float damage;
    public int baseDamage;
    public int range;
    private bool dualWield;
    public GameObject bullet;
    public GameObject swordSpawn;
    public OVRGrabber playerGrabber;
    
    public List<Transform> spawnLocations;
    private int locationNumber;
    public float spawnRate;
    private float spawnCounter;
    private float spawnCountModifier;
    private bool pierce;
    public GameObject iceSword;
    [SerializeField]
    private GameObject iceSwordTemplate;
    private bool isBeingUsed;
    private bool isBeingUsed2;
    private bool swordFired;
    [Header("Audio")]
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip drawSwordsound;
    [SerializeField]
    private AudioClip shootSwordssound;
    private void Awake()
    {
        CalculateDamage();
    }
    // Start is called before the first frame update
    public override void Use()
    {

        isBeingUsed2 = true;
        if (!isBeingUsed)
        {
            spawnCounter += spawnCountModifier;
            if (spawnCounter >= spawnRate)
            {
                if (locationNumber < spawnLocations.Count - 1)
                {
                    locationNumber++;
                }
                else
                {
                    locationNumber = 0;
                }
                audioSource.clip = shootSwordssound;
                audioSource.Play();
                GameObject obj = Instantiate(bullet, spawnLocations[locationNumber].position, spawnLocations[locationNumber].rotation) as GameObject;
                obj.GetComponent<IceBullet>().damage = damage;
                obj.GetComponent<IceBullet>().range = range;
                if(pierce)
                obj.GetComponent<IceBullet>().SetPierce();
                spawnCounter = 0;
            }

        }


    }
    public override void AltUse()
    {
        if (!isBeingUsed2)
        {
            isBeingUsed = true;
            swordFired = false;
        }
}
    private void CalculateDamage()
    {
        damage = baseDamage;
        dualWield = false;
        spawnCountModifier = 1;
        GameObject obj = GameObject.FindGameObjectWithTag("SaveManager");
        if (obj != null)
        {
           damage+=obj.GetComponent<SaveManager>().GetSkulls("IceBlaster")*1.5f;
            
            Debug.Log(obj.GetComponent<SaveManager>().GetSkulls("IceBlaster"));
            if(obj.GetComponent<SaveManager>().GetSkulls("IceBlaster")>=5)
            {
                spawnCountModifier *= 1.2f;
                dualWield = true;
            }
            if (obj.GetComponent<SaveManager>().GetSkulls("IceBlaster") >= 10)
            {
                pierce = true;
            }
        }

    }
    private void Update()
    {
        if (!isBeingUsed2)
        {
            if (isBeingUsed)
            {
                if (!iceSword && playerGrabber.grabbedObject == null)
                {
                    audioSource.clip = drawSwordsound;
                    audioSource.Play();
                    iceSword = GameObject.Instantiate(iceSwordTemplate, swordSpawn.transform.position, swordSpawn.transform.rotation);
                    iceSword.GetComponent<IceSword>().damage = damage * 0.4f;
                    iceSword.GetComponent<IceSwordGrab>().DualWield = dualWield;
                    
                    
                    
                   
                    StartCoroutine(wait());
                }

            }

            else if (swordFired == false)
            {
                swordFired = true;

                iceSword = null;






            }
        }
        else
        {
            if (!isBeingUsed)
            {
                if (swordFired == false)
                {
                    swordFired = true;
                    if (iceSword)
                    {
                        
                        playerGrabber.ForceRelease(iceSword.GetComponent<OVRGrabbable>());
                        iceSword.GetComponent<IceSword>().isHeld = false;
                        iceSword = null;

                    }


                }
            }
        }
        isBeingUsed = false;
        isBeingUsed2 = false;
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.001f);
       
       if(playerGrabber.grabbedObject == null)
        playerGrabber.GrabBegin();
    }
    private void OnDisable()
    {
        if (iceSword)
        {

            playerGrabber.ForceRelease(iceSword.GetComponent<OVRGrabbable>());
            iceSword = null;
        }
        GameObject[] objs =GameObject.FindGameObjectsWithTag("IceSword");
            if(objs.Length>0)
            {
               foreach(GameObject obj in objs)
                {
                    obj.GetComponent<IceSwordGrab>().DualWield = false;
                    obj.GetComponent<IceSword>().isHeld = false;
                   
                }
            }
       
    }
}
