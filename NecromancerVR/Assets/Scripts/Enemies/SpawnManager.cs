using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;
using UnityEngine.SceneManagement;
[System.Serializable]
public class GameObjectList
{
    public List<GameObject> myList;
}
[System.Serializable]
public class ListWrapper
{
    public List<GameObjectList> myList;
}
public class SpawnManager : MonoBehaviour
{ [SerializeField]
   private List<EnemySpawner> spawners;
    [SerializeField]
    private GameObject portal;
    [SerializeField]
    private Text winText;
    [SerializeField]
    private GameObject winUI;
    [SerializeField]
    private int maxSkulls;
    [SerializeField]
    private int skullsEarned;
    public Text timerText;
    public Text roundText;
    public int startingMoney;
    public List<ListWrapper> enemies;
    public float WaveHealthMultiplier;
    private GameObject player;
    [SerializeField]
    private float RestTimeMax;
    [SerializeField]
    private float RestTimeMaxRound1;
    [SerializeField]
    private float RestTime;
    private bool WaveOngoing;
    private int currentWave;
    [SerializeField]
    private int maxWave;
    [SerializeField]
    private int maxWaveEndless;
    public int enemiesLeft;
    [SerializeField]
    private bool Endless;
    private bool EndlessIncreased;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioSource audioSource2;
    private bool gameOver;
    [SerializeField]
    private AudioClip countDown;
    [SerializeField]
    private AudioClip finalRound;
    [SerializeField]
    private List<AudioClip> battleMusic;
    [SerializeField]
    private AudioClip peaceMusic;
    [SerializeField]
    private AudioClip victoryMusic;
    [SerializeField]
    private AudioClip defeatMusic;
    private bool countdownStarted;
    private int highestWave;
    private int currentEndlessWave;
    private bool undeadMode;
    private bool gameEnded;
    private int lastWave;
    // Start is called before the first frame update
    
