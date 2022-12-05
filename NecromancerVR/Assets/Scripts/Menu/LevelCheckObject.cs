using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelCheckObject : MonoBehaviour
{
    public int levels;
    public List<Text> levelTexts;
    public List<int> levelMaxSkulls;
    public List<GameObject> unDeadEffects;

    // Start is called before the first frame update
    void Start()
    {
      
        SetAll();
    }

    void SetAll()
    {
        for(int i = 1; i <= levels; i++)
        {

            
            levelTexts[i].text = "Level " + i + " Skulls Earned: " + PlayerPrefs.GetInt(NameFromIndex(i) + "SkullsEarned")+" / "+levelMaxSkulls[i];
            if(PlayerPrefs.GetInt(NameFromIndex(i)+ "UndeadMode") ==1)
            {
                unDeadEffects[i].SetActive(true);
                levelTexts[i].text = "Undead mode complete ";
            }
                
        }
    }
    private static string NameFromIndex(int BuildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }
}
