using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemySpawner : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject goal;
    private NavMeshPath path;
    [SerializeField]
    private List<GameObject> enemies;
    public bool waveActive;
    public int spawnTimer;
    private int spawnTimerCurrent;
    [SerializeField]
    private float HealthMultiplier;
    [SerializeField]
   
    private GameObject spawnLocation;
    [SerializeField]
    private GameObject Activeobj;
    [SerializeField]
    private GameObject InActiveobj;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        HealthMultiplier = 1.0f;
        goal = GameObject.FindGameObjectWithTag("Goal");
        spawnTimer = 90;
        spawnTimerCurrent = spawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(waveActive)
        {
            spawnTimerCurrent--;
            if(spawnTimerCurrent<=0)
            {
              GameObject obj=  Instantiate(enemies[0],spawnLocation.transform.position,spawnLocation.transform.rotation);
               if(obj.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent1))
                {
                    agent1.SetPath(path);
                }
                if (obj.TryGetComponent<EnemyHealth>(out EnemyHealth Ehealth))
                {
                    Ehealth.IncreaseBaseHealth(HealthMultiplier);
                }
                enemies.RemoveAt(0);
                if(enemies.Count==0)
                    waveActive = false;
                
                    
                spawnTimerCurrent = spawnTimer;
            }
        }
    }
   public void SpawnWave(List<GameObject> enemies_)
    {
        if (enemies_.Count > 0)
        {
            enemies = new List<GameObject>(enemies_);

            spawnTimerCurrent = spawnTimer;
            waveActive = true;
            agent.CalculatePath(goal.transform.position, path);
        }
    }
    public void IncreaseHealth(float increase_)
    {
       // HealthMultiplier += increase_*HealthMultiplier;
        HealthMultiplier += increase_;
        Debug.Log("HealthM: " + HealthMultiplier);
    }
    public void SetWave(bool active_)
    {
        if(active_)
        {
            Activeobj.SetActive(true);
            InActiveobj.SetActive(false);
        }
        else
        {
            Activeobj.SetActive(false);
            InActiveobj.SetActive(true);
        }
    }
}