    void Start()
    {
        
        skullsEarned = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "SkullsEarned");
        highestWave = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "HighestWave");
        
        GameObject saveManager = GameObject.FindGameObjectWithTag("SaveManager");



        if (saveManager)
        {
            Endless = saveManager.GetComponent<SaveManager>().endless;
            undeadMode = saveManager.GetComponent<SaveManager>().undeadMode;
        }

        RestTime = RestTimeMaxRound1;
        audioSource.clip = peaceMusic;
        audioSource.Play();
        

        StartCoroutine(WaitToStart());
        if (undeadMode)
        {
            highestWave = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "HighestWaveUndeadMode");
            GameObject obj = GameObject.FindGameObjectWithTag("Goal");
            if (obj)
            {
                obj.GetComponent<Goal>().baseLives = 1;
                obj.GetComponent<Goal>().currentLives = 1;
                obj.GetComponent<Goal>().updateText();
                maxWave += 1;

            }
            WaveHealthMultiplier *= 2;
            for (int i = 0; i < spawners.Count; i++)
            {
                spawners[i].IncreaseHealth(0.3f);
            }

        }
        



    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            if (!WaveOngoing)
            {
                RestTime -= Time.deltaTime;

                int seconds = Convert.ToInt32(RestTime % 60);
                if (RestTime <= 5 && !countdownStarted)
                {
                    countdownStarted = true;
                    audioSource2.clip = countDown;
                    audioSource2.Play();

                }
                timerText.text = seconds.ToString();
                if (RestTime <= 0)
                {
                    player.GetComponent<Player>().EndBuildPhase();
                    WaveOngoing = true;

                    StartCoroutine(ExecuteAfterTime(0.1f));

                }
            }
        }

    }
    public void WaveEnd()
    {
        SetSpawnersActive();
      if (!Endless)
        {


            if (currentWave < maxWave)
            {
                WaveOngoing = false;
                RestTime = RestTimeMax;

                StartCoroutine(EndWave());
            }
            else
            {
               
                Victory();
                Debug.Log("over");
            }
        }
        else
        {
            if (currentWave < maxWave)
            {
                WaveOngoing = false;
                RestTime = RestTimeMax;

                StartCoroutine(EndWave());
                currentEndlessWave++;
            }
            else
            {
                WaveOngoing = false;
                RestTime = RestTimeMax;
                currentEndlessWave++;
                StartCoroutine(EndWave());
                lastWave = currentWave;
                for(int i=0; i<60; i++)
                {
                    currentWave = UnityEngine.Random.Range(maxWave + 1, maxWaveEndless);
                    if (currentWave != lastWave)
                        break;
                }
               
                if(!EndlessIncreased)
                {
                    EndlessIncreased = true;
                    WaveHealthMultiplier *= 2;
                }
            }
        }

    }
    private void StartWave()
    {
        
        int e = UnityEngine.Random.Range(0, battleMusic.Count-1);
        audioSource.clip = battleMusic[e];
        audioSource.Play();
        enemiesLeft = 0;
        for(int i=0;i<spawners.Count;i++)
        {
            
            spawners[i].SpawnWave(enemies[i].myList[currentWave].myList);
            enemiesLeft += enemies[i].myList[currentWave].myList.Count;
            if (currentWave > 0)
            {
                spawners[i].IncreaseHealth(WaveHealthMultiplier);
            }




        }
        currentWave++;
        UpdateRoundText();
        if (currentWave==maxWave&&!Endless)
        {
            audioSource2.clip = finalRound;
            audioSource2.Play();
        }


    }
    public void EnemyDead()
    {
        enemiesLeft--;
        if(enemiesLeft==0)
        {
            WaveEnd();
        }
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        StartWave();
    }
    public void RushWave()
    {
        if(!countdownStarted)
        RestTime = 0;
    }
    private void SetSpawnersActive()
    {
        for (int i = 0; i < spawners.Count; i++)
        {
            if (enemies[i].myList[currentWave].myList.Count > 0)
                spawners[i].SetWave(true);
            else
                spawners[i].SetWave(false);
        }
    }
    IEnumerator EndWave()
    {
        yield return new WaitForSeconds(1.5f);
        if (!gameOver)
        {
            audioSource.clip = peaceMusic;
            audioSource.Play();
            countdownStarted = false;
            if (!WaveOngoing)
                player.GetComponent<Player>().StartBuildPhase();
        }
    }
    public void GameOver()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            portal.SetActive(true);
            audioSource.Stop();
            audioSource2.clip = defeatMusic;
            audioSource2.Play();
            gameOver = true;
            winUI.SetActive(true);
            winText.text = "Try Again!";
            if (Endless)
            {
                if (highestWave < currentEndlessWave)
                    highestWave = currentEndlessWave;
                winText.fontSize = winText.fontSize - 4;
                winText.text = "Waves Survived: " + currentEndlessWave + " Highest Wave: " + highestWave;
            }


        }
            
            

        
    }
    private void Victory()
    {
        if (!gameEnded)
        {
            audioSource2.clip = victoryMusic;
            audioSource2.Play();
            audioSource.Stop();
            portal.SetActive(true);
            GameObject obj = GameObject.FindGameObjectWithTag("Goal");
            int e = 0;
            if (obj.GetComponent<Goal>().currentLives < obj.GetComponent<Goal>().baseLives)
            {
                e = 1;
                if (obj.GetComponent<Goal>().currentLives < (obj.GetComponent<Goal>().baseLives / 2))
                {
                    e = 2;
                }
            }
            skullsEarned = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "SkullsEarned");
            int x = maxSkulls - skullsEarned - e;
            Debug.Log("x is:" + x);
            if (x > 0)
            {
                PlayerPrefs.SetInt("UnusedSkulls", (PlayerPrefs.GetInt("UnusedSkulls") + x));

                x += skullsEarned;
                skullsEarned = x;
                Debug.Log("x is after:" + x);
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "SkullsEarned", x);
                PlayerPrefs.Save();
            }
            winUI.SetActive(true);
            winText.text = "skulls won:            " + skullsEarned + " / " + maxSkulls;
            if (undeadMode)
            {
                winText.text = "Hail the Undead Lord!";
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "UndeadMode", 1);

            }
        }
    }
    private void UpdateRoundText()
    {
        if (!Endless)
            roundText.text = "Round:      " + currentWave + " / " + maxWave;
        else
            roundText.text = "Round:       " + currentEndlessWave; 
    }
    IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(0.1f);
        SetSpawnersActive();
        UpdateRoundText();
        player = GameObject.FindGameObjectWithTag("Player");
         if(player)
         player.GetComponent<Player>().AddMoney(startingMoney);
    }
}
