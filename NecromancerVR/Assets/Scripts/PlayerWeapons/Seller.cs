using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seller : UseableObject
{
    [SerializeField]
    private GameObject startLocation;
    [SerializeField]
    private LineRenderer lineComp;
    [SerializeField]
    private float laserMaxLength;
    [SerializeField]
    private float laserWidth;
    private Tower tower;
    private int usePressedtimer;
    private bool usePressed;

    public AudioSource audioSource;
   
   
    [SerializeField]
    private Player player;
    
    

    // Start is called before the first frame update
    void Awake()
    {
        lineComp.SetWidth(laserWidth, laserWidth);
        
       
    }

    // Update is called once per frame
     void Update()
    {
        ShootRay(startLocation.transform.position, startLocation.transform.forward, laserMaxLength);
        
        if (usePressed&&usePressedtimer<=0)
        {
            //function here
            usePressedtimer = 45;
            SellTower();
        }
        else
        {
            usePressed = false;
            usePressedtimer--;
        }
       


    }
    public override void Use()
    {
        usePressed = true;
    }
    
    private void ShootRay(Vector3 targetPosition, Vector3 direction, float length)
    {
        lineComp.positionCount = 2;
        Ray ray = new Ray(targetPosition, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = targetPosition + (length * direction);
        if (Physics.Raycast(ray, out raycastHit, length))
        {
            endPosition = raycastHit.point;
           
            if (raycastHit.transform.gameObject.tag == "Tower")
            {
                Tower newTower = raycastHit.transform.gameObject.GetComponentInParent(typeof(Tower)) as Tower;
                Debug.Log("hit tower");
                
                    if(newTower != tower)
                    {
                        tower = newTower;
                  
                        
                       
                        
                        

                    }
                
             

            }
        }
        lineComp.SetPosition(0, targetPosition);
        lineComp.SetPosition(1, endPosition);
    }
    private void OnDisable()
    {
        if (tower)
        {
           
            tower = null;
        }

        
    }
    private void SellTower()
    {
   
        if(tower&&tower.myTile)
        {
            player.AddMoney(tower.Sell());
            tower.myTile.DestroyTower();
            tower = null;
           if(audioSource.isPlaying==false)
            {
                audioSource.Play();
            }
                    
                    
              
        }
    }
   
}
