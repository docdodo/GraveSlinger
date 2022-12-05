using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Selector : UseableObject
{
    [SerializeField]
    private GameObject startLocation;
    [SerializeField]
    private LineRenderer lineComp;
    [SerializeField]
    private float laserMaxLength;
    [SerializeField]
    private float laserWidth;
    private Tile tile;
    private int usePressedtimer;
    private bool usePressed;
    [SerializeField]
    private List<Tower> towers;
    [SerializeField]
    private List<string> towersText;
    [SerializeField]
    private List<GameObject> tinyTowers;
    private int towerSelected;
    private bool swapPressed;
    private int swapPressedTimer;
    [SerializeField]
    private Player player;
    [SerializeField]
    private List <GameObject> enemySpawnLocations;
    [SerializeField]
    private GameObject goal;
    private NavMeshPath path;
    [SerializeField]
    private Text towerText;
    [Header("Audio")]
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip placeFailSound;
    [SerializeField]
    private AudioClip placeSucessSound;
    // Start is called before the first frame update
    void Awake()
    {
        lineComp.SetWidth(laserWidth, laserWidth);
        towerSelected = 0;
        enemySpawnLocations = new List<GameObject>();
       foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Spawn"))
        {
            enemySpawnLocations.Add(obj);
        }
        if (!goal)
            goal = GameObject.FindGameObjectWithTag("Goal");
        path = new NavMeshPath();
        towerText.text = towersText[towerSelected];
        tinyTowers[towerSelected].SetActive(true);
    }

    // Update is called once per frame
     void Update()
    {
        ShootRay(startLocation.transform.position, startLocation.transform.forward, laserMaxLength);
        
        if (usePressed&&usePressedtimer<=0)
        {
            //function here
            usePressedtimer = 45;
            BuildTower();
        }
        else if(swapPressed&&swapPressedTimer<=0)
        {
            swapPressedTimer = 10;
            SwapTower();
        }
        else
        {
            swapPressed = false;
            swapPressedTimer--;
            usePressed = false;
            usePressedtimer--;
        }


    }
    public override void Use()
    {
        usePressed = true;
    }
    public override void AltUse()
    {
        swapPressed = true;
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
           
            if (raycastHit.transform.gameObject.tag == "Tile")
            {
                
                if(raycastHit.transform.gameObject.TryGetComponent(out Tile newtile))
                {
                    if(newtile!=tile)
                    {if(tile)
                        {
                        tile.OnExit();

                        }
                        tile = newtile;
                  
                        tile.SetObstacle(true);
                       
                        
                        tile.OnEnter(towers[towerSelected]);

                    }
                }
             

            }
        }
        lineComp.SetPosition(0, targetPosition);
        lineComp.SetPosition(1, endPosition);
    }
    private void OnDisable()
    {
        if (tile)
        {
            tile.OnExit();
            tile = null;
        }

        
    }
    private void BuildTower()
    {
        if(tile)
        {
            foreach (GameObject obj in enemySpawnLocations)
            {
                obj.GetComponent<NavMeshAgent>().CalculatePath(goal.transform.position, path);
                if (path.status == NavMeshPathStatus.PathPartial)
                {
                    Debug.Log("path invalid");
                    tile.SetPathfindingInvalid();

                }
            }
            if (!tile.towerPlaced&&tile.pathFindingInvalid==false)
            {
                if (player.GetMoney() >= towers[towerSelected].value)
                {
                    tile.CreateTower(towers[towerSelected]);
                    player.AddMoney(-towers[towerSelected].value);
                    audioSource.clip = placeSucessSound;
                    audioSource.Play();
                }
            }
            else
            {
                audioSource.clip = placeFailSound;
                audioSource.Play();
            }
        }
    }
    private void SwapTower()
    {
        tinyTowers[towerSelected].SetActive(false);
        if (towerSelected == towers.Count-1)
        {
            towerSelected = 0;
        }
        else
        {
            towerSelected++;
            Debug.Log("towerselectedincrease");
        }
        towerText.text = towersText[towerSelected];
        tinyTowers[towerSelected].SetActive(true);
    }
}
