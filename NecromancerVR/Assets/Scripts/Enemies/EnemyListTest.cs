using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyListTest : MonoBehaviour
{
    public List<GameObject> enemies;
    public EnemySpawner spawn;
    public int timer;
    // Start is called before the first frame update
    void   Start()
    {
        timer = 10;
        //spawn.SpawnWave(enemies);
    }

    // Update is called once per frame
    void Update()
    {
        timer--;
        if (timer <= 0)
        {
            timer = 20000;

            spawn.SpawnWave(enemies);
        }
    }
}
