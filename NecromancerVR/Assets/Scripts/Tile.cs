using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private Color startcolor;
    [SerializeField]
    private Color hovercolor;
    [SerializeField]
    private Color hovercolorfailed;
    private Renderer renderer;
    [SerializeField]
    private GameObject RangeCube;
    public bool towerPlaced;
   
    public bool pathFindingInvalid;
    [SerializeField]
    private NavMeshObstacle obstacle;
    public Tower tower;
    // Start is called before the first frame update
    void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        renderer.material.color = startcolor;
        obstacle = GetComponent<NavMeshObstacle>();
    }

    // Update is called once per frame
    public void OnEnter(Tower tower_)
    {
        if (!towerPlaced && !pathFindingInvalid)
        {
            renderer.material.color = hovercolor;
            RangeCube.SetActive(true);
            RangeCube.transform.localScale = new Vector3(tower_.range*1.4f, tower_.range*0.5f, tower_.range*1.4f);
        }
        else
        {
            renderer.material.color = hovercolorfailed;

        }
    }
    public void OnExit()
    {
        SetObstacle(false);
        RangeCube.SetActive(false);
        renderer.material.color = startcolor;
        if (towerPlaced == false)
            pathFindingInvalid = false;
    }
    public void CreateTower(Tower tower_)
    {
     tower=Instantiate(tower_,(gameObject.transform.position+ new Vector3(0,1.25f,0)),gameObject.transform.rotation);
        tower.myTile = this;
        towerPlaced = true;
        
    }
    public void DestroyTower()
    {
        Debug.Log("destroy tower");
        Destroy(tower.gameObject);

        tower = null;
        towerPlaced = false;
    }
    public void SetObstacle(bool active_)
    {
        obstacle.enabled = active_;
    }
    public void SetPathfindingInvalid()
    {
        renderer.material.color = hovercolorfailed;
        pathFindingInvalid = true;

    }
}
