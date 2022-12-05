using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    public int baseLives;
    [SerializeField]
    public int currentLives;
    private GameObject spawnManager;
    public Text livesText;
    // Start is called before the first frame update
    void Start()
    {
        currentLives = baseLives;
        livesText.text = "Lives:       " + currentLives + " / " + baseLives;
        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
          currentLives-=  other.gameObject.GetComponent<EnemyHealth>().life;
            livesText.text = "Lives:       " + currentLives + " / " + baseLives;
            Destroy(other.gameObject);
            if(currentLives<=0)
            {
                spawnManager.GetComponent<SpawnManager>().GameOver();
                
            }
        }
    }
    public void updateText()
    {
        livesText.text = "Lives:       " + currentLives + " / " + baseLives;
    }
}
