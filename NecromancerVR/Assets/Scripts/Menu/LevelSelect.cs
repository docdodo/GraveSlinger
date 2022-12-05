using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    private int level;
    private bool endless;
    private bool undeadMode;
    public int levelMax;
   public  Text levelText;
   public GameObject activeOBJ;
    public GameObject inActiveOBJ;
    public GameObject undeadActiveOBJ;
    public GameObject undeadInActiveOBJ;
    private void Start()
    {
        level = 1;
        levelText.text = "Level: " + level;
    }
    public void SelectALevel()
    {
        GameObject saveManager = GameObject.FindGameObjectWithTag("SaveManager");
        if(saveManager)
        {
            saveManager.GetComponent<SaveManager>().endless = endless;
            saveManager.GetComponent<SaveManager>().undeadMode = undeadMode;
            saveManager.GetComponent<SaveManager>().SaveSkulls();
        }
        SceneManager.LoadScene(level);

    }
    public void ToggleLevel()
    {
        if (level < levelMax)
        {
            level++;
            
        }
        else
        {
            level = 1;
            
        }
        levelText.text = "Level: " + level;
    }
    public void ToggleEndless()
    {
        if(endless)
        {
            endless = false;
            activeOBJ.SetActive(false);
            inActiveOBJ.SetActive(true);
        }
        else
        {
            endless = true;
            activeOBJ.SetActive(true);
            inActiveOBJ.SetActive(false);
        }
    }
    public void ToggleUndeadMode()
    {
        if (undeadMode)
        {
            undeadMode = false;
            undeadActiveOBJ.SetActive(false);
            undeadInActiveOBJ.SetActive(true);
        }
        else
        {
            undeadMode = true;
            undeadActiveOBJ.SetActive(true);
            undeadInActiveOBJ.SetActive(false);
        }
    }
}
